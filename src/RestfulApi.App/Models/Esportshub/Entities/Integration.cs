using System;
using System.ComponentModel.DataAnnotations.Schema;
using RestfulApi.App.Extensions.Entities;
using RestfulApi.App.Models.Esportshub.Builders.IntegrationBuilders;

namespace RestfulApi.App.Models.Esportshub.Entities
{
    public class Integration : IEsportshubEntity
    {
        private Integration() { }

        public static IntegrationBuilder Builder()
        {
            return new IntegrationBuilder(new Integration());
        }

        public int IntegrationId { get; private set; }

        public Player Player { get; set; }

        public Guid IntegrationGuid { get; set; }

        public SocialMedia SocialMedia { get; set; }

        public int SocialMediaForeignKey { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }

        public int Id => IntegrationId;

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