// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using MauiApp2.Model;
//
//    var courses = Courses.FromJson(jsonString);

namespace MauiApp2.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Courses
    {
        [JsonProperty("course_id")]
        public string CourseId { get; set; }

        [JsonProperty("course_name")]
        public string CourseName { get; set; }

        [JsonProperty("credits")]
        public long Credits { get; set; }
    }

    public partial class Courses
    {
        public static List<Courses> FromJson(string json) => JsonConvert.DeserializeObject<List<Courses>>(json, MauiApp2.Model.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Courses> self) => JsonConvert.SerializeObject(self, MauiApp2.Model.Converter.Settings);
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
}
