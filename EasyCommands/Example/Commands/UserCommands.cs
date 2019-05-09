using System;
using EasyCommands;

namespace Example.Commands
{
    class UserCommands : CommandCallbacks<User>
    {
        [Command("myname")]
        [CommandDocumentation("Returns your name.")]
        public void MyName()
        {
            Console.WriteLine($"Your name is {Sender.Name}.");
        }
        
        [Command("favorite-food")]
        [CommandDocumentation("Gets or sets the favorite food of a user.")]
        public void FavoriteFood(User querying, string food = null)
        {
            if(food == null)
            {
                // Get food
                Console.WriteLine($"{querying.Name}'s favorite food is {querying.FavoriteFood}.");
            }
            else
            {
                // Set food
                querying.FavoriteFood = food;
                Console.WriteLine($"{querying.Name}'s favorite food was set to {food}.");
            }
        }

        [Command]
        [CommandDocumentation("Creates a new user.")]
        public void AddUser(
            string name,
            [AllowSpaces]
            string favoriteFood)
        {
            if(UserDatabase.GetUserByName(name) != null)
            {
                Fail($"User {name} already exists!");
            }
            UserDatabase.AddUser(name, favoriteFood);
            Console.WriteLine($"Added user {name} with favorite food {favoriteFood}.");
        }
    }
}
