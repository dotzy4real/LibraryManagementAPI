using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Entity
{
    public class OAuthAuthentication
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string Scope { get; set; }
        public required string GrantType { get; set; }
        public bool Status { get; set; }
    }
}
