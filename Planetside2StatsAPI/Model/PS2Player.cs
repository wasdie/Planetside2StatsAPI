namespace Planetside2StatsAPI.Models
{
    using System;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PS2PlayerList
    {
        [JsonProperty("character_list")]
        public PS2Player[] PS2Players { get; set; }

        [JsonProperty("returned")]
        public long Returned { get; set; }
    }

    public partial class PS2Player
    {
       [JsonProperty("character_id")]
        public string CharacterId { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("faction_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FactionId { get; set; }

        [JsonProperty("head_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long HeadId { get; set; }

        [JsonProperty("title_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TitleId { get; set; }

        [JsonProperty("times")]
        public Times Times { get; set; }

        [JsonProperty("certs")]
        public Certs Certs { get; set; }

        [JsonProperty("battle_rank")]
        public BattleRank BattleRank { get; set; }

        [JsonProperty("profile_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ProfileId { get; set; }

        [JsonProperty("daily_ribbon")]
        public DailyRibbon DailyRibbon { get; set; }

        [JsonProperty("prestige_level")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PrestigeLevel { get; set; }
    }

    public partial class BattleRank
    {
        [JsonProperty("percent_to_next")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PercentToNext { get; set; }

        [JsonProperty("value")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Value { get; set; }
    }

    public partial class Certs
    {
        [JsonProperty("earned_points")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long EarnedPoints { get; set; }

        [JsonProperty("gifted_points")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long GiftedPoints { get; set; }

        [JsonProperty("spent_points")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SpentPoints { get; set; }

        [JsonProperty("available_points")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AvailablePoints { get; set; }

        [JsonProperty("percent_to_next")]
        public string PercentToNext { get; set; }
    }

    public partial class DailyRibbon
    {
        [JsonProperty("count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Count { get; set; }

        [JsonProperty("time")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Time { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
    }

    public partial class Name
    {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("first_lower")]
        public string FirstLower { get; set; }
    }

    public partial class Times
    {
        [JsonProperty("creation")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Creation { get; set; }

        [JsonProperty("creation_date")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonProperty("last_save")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long LastSave { get; set; }

        [JsonProperty("last_save_date")]
        public DateTimeOffset LastSaveDate { get; set; }

        [JsonProperty("last_login")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long LastLogin { get; set; }

        [JsonProperty("last_login_date")]
        public DateTimeOffset LastLoginDate { get; set; }

        [JsonProperty("login_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long LoginCount { get; set; }

        [JsonProperty("minutes_played")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MinutesPlayed { get; set; }
    }

    public partial class PS2PlayerList
    {
        public static PS2PlayerList FromJson(string json) => JsonConvert.DeserializeObject<PS2PlayerList>(json, Planetside2StatsAPI.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this PS2PlayerList self) => JsonConvert.SerializeObject(self, Planetside2StatsAPI.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
