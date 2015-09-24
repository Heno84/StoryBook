using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StoryBook.Models
{
    public class IdentityModels
    {
        public class ApplicationUser : IdentityUser
        {
        }

        public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
            public ApplicationDbContext()
            {
            }
        }
    }
}