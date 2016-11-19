using System;
using System.Collections.Generic;
using EsportshubApi.Extensions;
using EsportshubApi.Models.Entities.mappings;

namespace EsportshubApi.Models.Entities
{
    public class Group : EsportshubEntity
    {

        private Group() { }

        public static GroupBuilder Build()
        {
            return new GroupBuilder(new Group());
        }

        public int Id => GroupId;
        public int GroupId { get; set; }
        public Guid GroupGuid { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Role Role { get; set;  }
        public Visibilty Visibilty { get; set; }
        public List<PlayerGroups> PlayerGroups { get; set; }
        public List<GroupEvent> GroupEvents { get; set; }

        public override bool Equals(object obj)
        {
            Group group = (Group)obj;

            if (this.CompareEntities(obj))
                return group.GroupId == this.GroupId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}