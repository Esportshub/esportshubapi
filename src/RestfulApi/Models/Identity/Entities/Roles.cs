using System.Collections.Generic;

namespace EsportshubApi.Models.Entities
{
    public class Roles
    {
        private static readonly string[] roles = { "Admin" };

        public static string Admin { get { return roles[0]; } }

        public static IEnumerable<string> All { get { return roles; } }
    }
}