using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SvNaum.Web.Models
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
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(ConfigurationManager.AppSettings.Get("SQLSERVER_CONNECTION_STRING") ??
                //"DefaultConnection", throwIfV1Schema: false)
                 "Server=bb2264f3-721c-4f43-b889-a4ad00b89cf3.sqlserver.sequelizer.com;Database=dbbb2264f3721c4f43b889a4ad00b89cf3;User ID=ntbjuekuftsjyche;Password=a4UTh4sX7cM8hyUtXYcF37mv2SioRiqGHKDsWqhLAmyTFANVayEhcSqbQxZwTdgX;", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}