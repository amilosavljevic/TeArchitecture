namespace TeArchitecture.Domain
{
    public interface IWallet
    {
        long Money { get; }
        long Tokens { get; }
    }

    public class Wallet : IWallet
    {
        public long Money { get; set; }

        public long Tokens { get; set; }
    }
}
