using Planetside2StatsAPI.Models;

namespace Planetside2StatsAPI.Services
{
    public interface IDayBreakAPIAccess
    {
        public PS2PlayerList GetPlayer(string name);
        public Task<PS2PlayerList> GetPlayerAsync(string name);

    }
    public class DaybreakAPIAccess : IDayBreakAPIAccess
    {
        /// <summary>
        /// This is the base URL used to connect with the DaybreakAPI
        /// </summary>
        private const String URL = "http://census.daybreakgames.com/s:mercstats/get/ps2:v2/";

        private HttpClient client = new HttpClient();

        public DaybreakAPIAccess()
        {
            client.BaseAddress = new Uri(URL);
        }

        public PS2PlayerList GetPlayer(string name)
        {
            PS2PlayerList player = null;
            HttpResponseMessage response = client.GetAsync("character/?name.first_lower=" + name).Result;

            if (response.IsSuccessStatusCode)
            {
                player = TranslateAPI(response);
            }

            return player;
        }

        private PS2PlayerList TranslateAPI(HttpResponseMessage response)
        {
            PS2PlayerList player = null;

            PS2PlayerList list = PS2PlayerList.FromJson(response.Content.ReadAsStringAsync().Result);

            if (list.PS2Players.Length > 0)
            {
                player = list.PS2Players[0];
            }

            return player;
        }

        public async Task<PS2PlayerList> GetPlayerAsync(string name)
        {
            PS2PlayerList player = null;
            HttpResponseMessage response = await client.GetAsync("character/?name.first_lower=" + name);

            if (response.IsSuccessStatusCode)
            {
                player = TranslateAPI(response);
            }

            return player;
        }

        private async Task<PS2PlayerList> TranslateAPIAsync(HttpResponseMessage response)
        {
            PS2PlayerList player = null;

            PS2PlayerList list = PS2PlayerList.FromJson(await response.Content.ReadAsStringAsync());

            if (list.PS2Players.Length > 0)
            {
                player = list.PS2Players[0];
            }

            return player;
        }
    }
}
