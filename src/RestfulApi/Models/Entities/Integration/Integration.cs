using System;
using EsportshubApi.Extensions;

namespace EsportshubApi.Models.Entities
{
    public class Integration
    {
        private Integration() { }

        public static IntegrationBuilder Builder()
        {
            return new IntegrationBuilder(new Integration());
        }
        public int IntegrationId { get; set; }
        public Guid IntegrationGuid { get; set; }
        public Name Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Player Player { get; set; }



        public override bool Equals(object obj)
        {
            Integration integration = (Integration)obj;

            if (this.CompareEntities(obj))
                return integration.IntegrationId == this.IntegrationId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}