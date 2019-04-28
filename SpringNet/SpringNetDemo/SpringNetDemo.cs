namespace SpringNetDemo
{
    using System;
    using global::SpringNetDemo.Entity;
    using global::SpringNetDemo.Services;
    using SpringNet;

    public class SpringNetDemo
    {
        public static void Main(string[] args)
        {
            var context = new ApplicationContext();
            context.Initialize();

            var userService = context.GetBean<UserService>("UserService");

            foreach (var user in userService.GetAdmins())
            {
                Console.WriteLine(user.Login);
            }

            var mainUser = context.GetBean<User>("MainUser");

            Console.WriteLine("Main User is {0}", mainUser.Login);

            Console.ReadKey();
        }
    }
}
