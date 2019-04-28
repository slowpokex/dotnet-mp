namespace SpringNetDemo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using global::SpringNetDemo.Entity;
    using global::SpringNetDemo.Repositories;
    using SpringNet.Attributes;

    [Component]
    public class UserService: IUserService
    {
        [Autowired]
        private UserRepository UserRepository;

        public IEnumerable<User> GetAdmins()
        {
            return UserRepository.GetAllUsers()
                .Where(x => x.UserRole == UserRole.ADMIN);
        }
    }
}
