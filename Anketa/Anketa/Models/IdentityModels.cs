using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Anketa.Models;

namespace Anketa.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    
        public virtual UserProfileInfo UserProfileInfo { get; set; }
    }

    public class UserProfileInfo
    {
        public int Id{ get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SurveyContext", throwIfV1Schema: false)
            // DefaultConnection ime referencijskog stringa unutar web.config
        {
        }

        public System.Data.Entity.DbSet<UserProfileInfo> UserProfileInfo {get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}