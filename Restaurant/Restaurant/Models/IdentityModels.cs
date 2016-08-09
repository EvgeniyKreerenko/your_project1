using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
                
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //creating role
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            //adding role to DB
            roleManager.Create(role1);
            roleManager.Create(role2);

            //adding users
            var admin = new ApplicationUser { UserName = "Admin", Email = "Admin@gmail.com", FirstName = "Admin", 
                                              LastName = "Admin", Address = "Chehova", City = "Taganrog", Phone = "123456789" };
            string password = "Qwertyui1";
            var result = userManager.Create(admin, password);

            var testuser1 = new ApplicationUser { Email = "TestUser1@gmail.com", UserName = "TestUser1", FirstName = "User", 
                                                  LastName = "Test", Address = "Chehova", City = "Taganrog", Phone = "123456789" };
            string password1 = "123456789";
            var result1 = userManager.Create(testuser1, password1);

            var testuser2 = new ApplicationUser { Email = "TestUser2@gmail.com", UserName = "TestUser2", FirstName = "User", 
                                                  LastName = "Test", Address = "Chehova", City = "Taganrog", Phone = "123456789" };
            string password2 = "123456789";
            var result2 = userManager.Create(testuser2, password2);
            // if creation sucsessful
            if (result.Succeeded)
            {
                //adding user's role
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}