using System;
using System.ComponentModel.DataAnnotations.Schema;
using EsportshubApi.Models.Entities;
using RestfulApi.Extensions.Entities;

namespace RestfulApi.Models.Esportshub.Entities.Integration
{
    public class Integration : EsportshubEntity
    {
        private Integration() { }

        public static IntegrationBuilder Builder()
        {
            return new IntegrationBuilder(new Integration());
        }

        public int IntegrationId { get; private set; }

        public Guid IntegrationGuid { get; set; }

        public Name Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }

        public Player Player { get; set; }

        public int Id
        {
            get
            {
                return IntegrationId;
            }
        }

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