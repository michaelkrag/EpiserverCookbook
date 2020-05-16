using EPiServer;
using EPiServer.Commerce.Extensions;
using EPiServer.Commerce.Marketing;
using EPiServer.Commerce.Marketing.Extensions;
using EPiServer.Commerce.Order;
using EPiServer.Framework.Localization;
using Mediachase.Commerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Promotions
{
    public class FreeStuffPromotionProcessor : EntryPromotionProcessorBase<FreeStuffPromotion>
    {
        private readonly CollectionTargetEvaluator _collectionTargetEvaluator;
        public readonly FulfillmentEvaluator _fulfillmentEvaluator;
        private readonly LocalizationService _localizationService;
        public readonly GiftItemFactory _giftItemFactory;
        private readonly IContentLoader _contentLoader;
        public readonly ReferenceConverter _referenceConverter;

        public FreeStuffPromotionProcessor(RedemptionDescriptionFactory redemptionDescriptionFactory,
                                            CollectionTargetEvaluator collectionTargetEvaluator,
                                            FulfillmentEvaluator fulfillmentEvaluator,
                                            LocalizationService localizationService,
                                            GiftItemFactory giftItemFactory,
                                            IContentLoader contentLoader,
                                            ReferenceConverter referenceConverter
                                            ) : base(redemptionDescriptionFactory)
        {
            _collectionTargetEvaluator = collectionTargetEvaluator;
            _fulfillmentEvaluator = fulfillmentEvaluator;
            _localizationService = localizationService;
            _giftItemFactory = giftItemFactory;
            _contentLoader = contentLoader;
            _referenceConverter = referenceConverter;
        }

        protected override RewardDescription Evaluate(FreeStuffPromotion promotionData, PromotionProcessorContext context)
        {
            var condition = promotionData.RequiredQty;
            var lineItems = context.OrderForm.GetAllLineItems();
            var skuCodes = _collectionTargetEvaluator.GetApplicableCodes(lineItems, condition.Items, false);
            var status = promotionData.RequiredQty.GetFulfillmentStatus(context.OrderForm, _collectionTargetEvaluator, _fulfillmentEvaluator);

            var redemptions = new List<RedemptionDescription>();
            if (status == FulfillmentStatus.Fulfilled)
            {
                var entries = _giftItemFactory.CreateGiftItems(promotionData.FreeItems, context);
                redemptions.Add(CreateRedemptionDescription(entries));
            }
            return RewardDescription.CreateGiftItemsReward(status, redemptions, promotionData, status.GetRewardDescriptionText());
        }

        protected override PromotionItems GetPromotionItems(FreeStuffPromotion promotionData)
        {
            throw new NotImplementedException();
        }
    }
}