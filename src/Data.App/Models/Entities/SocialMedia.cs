using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.App.Models.Entities
{
    public class SocialMedia : IEsportshubEntity
    {
        public int SocialMediaId { get; set; }
        public Guid SocialMediaGuid { get; set; }

        public List<Integration> Integrations { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public int Id => SocialMediaId;

        [NotMapped]
        public Guid Guid => SocialMediaGuid;
    }
}