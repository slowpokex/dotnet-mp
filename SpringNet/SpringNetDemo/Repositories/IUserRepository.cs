namespace SpringNetDemo.Repositories
{
    using System.Collections.Generic;
    using global::SpringNetDemo.Entity;

    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
    }
}