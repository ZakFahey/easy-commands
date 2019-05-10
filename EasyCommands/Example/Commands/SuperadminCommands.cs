using System;
using EasyCommands;

namespace Example.Commands
{
    class SuperadminCommands : CommandCallbacks<User>
    {
        [Command("superadmin-me")]
        [CommandDocumentation("Gives your account superadmin access. All you need is the super secret password, which definitely isn't 'hunter2'.")]
        [AccessLevel(PermissionLevel.Admin)]
        public void SuperAdminMe(string superSecretPassword)
        {
            if(superSecretPassword == "hunter2")
            {
                Console.WriteLine("You have successfully elevated your account to Superadmin.");
                Sender.PermissionLevel = PermissionLevel.Superadmin;
            }
            else
            {
                Fail("Incorrect password!");
            }
        }

        [Command("delete-production")]
        [CommandDocumentation("Why would you want to do this!?")]
        [AccessLevel(PermissionLevel.Superadmin)]
        public void DeleteProduction()
        {
            Console.WriteLine("Congratulations! You deleted production!");
            UserDatabase.DeleteProduction();
        }

        [Command("supersecret")]
        [CommandDocumentation("A or B.")]
        [AccessLevel(PermissionLevel.Superadmin)]
        public class SuperSecret: CommandCallbacks<User>
        {
            [SubCommand]
            [CommandDocumentation("A.")]
            public void A()
            {
                Console.WriteLine("A");
            }

            [SubCommand]
            [CommandDocumentation("B.")]
            public void B()
            {
                Console.WriteLine("B");
            }
        }
    }
}