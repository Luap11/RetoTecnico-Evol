using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetoTecnico_EVOL.Helpers;
using RetoTecnico_EVOL.Models;
using RetoTecnico_EVOL.Services.Interfaces;
using RetoTecnico_EVOL.Services.Services;
using RetoTecnico_EVOL.ViewModels;

namespace RetoTecnico_EVOL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvolController : ControllerBase
    {
        private readonly IEvolService _evolService;
        private readonly IConfiguration _config;

        public EvolController(IEvolService evolService, IConfiguration config)
        {
            _evolService = evolService;
            _config = config;

        }
        /// <summary>
        /// Obtiene el JWT para loguearse posteriormente por el ID del usuario en GoRest.
        /// </summary>
        /// <param name="id">El id del usuario del GoRest</param>
        /// <returns></returns>
        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser(Int64 id)
        {
            try
            {
                var user = await _evolService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Unauthorized("User not found");
                }
                var newUser = new User();

                newUser.GoRestId = user.id;
                newUser.Status = user.status;
                newUser.Name = user.name;
                newUser.Email = user.email;
                newUser.Gender = user.gender;

                await _evolService.SaveUser(newUser);

                var token = JwtConfigurator.GetToken(newUser, _config);

                return Ok(token);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpPost("GetExchangueRate")]
        public async Task<IActionResult> GetExchangueRate(ExchangeRatesViewModel exchangeRates)
        {
            try
            {
                var userId = User.FindFirst("UserId").Value;
                var userName = User.FindFirst("UserName").Value;
                var exchangeRatesResponse = await _evolService.GetExchangeRatesAsync(exchangeRates);

                var newExchangeRate = new ExchangeRates();

                newExchangeRate.InitialAmount = exchangeRates.InitialAmount;
                newExchangeRate.InitialCurrency = exchangeRates.InitialCurrency;
                newExchangeRate.FinalCurrency = exchangeRates.FinalCurrency;
                newExchangeRate.FinalAmount = exchangeRates.InitialAmount * exchangeRatesResponse.conversion_rate;
                newExchangeRate.UserId = Guid.Parse(userId);
                newExchangeRate.CreationDate = DateTime.Now;

                var exchange = await _evolService.SaveExchangueRate(newExchangeRate);

                var result = new ExchangeRatesResultViewModel();
                result.InitialCurrency = exchange.InitialCurrency;
                result.FinalCurrency = exchange.FinalCurrency;
                result.FinalAmount = exchange.FinalAmount;
                result.InitialAmount = exchange.InitialAmount;
                result.UserName = userName;
                result.CreationDate = DateTime.Now;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpPost("GetExchangeRatesList")]
        public IActionResult GetExchangeRatesList()
        {
            try
            {
                var result = _evolService.GetExchangeRatesList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("GetUserList")]
        public IActionResult GetUserList()
        {
            try
            {
                var result = _evolService.GetUserList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
