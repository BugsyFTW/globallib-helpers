﻿using Newtonsoft.Json;
using System;

namespace GlobalLib.Helpers
{
    /// <summary>
    /// Custom converter to convert empty string to null
    /// to make possible validations in optional fields with validation 
    ///  [JsonConverter(typeof(EmptyToNullConverter))]
    ///  public string FamilyName { get; set; }
    ///  
    /// Referência: http://stackoverflow.com/questions/23282514/web-api-not-converting-json-empty-strings-values-to-null
    /// 
    /// </summary>
    public class JsonEmptyToNullConverter : JsonConverter
    {
        private JsonSerializer _stringSerializer = new JsonSerializer();

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = _stringSerializer.Deserialize<string>(reader);
            if (string.IsNullOrEmpty(value))
            {
                value = null;
            }
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            _stringSerializer.Serialize(writer, value);
        }
    }
}