using System;
using System.IO;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json.Linq;
using Telephony.VritualNumberService.Entities.VirtualNumber;

namespace Telephony.VritualNumberService.ModelBinder
{
    public class VirtualNumberModelBinder : IModelBinder
    {
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration,
            params string[] blackList)
        {
            using (var streamReader = new StreamReader(context.Request.Body))
            {
                var jsonBody = JObject.Parse(streamReader.ReadToEnd());

                JToken purposeId;
                JToken providerId;
                JToken virtualNumber;

                jsonBody.TryGetValue("purposeId", out purposeId);
                jsonBody.TryGetValue("providerId", out providerId);
                jsonBody.TryGetValue("virtualPhoneNumber", out virtualNumber);

                if (purposeId == null
                    || providerId == null
                    || virtualNumber == null)
                {
                    throw new ArgumentException();    
                }

                var phoneNumber = virtualNumber.ToObject<PhoneNumber>();

                return new VirtualNumber
                {
                    PurposeId = purposeId.ToObject<int>(),
                    ProviderId = providerId.ToObject<int>(),
                    VirtualPhoneNumber = phoneNumber
                };
            }
        }

        public bool CanBind(Type modelType)
        {
            return typeof (VirtualNumber) == modelType;
        }
    }
}