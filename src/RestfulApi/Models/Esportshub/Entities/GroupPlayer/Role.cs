using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulApi.Models.Esportshub.Entities.GroupPlayer {

    public class Role 
    {
        public int RoleId { get; private set; }

        public Permission Permission { get; set; }

        public Guid RoleGuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public  DateTime Created { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }
    }
}