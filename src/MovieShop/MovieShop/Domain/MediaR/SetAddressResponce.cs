namespace MovieShop.Domain.MediaR
{
    public class SetAddressResponce
    {
        public bool IsValid { get; }

        public SetAddressResponce(bool isValid)
        {
            IsValid = isValid;
        }

        public static SetAddressResponce Empty => new SetAddressResponce(false);
    }
}