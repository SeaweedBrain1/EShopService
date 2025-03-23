using EShop.Application.Services;
using EShop.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly CardValidatorService _cardValidatorService;

        public CreditCardController(CardValidatorService cardValidatorService)
        {
            _cardValidatorService = cardValidatorService;
        }

        [HttpPost("validate")]
        public IActionResult ValidateCard([FromBody] string cardNumber)
        {
            try
            {
                var isValid = _cardValidatorService.ValidateCard(cardNumber);
                var cardType = _cardValidatorService.GetCardType(cardNumber);
                return Ok(new { isValid, cardType });
            }
            catch (CardNumberTooLongException)
            {
                return StatusCode(414, "Card number is too long.");
            }
            catch (CardNumberTooShortException)
            {
                return BadRequest("Card number is too short.");
            }
            catch (CardNumberInvalidException)
            {
                return BadRequest("Card number is invalid.");
            }
            catch (Exception)
            {
                return StatusCode(406, "Card type not supported.");
            }
        }
    }
}