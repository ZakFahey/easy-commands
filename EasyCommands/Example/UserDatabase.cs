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
        private static List<User> users = new List<User>
        {
            new User("Admin", "lasagna"),
            new User("Jeff", "steak"),
            new User("Cameron", "Oreos"),
            new User("Jessica", "tacos"),
            new User("Blake", "oranges"),
            new User("Henry", "haggis")
        };

        public static User GetUserByName(string name)
        {
            return users.FirstOrDefault(u => u.Name == name);
        }

        public static void AddUser(string name, string favoriteFood)
        {
            users.Add(new User(name, favoriteFood));
        }
    }
}
