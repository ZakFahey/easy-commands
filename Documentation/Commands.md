# Command callbacks

To run any commands, you'll have to specify what they are. To see a working example of command callbacks, check the [Example project](https://github.com/ZakFahey/easy-commands/tree/master/EasyCommands/Example/Commands).

Simply create a class that inherits from `CommandCallbacks<your sender type>`. In that class, you can have methods for any of your command callbacks. Decorate them with the `[Command]` attribute. This attribute takes the arguments of the command name and its aliases. If you don't specify a command name, it will auto-generate one based on the method name. Be sure that all methods and classes within this callback are public.

You can also add any other attribute to a command that inherits from the `CustomAttribute` class, and then you can use `command.GetCustomAttribute<AttributeType>()` in your [command handler's `PreCheck` method](CommandHandler.md) to specify custom behavior such as documentation or access management. The example project utilizes this feature for those purposes.

The arguments of your method specify the arguments of your command. You can use the `[ParamName]` attribute to specify that the name of the parameter in any error messages is overridden. You can also use C#'s default parameters in your callbacks, and the command handler will make those arguments optional.

You can also use the `[AllowSpaces]` attribute to let an argument have spaces without the need for a user to use quotes. To prevent ambiguity, however, you can only use one per command.

You can add any other attribute to a parameter as well, and then you can use attribute overrides in your [argument parsers](ParsingRules.md) to specify custom behavior and validation for a certain parameter.

To show an error, use the `Fail` method in your callback to throw an exception and send an error message to the user.

You have access to the `Sender` object, which specifies who sent the command. You also have access to `TextOptions`, `CommandRepository`, and `RawCommandText`.

If you want subcommands, all you need to do is put the `[Command]` attribute on your class and use the `[Subcommand]` attribute for your subcommands. See [WindowCommands.cs](https://github.com/ZakFahey/easy-commands/blob/master/EasyCommands/Example/Commands/WindowCommands.cs) You can also include a public subcommand class within your command handler class. See [this example](https://github.com/ZakFahey/easy-commands/blob/master/EasyCommands/Test/Commands/SubcommandTest3.cs).

To register these commands into your handler, simply run `CommandHandler.RegisterCommands(type or namespace)`.
