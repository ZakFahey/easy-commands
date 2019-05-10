using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class User
    {
        public string Name;
        public string FavoriteFood;
        public PermissionLevel PermissionLevel;

        public User(string name, string favoriteFood, PermissionLevel permissionLevel)
        {
            Name = name;
            FavoriteFood = favoriteFood;
            PermissionLevel = permissionLevel;
        }
    }
}
