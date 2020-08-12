using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.Security;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Markets;
using Mediachase.Commerce.Pricing;
using MoveDb;
using MoveDb.Models;
using MovieShop.Business.Extensions;
using MovieShop.Business.Services.ImageStore;
using MovieShop.Domain.Commerce.Nodes;
using MovieShop.Domain.Commerce.Products;
using MovieShop.Domain.Commerce.Variants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MovieShop.Business.ScheduledJob
{
    [ScheduledPlugIn(DisplayName = "Import movies", GUID = "8408E621-1B5E-49C2-A52D-C9BA4DE9990B")]
    public class ImportMoviesScheduledJob : ScheduledJobBase
    {
        private readonly IContentLoader _contentLoader;
        private readonly ReferenceConverter _referenceConverter;
        private readonly IContentRepository _contentRepository;
        private readonly IRelationRepository _relationRepository;
        private readonly IImageRepository _imageRepository;
        private readonly MovieDbClient _movieDbClient;
        private readonly IPriceDetailService _priceDetailService;
        private readonly IMarketService _marketService;
        private bool _stopSignaled = false;
        private Dictionary<string, ContentReference> _generDictionary = new Dictionary<string, ContentReference>();

        public ImportMoviesScheduledJob(IContentLoader contentLoader, ReferenceConverter referenceConverter, IContentRepository contentRepository, IRelationRepository relationRepository, IImageRepository imageRepository, IPriceDetailService priceDetailService, IMarketService marketService)
        {
            _contentLoader = contentLoader;
            _referenceConverter = referenceConverter;
            _contentRepository = contentRepository;
            _relationRepository = relationRepository;
            _imageRepository = imageRepository;
            IsStoppable = true;
            _movieDbClient = new MovieDbClient();
            _priceDetailService = priceDetailService;
            _marketService = marketService;
        }

        public override void Stop()
        {
            _stopSignaled = true;
        }

        public override string Execute()
        {
            OnStatusChanged($"Starting execution of {this.GetType()}");
            //var path = HttpContext.Current.Server.MapPath("~/App_Data/movie_ids_10_26_2019.json");
            //var path = $"{EPiServerFrameworkSection.Instance.AppData.BasePath}/movie_ids_10_26_2019.json";

            var path = $"c:\\Users\\krag7\\source\\repos\\EpiserverCookbook\\src\\MovieShop\\MovieShop\\App_Data\\movie_ids_10_26_2019.json";

            //_relationRepository.SetNodeParent("MovieCatelog");
            var movieCount = 0;
            var movieImportedCount = 0;
            var catelogReference = GetOrCreateCatelog("MovieCatelog");
            var movieNode = GetOrCreateMovieNode("Movies", catelogReference);
            using (StreamReader readFile = new StreamReader(path))
            {
                string line;
                while ((line = readFile.ReadLine()) != null)
                {
                    if (_stopSignaled)
                    {
                        return $"Stoped movies in all {movieCount}, movie index now {movieImportedCount}";
                    }
                    var movieid = JsonConvert.DeserializeObject<MoveId>(line);
                    try
                    {
                        if (!ContentExcist(movieid.id.ToString()))
                        {
                            var movieDetails = _movieDbClient.GetMovieDetails(movieid.id).Result;
                            var castAndCrew = _movieDbClient.GetCastAndCrew(movieid.id).Result;

                            var genreName = movieDetails?.genres?.FirstOrDefault()?.name ?? "Unkown";
                            //create genres folder
                            var nodeReference = CreateGenresNode(genreName, movieNode);

                            //create product folder
                            var productReference = CreateProduct(movieDetails, castAndCrew, nodeReference);
                            AddNodeEntryRelation(productReference, nodeReference, true);
                            //create variant BlueRay and DVD
                            var bluRay = CreateVariant(movieDetails, "BlueRay", productReference, true);
                            AddNodeEntryRelation(bluRay, nodeReference, true);
                            AddProductVariation(productReference, bluRay);

                            var dvd = CreateVariant(movieDetails, "DVD", productReference, false);
                            AddNodeEntryRelation(dvd, nodeReference, false);
                            AddProductVariation(productReference, dvd);
                            movieImportedCount++;
                        }
                        movieCount++;
                        if (movieCount % 100 == 0)
                        {
                            OnStatusChanged($"Status movies in all {movieCount}, movie index now {movieImportedCount}");
                        }
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged($"Error on recive {movieid.id} : {ex.Message}");
                    }
                }
            }

            return $"Done movies in all {movieCount}, movie index now {movieImportedCount}";
        }

        public bool ContentExcist(string code)
        {
            var reference = _referenceConverter.GetContentLink(code);
            if (ContentReference.IsNullOrEmpty(reference))
            {
                return false;
            }
            try
            {
                var content = _contentLoader.Get<IContent>(reference);
                return content == null ? false : true;
            }
            catch
            {
                return false;
            }
        }

        public ContentReference GetOrCreateCatelog(string catalogName)
        {
            var root = _referenceConverter.GetRootLink();

            var catalogs = _contentRepository.GetChildren<CatalogContent>(root);
            var catalog = catalogs?.Where(x => x.Name == catalogName).FirstOrDefault();
            if (catalog != null)
            {
                return catalog.ContentLink;
            }

            var newCatalog = _contentRepository.GetDefault<CatalogContent>(root);
            newCatalog.Name = catalogName;
            newCatalog.DefaultCurrency = "USD";
            newCatalog.DefaultLanguage = "en";
            newCatalog.WeightBase = "kgs";
            newCatalog.LengthBase = "cm";
            var contentReference = _contentRepository.Save(newCatalog, SaveAction.Publish, AccessLevel.NoAccess);
            return contentReference;
        }

        public ContentReference GetOrCreateMovieNode(string nodeName, ContentReference parent)
        {
            var children = _contentRepository.GetChildren<MovieNode>(parent);
            var movieNode = children?.Where(x => x.Name == nodeName).FirstOrDefault();
            if (movieNode != null)
            {
                return movieNode.ContentLink;
            }
            var code = $"{nodeName}_1";
            var newMovieNode = _contentRepository.GetDefault<MovieNode>(parent);
            newMovieNode.Name = nodeName;
            newMovieNode.Code = code;
            newMovieNode.SeoUri = $"{code}.aspx";
            var contentReference = _contentRepository.Save(newMovieNode, SaveAction.Publish, AccessLevel.NoAccess);
            return contentReference;
        }

        public ContentReference CreateGenresNode(string nodeName, ContentReference parent)
        {
            if (_generDictionary.ContainsKey(nodeName))
            {
                return _generDictionary[nodeName];
            }

            var children = _contentRepository.GetChildren<GenreNode>(parent);
            var movieNode = children?.Where(x => x.Name == nodeName).FirstOrDefault();
            if (movieNode != null)
            {
                _generDictionary[nodeName] = movieNode.ContentLink;
                return movieNode.ContentLink;
            }

            var code = $"{nodeName.Replace(" ", "-")}_1";
            var product = _contentRepository.GetDefault<GenreNode>(parent);
            product.Name = nodeName;
            product.Code = code;
            product.SeoUri = $"{code}.aspx";
            return _contentRepository.Save(product, SaveAction.Publish, AccessLevel.NoAccess);
        }

        /**/

        public void AddProductVariation(ContentReference referenceToProduct, ContentReference referenceToVariation)
        {
            var newVariation = new ProductVariation
            {
                SortOrder = 100,
                Parent = referenceToProduct,
                Child = referenceToVariation,
                GroupName = "Default",
                Quantity = 1.000M
            };
            _relationRepository.UpdateRelation(newVariation);
        }

        public void AddNodeEntryRelation(ContentReference child, ContentReference parent, bool isPrimary)
        {
            var nodeEntryRelation = new NodeEntryRelation()
            {
                IsPrimary = isPrimary,
                Child = child,
                Parent = parent
            };
            _relationRepository.UpdateRelation(nodeEntryRelation);
        }

        public ContentReference CreateProduct(MovieDetails movieDetails, CastAndCrew castAndCrew, ContentReference linkToParentNode)
        {
            var name = $"{movieDetails.title.TruncateString(50)} ({ movieDetails.release_date.ToYear()})";
            //Create a new instance of CatalogContentTypeSample that will be a child to the specified parentNode.
            var assetLink = ContentReference.EmptyReference;
            if (!string.IsNullOrEmpty(movieDetails.poster_path))
            {
                try
                {
                    var imageData = _movieDbClient.GetImage(movieDetails.poster_path).Result;
                    assetLink = _imageRepository.Insert($"MovieBackdrop/{movieDetails.title.First()}/{name}", Path.GetExtension(movieDetails.poster_path), imageData);
                }
                catch
                {
                    Debug.WriteLine($"{movieDetails.title} is missing image");
                }
            }
            var product = _contentRepository.GetDefault<MovieProduct>(linkToParentNode);
            product.Name = name;
            product.StartPublish = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
            product.Code = movieDetails.id.ToString();
            product.SeoUri = $"{movieDetails.id}.aspx";
            product.DisplayName = NameFix(movieDetails.title);
            product.ImdbId = movieDetails.imdb_id;
            product.OriginalTitle = movieDetails.original_title;
            product.Title = movieDetails.title;
            product.Overview = movieDetails.overview;
            product.TheMovieDbId = movieDetails.id.ToString();
            product.Adult = movieDetails.adult;
            product.ReleaseDate = movieDetails.release_date.HasValue ? movieDetails.release_date.Value : DateTime.MinValue;
            product.VoteAverage = movieDetails.vote_average;
            product.VoteCount = movieDetails.vote_count.HasValue ? movieDetails.vote_count.Value : int.MinValue;
            product.Genres = movieDetails?.genres?.FirstOrDefault()?.name ?? "Unkown";
            product.OrginalLang = movieDetails.original_language;
            product.BelongsToCollection = movieDetails?.belongs_to_collection?.name ?? string.Empty;
            product.SpokenLanguages = movieDetails?.spoken_languages?.FirstOrDefault()?.name ?? string.Empty;
            product.Casts = castAndCrew?.Cast.Select(x => new MovieShop.Domain.Commerce.Products.Cast() { CharacterName = x.Character, Gender = x.Gender, Name = x.Name }).ToList();
            product.Crews = castAndCrew?.Crew.Select(x => new MovieShop.Domain.Commerce.Products.Crew() { Job = x.Job, Gender = x.Gender, Name = x.Name }).ToList();

            if (!ContentReference.IsNullOrEmpty(assetLink))
            {
                product.CommerceMediaCollection.Add(new CommerceMedia()
                {
                    AssetLink = assetLink,
                    AssetType = "Image",
                    GroupName = "Poster",
                    SortOrder = 1,
                });
            }
            return _contentRepository.Save(product, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentReference CreateVariant(MovieDetails movieDetails, string mediaType, ContentReference linkToParentNode, bool isPrime)
        {
            //Create a new instance of CatalogContentTypeSample that will be a child to the specified parentNode.
            var newSku = _contentRepository.GetDefault<MovieVariant>(linkToParentNode);
            //Set some required properties.
            newSku.Code = $"{movieDetails.id}-{mediaType}";
            newSku.SeoUri = $"{movieDetails.id}-{mediaType}.aspx";
            newSku.MediaTypes = mediaType;
            newSku.IsPrimary = isPrime;
            newSku.Title = $"{movieDetails.title.TruncateString(50)} {mediaType}";
            newSku.Name = $"{movieDetails.title.TruncateString(50)} ({ movieDetails.release_date.ToYear()}) {mediaType}";
            newSku.DisplayName = $"{movieDetails.title.TruncateString(50)} ({ movieDetails.release_date.ToYear()}) {mediaType}";
            //Set the description
            //  newSku.Description = "This new SKU is great";
            //Publish the new content and return its ContentReference.
            var contentRefereence = _contentRepository.Save(newSku, SaveAction.Publish, AccessLevel.NoAccess);

            AddPrice(newSku);
            return contentRefereence;
        }

        public void AddPrice(VariationContent variant)
        {
            var allMarkets = _marketService.GetAllMarkets();
            var added = 0;

            var newPrices = new List<PriceDetailValue>();
            var currentPrices = _priceDetailService.List(variant.ContentLink);
            //     var currentPrices2 = _priceService.GetCatalogEntryPrices(new CatalogKey(variant.Code));

            var markets = currentPrices?.Select(x => x.MarketId).Distinct().ToHashSet();

            foreach (var market in allMarkets)
            {
                if (!markets.Contains(market.MarketId))
                {
                    foreach (var currency in market.Currencies)
                    {
                        PriceDetailValue newPriceEntry = new PriceDetailValue();

                        newPriceEntry.CatalogKey = new CatalogKey(variant.Code);
                        newPriceEntry.MinQuantity = 0;
                        newPriceEntry.MarketId = market.MarketId;
                        newPriceEntry.UnitPrice = new Money(GetPrice(currency), currency);
                        newPriceEntry.ValidFrom = DateTime.Now.AddDays(-1);
                        newPriceEntry.ValidUntil = DateTime.Now.AddYears(20);
                        newPriceEntry.CustomerPricing = new CustomerPricing(0, "");
                        newPrices.Add(newPriceEntry);
                    }
                }
            }

            if (newPrices.Any())
            {
                _priceDetailService.Save(newPrices);
                added += newPrices.Count;
            }
        }

        private static Random _random = new Random();

        public decimal GetPrice(Currency currency)
        {
            if (currency == Currency.DKK)
            {
                return _random.Next(119, 240);
            }
            if (currency == Currency.EUR)
            {
                return _random.Next(8, 11);
            }
            if (currency == Currency.USD)
            {
                return _random.Next(5, 9);
            }
            return _random.Next(5, 900);
        }

        private const int MaxNameLength = 100;
        private const string LongestPrefix = " blueray";

        private static string NameFix(string movieName)
        {
            var name = $"{movieName}{LongestPrefix}".Length > MaxNameLength ? $"{movieName.Substring(0, MaxNameLength - LongestPrefix.Length - "...".Length)}..." : $"{movieName}";

            return name;
        }
    }
}