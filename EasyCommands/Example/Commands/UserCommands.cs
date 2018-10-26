using System;
using EasyCommands;

namespace Example.Commands
{
    class UserCommands
    {
        [ExampleCommand(
            CommandName = "myname",
            Documentation = "Returns your name.")]
        void MyName(User sender)
        {
            Console.WriteLine($"Your name is {sender.Name}.");
        }

        [ExampleCommand(
            CommandName = "favorite-food",
            Documentation = "Gets the favorite food of a user.")]
        void GetFavoriteFood(User sender, User querying)
        {
            Console.WriteLine($"{querying.Name}'s favorite food is {querying.FavoriteFood}.");
        }

        [ExampleCommand(
            CommandName = "favorite-food",
            Documentation = "Sets the favorite food of a user.")]
        //TODO: documentation resolution for duplicate command hooks
        void SetFavoriteFood(User sender, User querying, string food)
        {
            querying.FavoriteFood = food;
            Console.WriteLine($"{querying.Name}'s favorite food was set to {food}.");
        }

        [ExampleCommand(
            CommandName = "add-user",
            Documentation = "Creates a new user.")]
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
