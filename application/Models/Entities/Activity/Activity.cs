using System;

namespace Models.Entities 
{
    public class Activity 
    {
        public int ActivityId { get; set; }
        public Guid  ActivityGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Player Player { get; set; }
    }
}