using RetoTecnico_EVOL.Models;
using RetoTecnico_EVOL.ViewModels;

namespace RetoTecnico_EVOL.Services.Interfaces
{
    public interface IEvolService
    {
        Task<UserViewModel> GetUserByIdAsync(Int64 id);
        Task<User> SaveUser(User user);
        Task<ExchangeRatesResponseViewModel> GetExchangeRatesAsync(ExchangeRatesViewModel exchangeRates);
        Task<ExchangeRates> SaveExchangueRate(ExchangeRates exchangeRates);
        List<ExchangeRatesResultViewModel> GetExchangeRatesList();
        List<UserViewModel> GetUserList();

    }

}
