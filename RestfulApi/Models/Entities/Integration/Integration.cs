using System;

namespace Models.Entities {
    public class Integration 
    {
        public int IntegrationId { get; set; }
        public Guid IntegrationGuid { get; set; }
        public Name Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Player Player { get; set; }
    }
}