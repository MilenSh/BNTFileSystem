using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BussinessLayer
{
    public class User : IdentityUser
    {
        [Required, DisplayName("First name")]
        public string FirstName { get; set; }

        [Required, DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Biography { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
