namespace RetoTecnico_EVOL.ViewModels
{
    public class ExchangeRatesViewModel
    {
        public decimal InitialAmount { get; set; }
        public string InitialCurrency { get; set; }
        public string FinalCurrency { get; set; }
    }
    public class ExchangeRatesResponseViewModel
    {
        public decimal conversion_rate { get; set; }
    }
    public class ExchangeRatesResultViewModel: ExchangeRatesViewModel
    {
        public decimal FinalAmount { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
