# Flags

In addition to basic arguments, you can create arguments that take many unordered, optional parameters in the form of flags. All you need to do is make a flags class and register it with `CommandHandler.AddFlagRule(flags)` or `CommandHandler.AddParsingRules(namespace)`. Keep in mind that registration order matters, and you must register flags after you register the parsing rules for the fields the flags class contains. Looking at [ExampleFlags.cs](https://github.com/ZakFahey/easy-commands/blob/master/EasyCommands/Example/ExampleFlags.cs), we have our sample flags class:

```
[FlagsArgument]
public class ExampleFlags
{
    [FlagParams("-a")]
    public string A = "DEFAULT";

    [FlagParams("-b")]
    [ReadAsHex] // Attribute overrides work here
    public int B = 0;

    [FlagParams("-c", "-C")] // Aliases are supported
    public User C = null;

    [FlagParams("--d")]
    public bool D = false;
}
```

Use the `[FlagsArgument]` attribute to specify a class to be used as flags and the `[FlagParams]` attribute to specify the fields of a class to be used as argument names. You would enter commands with this using the syntax `command -a test -b 10`, `command --d`, `command` (no flags set), or `command -C Jim -b 5`, just to give examples. Boolean values do not require you to specify the argument value; including the parameter simply sets the value to true.
