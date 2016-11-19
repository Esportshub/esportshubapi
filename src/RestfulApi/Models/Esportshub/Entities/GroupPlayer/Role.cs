using System;

namespace EsportshubApi.Models.Entities {

    public class Role 
    {
        public int RoleId { get; set; }
        public Permission Permission { get; set; }
        public Guid RoleGuid { get; set; }
        public  DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}