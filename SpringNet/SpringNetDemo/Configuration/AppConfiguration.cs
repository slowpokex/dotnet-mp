namespace SpringNetDemo.Configuration
{
    using System.Collections.Generic;
    using global::SpringNetDemo.Entity;
    using global::SpringNetDemo.Repositories;
    using SpringNet.Attributes;

    [Configuration]
    public class AppConfiguration
    {
        [Bean("MainUser")]
        public User GetMainUser()
        {
            return new User {
                Login = "ROOT",
                UserRole = UserRole.SUPER_ADMIN
            };
        }

        [Bean("AllUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            return new List<User>
            {
                new User {
                    Login = "harley26",
                    UserRole = UserRole.ADMIN
                },
                new User {
                    Login = "valakas",
                    UserRole = UserRole.ADMIN
                },
                new User {
                    Login = "lotysh",
                    UserRole = UserRole.MODERATOR
                },
                new User {
                    Login = "veracsich",
                    UserRole = UserRole.GUEST
                },
                new User {
                    Login = "valera",
                    UserRole = UserRole.GUEST
                }
            };
        }
    }
}
