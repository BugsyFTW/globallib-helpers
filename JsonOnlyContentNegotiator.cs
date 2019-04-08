using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace GlobalLib.Helpers
{
    /// <summary>
    /// Para fazer override do mecanismos de content negotiation.
    /// Desta forma só responde com JSON e evita todas as outras validações standard da plataforma
    /// 
    /// Origem: http://www.strathweb.com/2013/06/supporting-only-json-in-asp-net-web-api-the-right-way/
    /// 
    /// Adicionado tratamento para nulos aqui também
    /// Adaptado de http://stackoverflow.com/questions/23282514/web-api-not-converting-json-empty-strings-values-to-null
    /// 
    /// </summary>
    public class JsonOnlyContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonOnlyContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            // to convert properties to lowercase in json because in C# we use uppercase
            //  _jsonFormatter.SerializerSettings.ContractResolver = new SnakeCaseContractResolver(); //new Newtonsoft.Json.Serialization. CamelCasePropertyNamesContractResolver();

            // to convert empty strings in json to null.
            _jsonFormatter.SerializerSettings.Converters.Add(new JsonEmptyToNullConverter());

            var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));

            return result;
        }
    }
}