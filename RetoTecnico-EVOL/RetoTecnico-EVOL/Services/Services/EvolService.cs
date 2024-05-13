using Microsoft.EntityFrameworkCore;
using RetoTecnico_EVOL.Models;
using RetoTecnico_EVOL.Models.Context;
using RetoTecnico_EVOL.Services.Interfaces;
using RetoTecnico_EVOL.ViewModels;
using System;
using System.Net.Http;
using System.Text.Json;

namespace RetoTecnico_EVOL.Services.Services
{
    public class EvolService : IEvolService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly EvolDbContext _evolDbContext;

        public EvolService(IHttpClientFactory httpClientFactory, EvolDbContext evolDbContext)
        {
            _httpClientFactory = httpClientFactory;
            _evolDbContext = evolDbContext;
        }
        public async Task<UserViewModel> GetUserByIdAsync(Int64 id)
        {
            var httpClient = _httpClientFactory.CreateClient("GoRestClient");

            var response = await httpClient.GetAsync($"users/{id}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw new Exception("Failed to retrieve user.");
            }
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<UserViewModel>(resultString);

            return result;
        }
        public async Task<User> SaveUser(User user)
        {
            var result = await _evolDbContext.AddAsync(user);
            _evolDbContext.SaveChanges();
            return result.Entity;
        }
        public async Task<ExchangeRatesResponseViewModel> GetExchangeRatesAsync(ExchangeRatesViewModel exchangeRates) {

            var httpClient = _httpClientFactory.CreateClient("ExchangeRate");
            var response = await httpClient.GetAsync("pair/" + exchangeRates.InitialCurrency.ToString() + "/" + exchangeRates.FinalCurrency.ToString());
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ExchangeRatesResponseViewModel>(jsonResponse);

            return result;
        }
        public async Task<ExchangeRates> SaveExchangueRate(ExchangeRates exchangeRates)
        {
            var result = await _evolDbContext.ExchangeRates.AddAsync(exchangeRates);
            _evolDbContext.SaveChanges();
            return result.Entity;
        }
        public List<ExchangeRatesResultViewModel> GetExchangeRatesList()
        {
            var result = _evolDbContext.ExchangeRates.Include(x=>x.User).Select(x => new ExchangeRatesResultViewModel
            {
                FinalAmount = x.FinalAmount,
                FinalCurrency = x.FinalCurrency,
                InitialAmount = x.InitialAmount,
                InitialCurrency = x.InitialCurrency,
                UserName = x.User.Name,
                CreationDate = x.CreationDate,
            }).ToList();
            return result.ToList();
        }
        public List<UserViewModel> GetUserList()
        {
            var result = _evolDbContext.User.Select(x => new UserViewModel
            {
                id = x.GoRestId,
                email = x.Email,
                name = x.Name,
                status = x.Status,
                gender = x.Gender,
            }).ToList();

            return result.ToList();
        }
    }
}
