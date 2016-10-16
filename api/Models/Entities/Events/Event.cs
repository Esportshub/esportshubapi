using System;

namespace Models.Entities 
{
    public class Event 
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public Guid EventGuid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}