using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        public RabbitMqController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [Route("[action]/{message}")]
        [HttpGet]
        public IActionResult SendMessage(string message)
        {
            _rabbitMqService.SendMessage(message);

            return Ok("Код отправлен");
        }
    }
}
