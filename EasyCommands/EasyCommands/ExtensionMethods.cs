﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyCommands
{
    static class ExtensionMethods
    {
        /// <summary>
        /// Converts a string from CamelCase to dash-notation. Ex: ThisIsATest => this-is-a-test
        /// </summary>
        public static string CamelCaseToDashes(this string str)
        {
            // Source: https://stackoverflow.com/a/18781533
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString().ToLower() : x.ToString().ToLower()));
        }

        /// <summary>
        /// Returns the list of names for a command/subcommand, or null if a member does not have the command attribute.
        /// If there are no command names specified, it will generate one based on the method name.
        /// </summary>
        /// <typeparam name="T">The type of command to check for, either Command or SubCommand</typeparam>
        public static string[] GetCommandNames<T>(this MemberInfo member) where T : Command
        {
            T attribute = (T)member.GetCustomAttribute(typeof(T));
            if(attribute == null || attribute.GetType() != typeof(T))
            {
                return null;
            }
            if(attribute.Names.Length == 0)
            {
                return new[] { member.Name.CamelCaseToDashes() };
            }
            return attribute.Names;
        }

        /// <summary>
        /// Return the <see cref="SubCommand.IsDefault"/> attribute of a <see cref="SubCommand"/>
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool GetSubCommandIsDefault(this MemberInfo member)
        {
            var attribute = (SubCommand)member.GetCustomAttribute(typeof(SubCommand));
            if (attribute == null || attribute.GetType() != typeof(SubCommand))
            {
                return false;
            }
            return attribute.IsDefault;
        }

        public static (string first, string last) SplitAfterFirstSpace(this string str)
        {
            int firstSpace = str.IndexOf(' ');
            if(firstSpace == -1)
            {
                return (str, "");
            }
            else
            {
                return (str.Substring(0, firstSpace), str.Substring(firstSpace + 1));
            }
        }
    }
}
