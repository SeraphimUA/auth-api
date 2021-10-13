using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth2.Api.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Role { get; set; }
    }
}
