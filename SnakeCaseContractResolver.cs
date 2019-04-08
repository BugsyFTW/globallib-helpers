using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GlobalLib.Helpers
{
    public class SnakeCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            var result = Regex.Replace(propertyName, "(?<=[a-z0-9])[A-Z]", m => "_" + m.Value);
            result = result.ToLowerInvariant();

            return result;
        }

    }
}