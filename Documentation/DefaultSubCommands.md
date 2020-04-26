# Default SubCommands

You can mark any `SubCommand` as the default one in a `Command`.

A default `SubCommand` will be executed whenever a invalid command is executed, see the example bellow.

There is a maximum of one default `SubCommand` in a `Command`, trying to set two or more default `SubCommand` in a `Command` will raise an exception.

Use the `SubCommandType` attribute in the `SubCommand` constructor to mark it as default. You can't set a name to a default `SubCommand`.

An example on how to use a default `SubCommand`:

```cs
[Command("default")]
[CommandDocumentation("Show how to use a default sub command.")]
class DefaultExampleCommands : CommandCallbacks<User>
{
    [SubCommand("hello")]
    [CommandDocumentation("Say hello.")]
    public void Hello([AllowSpaces] string name)
    {
        Console.WriteLine($"Hello {name}!");
    }

    [SubCommand("bye")]
    [CommandDocumentation("Say bye.")]
    public void Bye([AllowSpaces] string name)
    {
        Console.WriteLine($"Bye {name}!");
    }

    // It is executed whenever an invalid request is sent, for example: "default Eveldee" will execute the default subcommand with args "Eveldee"
    [SubCommand(SubCommandType.Default)]
    [CommandDocumentation("This is the default subcommand.")]
    public void IAmDefault(string name)
    {
        Console.WriteLine($"Have a nice day {name}!");
    }
```

If an unknown `SubCommand` is executed, it will fallback to the default one, note that all the parsing rules for arguments are still effective, here is some example requests:

```none
default hello Eveldee
> Hello Eveldee!

default bye Eveldee
> Bye Eveldee!

default Eveldee
> Have a nice day Eveldee!
```
