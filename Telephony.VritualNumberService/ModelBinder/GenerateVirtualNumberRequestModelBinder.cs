using System;
using System.IO;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json.Linq;
using Telephony.VritualNumberService.Modules.VirtualNumbers;
using Telephony.VritualNumberService.Utilities;

namespace Telephony.VritualNumberService.ModelBinder
{
    public class GenerateVirtualNumberRequestModelBinder : IModelBinder
    {
        private readonly IVirtualNumberRequestParser _virtualNumberRequestParser;

        public GenerateVirtualNumberRequestModelBinder()
        {
            _virtualNumberRequestParser = new VirtualNumberRequestParser();
        }

        public object Bind(
            NancyContext context, 
            Type modelType, 
            object instance, 
            BindingConfig configuration,
            params string[] blackList)
        {
            using (var streamReader = new StreamReader(context.Request.Body))
            {
                return _virtualNumberRequestParser.ParseRequest(
                    JObject.Parse(streamReader.ReadToEnd()));
            }
        }

        public bool CanBind(Type modelType)
        {
            return typeof (VirtualNumberRequest) == modelType;
        }
    }
}