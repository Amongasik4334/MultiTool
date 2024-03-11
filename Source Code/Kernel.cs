using Cosmos.Core.IOGroup;
using System;
using static System.Console;
using Sys = Cosmos.System;
using static MultiToolCommander.MultiTool;

namespace MultiToolCommander
{
    public class Kernel : Sys.Kernel
    {
        public static Sys.FileSystem.CosmosVFS FileSystem;

        protected override void BeforeRun()
        {
            try
            {
                Clear();

                MultiTool.Boot();
                MultiTool.Start();

                /*WriteLine($"Do you want to install MultiTool (v{MultiTool.Version})");
                switch (Input("[Y / N] ").ToLower())
                {
                    case "y" or "yes":
                        Clear();
                        Setup();
                        break;
                    case "n" or "no":
                        WriteLine("You missed the installation");

                        Clear();
                        break;
                }*/
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
        }

        protected override void Run()
        {
            if(isInstalled)
            {
                try
                {
                    SetColor(ConsoleColor.Red); Write("root"); ResetColor(); Execute(Input($"-{CurrentPath}~$"));
                }
                catch (Exception ex)
                {
                    WriteLine($"{ex}\r\n{ex.Message}");
                }
            }
            else
            {
                try
                {
                    SetColor(ConsoleColor.Red); Write("root"); ResetColor(); ExecuteTest(Input($"-{CurrentPath}~$"));
                }
                catch (Exception ex)
                {
                    WriteLine($"{ex}\r\n{ex.Message}");
                }
            }
        }
    }
}
