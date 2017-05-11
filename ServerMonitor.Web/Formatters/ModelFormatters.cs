using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ServerMonitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerMonitor.Web.Formatters
{
    public class ModelFormatters
    {

        public static IList<JsonConverter> GetAllFormatters()
        {
            List<JsonConverter> lConverters = new List<JsonConverter>();

            lConverters.Add(new ServerInfoFormatter());


            return lConverters;
        }
    }
    public class ServerInfoFormatter : CustomCreationConverter<ServerInfo>
    {

        public override ServerInfo Create(System.Type objectType)
        {
            return new ServerInfo();
        }
    }
}