using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    abstract class CommandDelegate<TSender>
    {
        protected TextOptions textOptions;
        protected ArgumentParser parser;

        public string Name { get; private set; }

        public abstract void Invoke(TSender sender, IEnumerable<string> args);

        public void Invoke(TSender sender, string args)
        {
            Invoke(sender, args.Split(' '));
        }

        public CommandDelegate(TextOptions options, ArgumentParser parser, string name)
        {
            textOptions = options;
            this.parser = parser;
            Name = name;
        }
    }
}
