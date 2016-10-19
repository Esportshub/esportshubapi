using System;
using System.Collections.Generic;
using EsportshubApi.Extensions;
using EsportshubApi.Models.Entites;

namespace EsportshubApi.Models.Entities
{
    public class Group
    {

        private Group() { }

        public static GroupBuilder Build()
        {
            return new GroupBuilder(new Group());
        }

        public int GroupId { get; set; }

        public Guid GroupGuid { get; set; }

        public string Name { get; set; }
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public Visibilty Visibilty { get; set; }
        public List<Role> Roles { get; set; }
        public List<Player> Players { get; set; }
        public List<Event> Events { get; set; }

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