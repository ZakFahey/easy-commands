using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    public abstract class CommandDelegate<TSender>
    {
        protected TextOptions textOptions;
        protected ArgumentParser<TSender> parser;

        public string Name { get; private set; }

        public abstract void Invoke(TSender sender, string args);
        public abstract string SyntaxDocumentation();

        public CommandDelegate(TextOptions options, ArgumentParser<TSender> parser, string name)
        {
            textOptions = options;
            this.parser = parser;
            Name = name;
        }
    }
}
