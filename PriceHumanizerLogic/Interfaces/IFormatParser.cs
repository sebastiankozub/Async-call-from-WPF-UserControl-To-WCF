namespace Interviews.VM.PriceHumanizer.Logic
{
    public interface IFormatParser
    {
        CurrencyData Parse(string currencyValue);
        bool CheckFormat(string currencyValue);
    }

    public struct CurrencyData
    {
        public ushort millions;
        public ushort thousands;
        public ushort dollars;
        public ushort decimals;
        public bool decimalExists;
    }
}