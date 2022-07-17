using Microsoft.AspNetCore.Mvc;
using Planetside2StatsAPI.Models;
using Planetside2StatsAPI.Services;

namespace Planetside2StatsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Planetside2GetPlayerController : ControllerBase
    {
        private IDayBreakAPIAccess _daybreakAPI;

        public Planetside2GetPlayerController(IDayBreakAPIAccess daybreakAPI)
        {
            _daybreakAPI = daybreakAPI;
        }

        [HttpGet(Name = "GetPlayer")]
        public PS2PlayerList GetPlayer(string playerName)
        {
            return _daybreakAPI.GetPlayer(playerName);
        }

        //[HttpGet(Name = "GetPlayerAsync")]
        //public async Task<PS2PlayerList> GetPlayerAsync(string playername)
        //{
        //    var player = await _daybreakAPI.GetPlayerAsync(playername);

        //    if (player == null)
        //    {
        //        throw new ArgumentException();
        //    }

        //    return player;
        //}
    }
}
