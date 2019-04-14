namespace SpringNetDemo.Configuration
{
    using global::SpringNetDemo.Entity;
    using global::SpringNetDemo.Repositories;
    using SpringNet.Attributes;

    [Configuration]
    public class AppConfiguration
    {
        [Bean("mainUser")]
        public User GetMainUser()
        {
            return new User();
        }

        [Bean("userRepository")]
        public UserRepository GetUserRepository()
        {
            return new UserRepository();
        }
    }
}
