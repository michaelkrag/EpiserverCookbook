using CommonLib.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.MediaR
{
    public class VariantsResponce
    {
        public IEnumerable<Variant> Variants { get; set; } = new List<Variant>();
        public Variant ActiveVariant { get; set; }

        public bool Any()
        {
            return Variants?.Any() ?? false;
        }

        public bool IsPrimaryVariant(Variant variant)
        {
            var result = ActiveVariant.Equals(variant);
            return result;
        }

        public bool HasActiveVariant()
        {
            return ActiveVariant != null;
        }
    }

    public class Variant : ValueObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
        public string DisplayName { get; set; }
        public string NormalPrice { get; set; }
        public string DiscountPrice => Discounts.LastOrDefault()?.Price ?? string.Empty;
        public IEnumerable<DiscountView> Discounts = new List<DiscountView>();

        public bool HasDiscount => Discounts.Any();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
        }
    }

    public class DiscountView
    {
        public string Price { get; set; }
        public string Discription { get; set; }
    }
}