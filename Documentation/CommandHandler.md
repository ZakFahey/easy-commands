# CommandHandler class

In order to parse commands, you need to implement the abstract `CommandHandler` class. This class specifies the behavior of the command parser. Looking at [ExampleCommandHandler.cs](https://github.com/ZakFahey/easy-commands/blob/master/EasyCommands/Example/ExampleCommandHandler.cs):

```cs
public class ExampleCommandHandler : CommandHandler<User>
{
    public ExampleCommandHandler() : base() { }
    public ExampleCommandHandler(TextOptions options) : base(options) { }

    protected override void SendFailMessage(User sender, string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach(string line in message.Split('\n'))
        {
            Console.WriteLine(line);
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected override void Initialize()
    {
        AddParsingRules(typeof(DefaultParsingRules<User>));
        AddParsingRules(typeof(ExampleParsingRules<User>));
    }

    public override void PreCheck(User sender, CommandDelegate<User> command)
    {
        AccessLevel permLevel = command.GetCustomAttribute<AccessLevel>();
        if(permLevel != null && sender.PermissionLevel < permLevel.MinimumLevel)
        {
            Fail($"Command \"{command.Name}\" does not exist.");
        }
    }

    public override bool CanSeeCommand(User sender, CommandDelegate<User> command)
    {
        AccessLevel permLevel = command.GetCustomAttribute<AccessLevel>();
        return permLevel == null || sender.PermissionLevel >= permLevel.MinimumLevel;
    }
}
```

You inherit from `EasyCommands.CommandHandler<T>`, where T is the entity that is sending the message. You'll need to include the base constructors as well.

The `SendFailMessage` method specifies the behavior when a command fails. In this case, we write the error in red to the console.

The `Initialize` method runs when the handler is created. Here we use it to specify behavior for how arguments are parsed (see [parsing rules](ParsingRules.md)) with the `AddParsingRules` method.

The `PreCheck` method is run before any command or subcommand is executed. Here it is used to check for the `PermissionLevel` attribute on a command and stop execution with `Fail` if the user does not have permission to run the command. This permissions system is not implemented natively in the library - it is specified in the example project, meaning that if your permissions system is slightly different, you can handle it however you want. For instance, you could have separate command permissions for moderators and users at a certain donation tier, and those could both be separate attributes. You could specify that a command is runnable only at a certain time, or whatever else you want.

The `CanSeeCommand` method returns whether the user has permission to see/use the command. This ensures that users can't see commands they can't use in help documentation.

There is also the `CommandList` property in this class, which lists all commands that have been registered.

There is the overrideable `CommandRepositoryToUse`, which specifies how commands are stored. There is a default command storage engine, but if you wanted to, say, write a wrapper on top of an existing command system, you could use this override and make a new `CommandRepository` class. In addition to this, you have the `HandleCommandException` override, which can be used to handle what happens when an unhandled exception is thrown in a command handler. By default, it logs the exception to the console.

To use this handler, look at [Program.cs](https://github.com/ZakFahey/easy-commands/blob/master/EasyCommands/Example/Program.cs):

```cs
var commandHandler = new ExampleCommandHandler();
commandHandler.RegisterCommands("Example.Commands");
Console.WriteLine("Input a command. Type `help` to see a list of commands.");
while(true)
{
    _ = commandHandler.RunCommandAsync(CurrentUser, Console.ReadLine());
}
```

You just instantiate the class, register the class or namespace with your commands, and run your commands with either `commandHandler.RunCommand` or `commandHandler.RunCommandAsync`.
