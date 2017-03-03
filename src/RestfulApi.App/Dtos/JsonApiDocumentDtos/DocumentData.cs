using System.Collections.Generic;
using Newtonsoft.Json;
using Humanizer;

namespace RestfulApi.App.Dtos.JsonApiDocumentDtos
{
    public class DocumentData
    {
        private string _type;

        [JsonProperty("type")]
        public string Type
        {
            get { return _type.Camelize(); }
            set { _type = value; }
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public Dictionary<string, object> Attributes { get; set; }

        [JsonProperty("relationships")]
        public Dictionary<string, RelationshipData> Relationships { get; set; }
    }
}