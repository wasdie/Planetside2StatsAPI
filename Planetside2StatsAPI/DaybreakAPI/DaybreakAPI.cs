using Planetside2StatsAPI.Models;

namespace Planetside2StatsAPI.DaybreakAPI
{
    public static class DaybreakAPI
    {
        /// <summary>
        /// This is the base URL used to connect with the DaybreakAPI
        /// </summary>
        private const String URL = "http://census.daybreakgames.com/s:mercstats/get/ps2:v2/";


        static HttpClient client = new HttpClient();


        static DaybreakAPI()
        {
            client.BaseAddress = new Uri(URL);
        }

        public static PS2Player GetPlayerAsync(string name)
        {
            PS2Player player = null;
            HttpResponseMessage response = client.GetAsync("character/?name.first_lower=" + name).Result;

            if (response.IsSuccessStatusCode)
            {
                player = TranslateAPI(response);
            }

            return player;
        }

        private static PS2Player TranslateAPI(HttpResponseMessage response)
        {
            PS2Player player = null;

            PS2PlayerList list = PS2PlayerList.FromJson(response.Content.ReadAsStringAsync().Result);

            if (list.PS2Players.Length > 0)
            {
                player = list.PS2Players[0];
            }

            return player;
        }
    }
}
