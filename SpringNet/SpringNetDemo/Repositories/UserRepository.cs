namespace SpringNetDemo.Repositories
{
    using System.Collections.Generic;
    using global::SpringNetDemo.Entity;
    using SpringNet.Attributes;

    [Component]
    public class UserRepository: IUserRepository
    {
        [Autowired]
        private List<User> AllUsers;

        public IEnumerable<User> GetAllUsers()
        {
            return AllUsers;
        }
    }
}
