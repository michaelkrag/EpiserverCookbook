using EPiServer.Commerce.Marketing;
using EPiServer.Commerce.Marketing.DataAnnotations;
using EPiServer.Commerce.Marketing.Promotions;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Promotions
{
    [ContentType(DisplayName = "Free stuff", GUID = "2563A9F9-DF28-4196-AAF6-191D993A64B7", Description = "")]
    public class FreeStuffPromotion : EntryPromotion
    {
        [PromotionRegion(PromotionRegionName.Condition)]
        [Display(Order = 20)]
        public virtual PurchaseQuantity RequiredQty { get; set; }

        [PromotionRegion(PromotionRegionName.Reward)]
        [Display(Order = 30)]
        public virtual IList<ContentReference> FreeItems { get; set; }
    }
}