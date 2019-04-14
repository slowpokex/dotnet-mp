namespace SpringNetDemo.Services
{
    using global::SpringNetDemo.Repositories;
    using SpringNet.Attributes;

    [Component]
    public class UserService
    {
        [Autowired]
        public UserRepository UserRepository;
    }
}
