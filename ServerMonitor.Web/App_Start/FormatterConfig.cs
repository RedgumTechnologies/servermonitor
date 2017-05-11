using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using Newtonsoft.Json;
using ServerMonitor.Web.Formatters;


namespace ServerMonitor.Web
{
    public class FormatterConfig
    {
        public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            //System.Diagnostics.Debug.WriteLine("Registering Json Converters...");
            //dynamic lFormatter = (JsonMediaTypeFormatter)formatters.Where(x => x.SupportedMediaTypes.Any(t => t.MediaType.Equals("application/json", StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();

            //if (lFormatter != null)
            //{
            //    JsonSerializerSettings lFormatterSettings = new JsonSerializerSettings();
            //    lFormatterSettings.NullValueHandling = NullValueHandling.Ignore;
            //    lFormatterSettings.Converters = ModelFormatters.GetAllFormatters();
            //    System.Diagnostics.Debug.WriteLine("{0} converters bound to serializer", lFormatterSettings.Converters.Count);
            //    lFormatter.SerializerSettings = lFormatterSettings;
            //}
        }
    }
}