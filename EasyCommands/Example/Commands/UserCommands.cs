using System;
using EasyCommands;

namespace Example.Commands
{
    class UserCommands
    {
        [Command("myname")]
        [CommandDocumentation("Returns your name.")]
        void MyName(User sender)
        {
            Console.WriteLine($"Your name is {sender.Name}.");
        }
        
        [Command("favorite-food")]
        [CommandDocumentation("Gets or sets the favorite food of a user.")]
        void GetFavoriteFood(User sender, User querying)
        {
            Console.WriteLine($"{querying.Name}'s favorite food is {querying.FavoriteFood}.");
        }
        
        [Command("favorite-food")]
        void SetFavoriteFood(User sender, User querying, string food)
        {
            querying.FavoriteFood = food;
            Console.WriteLine($"{querying.Name}'s favorite food was set to {food}.");
        }
        
        [Command("add-user")]
        [CommandDocumentation("Creates a new user.")]
        void AddUser(
            User sender,
            string name,
            [AllowSpaces]
            string favoriteFood)
        {
            UserDatabase.AddUser(name, favoriteFood);
            Console.WriteLine($"Added user {name} with favorite food {favoriteFood}.");
        }
    }
}
