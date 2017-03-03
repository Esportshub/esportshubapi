using Newtonsoft.Json.Serialization;

namespace RestfulApi.App.Dtos.JsonApiDocumentDtos
{
    public class Links
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("related")]
        public string Related { get; set; }

    }
}