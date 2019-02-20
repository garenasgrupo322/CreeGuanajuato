using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CreeGuanajuato.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CreeGuanajuatoUser class
    public class CreeGuanajuatoUser : IdentityUser
    {
        [PersonalData]
        public string nombre { get; set; }

        [PersonalData]
        public string apellido_paterno { get; set; }

        [PersonalData]
        public string apellido_materno { get; set; }
    }
}
