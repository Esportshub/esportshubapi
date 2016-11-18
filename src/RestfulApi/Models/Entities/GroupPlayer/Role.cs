using System;

namespace EsportshubApi.Models.Entities {

    public class Role 
    {
        public int RoleId { get; set; }
        public Permission Permission { get; set; }
        public int GroupForeignKey { get; set; }
        public Guid RoleGuid { get; set; }
        public  DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Player Player { get; set; }
        public Group Group { get; set; }
    }
}