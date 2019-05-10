using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    /// <summary>
    /// Mock user database to demonstrate parsing irregular types
    /// </summary>
    public static class UserDatabase
    {
        private static List<User> users;

        static UserDatabase()
        {
            Reset();
        }

        public static void Reset()
        {
            users = new List<User>
            {
                new User("Admin", "lasagna", PermissionLevel.Admin),
                new User("Jeff", "steak", PermissionLevel.DefaultUser),
                new User("Cameron", "Oreos", PermissionLevel.DefaultUser),
                new User("Jessica", "tacos", PermissionLevel.Guest),
                new User("Blake", "oranges", PermissionLevel.DefaultUser),
                new User("Henry", "haggis", PermissionLevel.Guest)
            };
        }

        public static void DeleteProduction()
        {
            users = new List<User>();
        }

        public static User GetUserByName(string name)
        {
            return users.FirstOrDefault(u => u.Name == name);
        }

        public static void AddUser(string name, string favoriteFood, PermissionLevel permissionLevel)
        {
            users.Add(new User(name, favoriteFood, permissionLevel));
        }
    }
}
