using System;
using System.Collections.Generic;
using System.Text;

namespace MoibleApp.Models
{
    public class User
    {
        public long? ID { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Nullable<int> Age { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<long> RoleID { get; set; }
    }
}
