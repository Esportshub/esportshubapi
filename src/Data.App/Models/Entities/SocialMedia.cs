using System;

namespace Data.App.Models.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }

        public Guid SocialMediaGuid { get; set; }

        public Integration Integration { get; set; }

        public string Name { get; set; }

    }
}