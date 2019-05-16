# Easy Commands

Easy Commands is a C# library that lets you easily write a command-line UI. This is particularly useful for use cases such as modded game servers; chat bots on Discord, Twitch, etc.; and any other place where you'd want to convert a text command to an action. This project is also extendable enough to allow custom permission systems, custom command arguments, and anything else you'd want to use.

To install, simply go to Visual Studio's Package Manager Console and type `Install-Package easy-commands`.

To specify a command, all you have to do is implement the abstract `CommandHandler` class and write out your command callbacks with this syntax:
```
[Command("add")]
// You can call this with `add 1 2`
public void Add(int num1, int num2)
{
    Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
}

// You can parse any object type you want, as long as you define the behavior for it.
// You can call this with `permission-level John`
[Command("permission-level")]
public void GetPermissionLevel(User user)
{
    Console.WriteLine($"{user.Name} has the permission level of {user.PermissionLevel}.");
}
```
Essentially, the arguments of your method specify the arguments of the command. It also supports optional parameters, subcommands, and much more. To read more about usage, visit the [documentation](/Documentation). The Example project also demonstrates all of this library's functionality.
