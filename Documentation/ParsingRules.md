# Parsing rules

Parsing rules are classes that specify the behavior for a command argument. They include functions for different argument types. You take a string or string array (the user's input) as input, and the function outputs the specified type. Take [ExampleParsingRules.cs](https://github.com/ZakFahey/easy-commands/blob/master/EasyCommands/Example/ExampleParsingRules.cs) as an example:

```
class ExampleParsingRules<TSender> : ParsingRules<TSender>
{
    [ParseRule]
    public User ParseUser(string arg)
    {
        User user = UserDatabase.GetUserByName(arg);
        if(user == null)
        {
            Fail($"User {arg} not found.", false);
        }
        return user;
    }

    [ParseRule]
    public User[] ParseUsers(string[] args)
    {
        return args.Select(ParseUser).ToArray();
    }

    [ParseRule]
    public PermissionLevel ParsePermissionLevel(string arg)
    {
        PermissionLevel level;
        if(!Enum.TryParse(arg, out level))
        {
            Fail($"{arg} is not a permission level. Valid values: {string.Join(", ", Enum.GetNames(typeof(PermissionLevel)))}", false);
        }
        return level;
    }

    [ParseRule]
    public int ParseIntAsHex(string arg, ReadAsHex attributeOverride)
    {
        int ret = 0;
        if(!int.TryParse(arg, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ret))
        {
            Fail("Invalid syntax! {0} must be a whole number!");
        }
        return ret;
    }
```

`ParseUser` allows a command to have a parameter of type `User`, and the handler finds that user and ensures the user exists.

`ParseIntAsHex` is a special case. Notice the `ReadAsHex attributeOverride` argument. You can optionally include an attribute type as a second argument. Include this attribute on your parameter, and the parser will use this handler instead of the default one. This is useful if you have multiple behaviors for a single type and can also be used for input validation.

In the `Fail` method, you can use `{0}` to substitute for the parameter name. The second argument specifies whether to show the proper syntax for the command.

This class also contains the `CommandRepository`, `ParameterName`, `ProperSyntax`, and `TextOptions` members, which you can use.

To register these parsing rules, simply do so in the [handler](CommandHandler.md) class (preferably in the `Initialize` method) with the `AddParsingRules(type or namespace)` method. When instantiating your handler, do not forget to add `EasyCommands.Defaults.DefaultParsingRules`, in addition to your own. This class has handlers for string, int, double, float, and bool.
