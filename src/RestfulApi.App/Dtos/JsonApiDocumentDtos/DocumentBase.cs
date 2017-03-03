using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestfulApi.App.Dtos.JsonApiDocumentDtos
{
    public class DocumentBase
    {
        [JsonProperty("links")]
        public RootLinks Links { get; set; }

        [JsonProperty("included")]
        public List<DocumentData> Included { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, object> Meta { get; set; }

        public bool ShouldSerializeIncluded()
        {
            return (Included != null);
        }

        public bool ShouldSerializeMeta()
        {
            return (Meta != null);
        }

        public bool ShouldSerializeLinks()
        {
            return (Links != null);
        }
    }
}