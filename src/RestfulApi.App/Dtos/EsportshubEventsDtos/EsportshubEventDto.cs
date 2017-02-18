using System;

namespace RestfulApi.App.Dtos.EsportshubEventsDtos
{
    public class EsportshubEventDto
    {

        public Guid EsportshubEventGuid { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}