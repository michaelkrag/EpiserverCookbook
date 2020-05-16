using Mediachase.BusinessFoundation.Core;
using Mediachase.BusinessFoundation.Data;
using Mediachase.BusinessFoundation.Data.Business;
using Mediachase.BusinessFoundation.Data.Meta.Management;
using MovieShop.Business.Factory;
using MovieShop.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MovieShop.Features.Home
{
    public class HomePageController : BasePageController<HomePage>
    {
        private readonly IViewModelFactory _viewModelFactory;

        public HomePageController(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public async Task<ActionResult> Index(HomePage currentPage)
        {
            var viewModel = await _viewModelFactory.Create(currentPage);
            return View("~/Features/Home/HomePage.cshtml", viewModel);
        }

        public void CreateBusinessFoundation()
        {
            var clubCard = DataContext.Current.GetMetaClass("ClubCard");
            if (clubCard == null)
            {
                using (MetaClassManagerEditScope metaEdit = DataContext.Current.MetaModel.BeginEdit())
                {
                    clubCard = DataContext.Current
                                                    .MetaModel
                                                    .CreateMetaClass("ClubCard", "Club Card", "ClubCards", "demoClub_Cards", PrimaryKeyIdValueType.Integer);
                    clubCard.AccessLevel = AccessLevel.Customization;
                    metaEdit.SaveChanges();
                }
            }

            MetaFieldType cardEnum = DataContext.Current.MetaModel.RegisteredTypes["CardType"];
            if (cardEnum == null)
            {
                using (MetaClassManagerEditScope metaEdit = DataContext.Current.MetaModel.BeginEdit())
                {
                    cardEnum = MetaEnum.Create("CardType", "Club Card Type", false);
                    cardEnum.AccessLevel = AccessLevel.Customization;
                    metaEdit.SaveChanges();
                    MetaEnum.AddItem(cardEnum, "Gold", 1);
                    MetaEnum.AddItem(cardEnum, "Silver", 2);
                    MetaEnum.AddItem(cardEnum, "Bronze", 3);
                }
            }

            using (MetaFieldBuilder fieldBuilder = new MetaFieldBuilder(DataContext.Current.GetMetaClass("ClubCard")))
            {
                MetaField titleField = fieldBuilder.CreateText("TitleField", "Title Field", false, 100, false);
                fieldBuilder.MetaClass.TitleFieldName = titleField.Name;
                fieldBuilder.CreateText("CardOwnerName", "Card Owner Name", false, 100, false);
                fieldBuilder.CreateEmail("Email", "Email", false, 100, true);
                fieldBuilder.CreateInteger("Balance", "Balance", true, 0);
                var mf = fieldBuilder.CreateEnumField("CardTypeEnum", "Card Type", cardEnum.Name, true, string.Empty, true);
                mf.AccessLevel = AccessLevel.Customization;
                fieldBuilder.SaveChanges();

                MetaDataWrapper.CreateReference("Contact", "ClubCard", "ContactRef", "Contact Reference", false, "InfoBlock", "ClubCard", "10");
            }
            //set data
            EntityObject cardObjSet = true ? BusinessManager.InitializeEntity("ClubCard") : BusinessManager.Load("ClubCard", 12);
            cardObjSet["TitleField"] = "test";

            if (true)
            {
                BusinessManager.Create(cardObjSet);
            }
            else
            {
                BusinessManager.Update(cardObjSet);
            }

            //get data

            EntityObject cardObj = BusinessManager.Load("ClubCartd", 12);
            var test = (string)cardObj["TitleField"];

            //delete

            DataContext.Current.MetaModel.DeleteMetaClass("ClubCard");
            MetaFieldType cardEnumDelete = DataContext.Current.MetaModel.RegisteredTypes["CardType"];
            if (cardEnumDelete != null && !MetaEnum.IsUsed(cardEnum))
            {
                MetaEnum.Remove(cardEnumDelete);
            }
        }
    }
}