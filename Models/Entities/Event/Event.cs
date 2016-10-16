using System;
using System.Collections.Generic;

namespace Models.Entities {

    public class Event {
                public int id { get; set; }
                public string Name { get; set; }
                public Guid Guid { get; set; }
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
                public DateTime Created { get; set; }
                public DateTime Updated { get; set; }
                public List<Team> Teams { get; set; }
                public List<Group> Groups { get; set; }
                public int MyProperty { get; set; }
    }
}