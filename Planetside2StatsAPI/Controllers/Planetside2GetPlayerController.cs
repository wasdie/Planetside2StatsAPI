using Microsoft.AspNetCore.Mvc;
using Planetside2StatsAPI.Models;

namespace Planetside2StatsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Planetside2GetPlayerController : ControllerBase
    {

        private readonly ILogger<Planetside2GetPlayerController> _logger;

        public Planetside2GetPlayerController(ILogger<Planetside2GetPlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPlayer")]
        public PS2Player Get()
        {
            return DaybreakAPI.DaybreakAPI.GetPlayerAsync("braxor");
        }
    }
}
