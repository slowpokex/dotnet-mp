namespace SpringNetDemo.Services
{
    using System.Collections.Generic;
    using global::SpringNetDemo.Entity;

    public interface IUserService
    {
        IEnumerable<User> GetAdmins();
    }
}