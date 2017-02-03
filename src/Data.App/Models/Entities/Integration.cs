using System;
using System.ComponentModel.DataAnnotations.Schema;
using Data.App.Extensions.Entities;
using Data.App.Models.Builders.IntegrationBuilders;

namespace Data.App.Models.Entities
{
    public class Integration : IEsportshubEntity
    {
        private Integration() { }

        public static IntegrationBuilder Builder()
        {
            return new IntegrationBuilder(new Integration());
        }

        public int IntegrationId { get; set; }

        public Player Player { get; set; }

        public Guid IntegrationGuid { get; set; }

        public SocialMedia SocialMedia { get; set; }

        public int SocialMediaForeignKey { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }

        [NotMapped]
        public int Id => IntegrationId;

        [NotMapped]
        public Guid Guid => IntegrationGuid;

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