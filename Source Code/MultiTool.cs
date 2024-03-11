using System;
using System.ComponentModel;
using System.IO;
using Cosmos.Core.IOGroup;
using Cosmos.HAL;
using Cosmos.System;
using static System.Console;
using Sys = Cosmos.System;
using static MultiToolCommander.Kernel;

namespace MultiToolCommander
{
    public static class MultiTool
    {
        public static string Version = "1.9.24";
        public static string Title = $"MultiTool [alpha {Version}]";

        public static bool isInstalled = false;

        public static string CurrentPath = @"0:\";

        public static void Start()
        {
            WriteLine($"{Title} {TestIn(isInstalled)}\r\n(c) Amongasik's studio.\r\n");
        }

        public static void Boot()
        {

        }

        public static string TestIn(bool iss)
        {
            if (!iss) { return "test"; }
            else { return ""; }
        }

        public static void Setup()
        {
            WriteLine($"Setup file system...");
            FileSystem = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FileSystem);
            WriteLine($"File system register ({new DriveInfo("0:\\").DriveFormat})\r\n");

            WriteLine($"Setup disks...");
            FileSystem.Disks.ForEach(disk =>
            {
                disk.DisplayInformation();
            });
            WriteLine($"Disks installed!\r\n");

            WriteLine($"PS2 setup...");
            WriteLine($"Keyboard setup: NL:{KeyboardManager.NumLock}");
            WriteLine($"PS2 installed!\r\n");

            isInstalled = true;
            WriteLine($"MultiTool (v{Version}) installed!");
            ReadKey();
            Clear();
            
            MultiTool.Start();
        }

        /// <summary>
        /// cd, create, delete, edit, format, help, ls, ls/d, read, reboot, shutdown, type, visual
        /// </summary>

        #region functions 0_0

        public static string Input(string? str)
        {
            Write(str); return ReadLine();
        }

        public static void SetColor(ConsoleColor clr, char typ = 'f')
        {
            switch (typ)
            {
                case 'f':
                    ForegroundColor = clr;
                    break;
                case 'b':
                    BackgroundColor = clr;
                    break;
                case 'a':
                    ForegroundColor = clr;
                    BackgroundColor = clr;
                    break;
                default:
                    ForegroundColor = clr;
                    break;
            }
        }
        #endregion

        public static void ExecuteTest(string str)
        {
            string command = str.Split(' ')[0];
            string args = "";

            for (int i = command.Length + 1; i < str.Length; i++)
            {
                args += str[i];
            }

            switch (command)
            {
                case "cd":
                    WriteLine("You haven't installed MultiTool. Type \"Install\" to install it");
                    break;
                case "create":
                    WriteLine("You haven't installed MultiTool. Type \"Install\" to install it");
                    break;
                case "cmd":
                    WriteLine($"{Title}\r\n(c) Amongasik's studio.\r\n");
                    break;
                case "clear" or "cls":
                    Clear();
                    WriteLine();
                    break;
                case "mouse":
                    uint x_ = 0;
                    uint y_ = 0;
                    Clear();
                    SetCursorPosition(0, 0);
                    WriteLine($"{0} | {0}");
                    CursorVisible = false;
                    while (true)
                    {
                        uint x = MouseManager.X;
                        uint y = MouseManager.Y;
                        WriteLine($"{x} | {y}");
                        x_ = x; y_ = y;
                    }
                    break;
                case "delete" or "del":
                    WriteLine("You haven't installed MultiTool. Type \"Install\" to install it");
                    break;
                case "edit":
                    WriteLine("You haven't installed MultiTool. Type \"Install\" to install it");
                    break;
                case "format" or "fr":
                    WriteLine("You haven't installed MultiTool. Type \"Install\" to install it");
                    break;
                case "help":
                    WriteLine("Supported command:");
                    SetColor(ConsoleColor.Red); WriteLine("  CD (X)\r\n  CREATE (X)\r\n  DELETE (X)\r\n  EDIT (X)\r\n  FORMAT (X)\r\n  HELP\r\n  LS (X)\r\n  LS/D\r\n  READ (X)\r\n  REBOOT\r\n  SHUTDOWN\r\n\r\n  INSTALL"); ResetColor();
                    break;
                case "dir" or "ls":
                    WriteLine("You haven't installed MultiTool. Type \"Install\" to install it");
                    break;
                case "ls/d":
                    FileSystem.Disks.ForEach(disk =>
                    {
                        disk.DisplayInformation();
                        WriteLine("\r\n");
                    });
                    break;
                case "read" or "rd":
                    WriteLine("You haven't installed MultiTool. Type \"Install\" to install it");
                    break;
                case "beep":
                    Beep(49, 5);
                    break;
                case "reboot":
                    Cosmos.System.Power.Reboot();
                    break;
                case "shutdown":
                    Cosmos.System.Power.Shutdown();
                    break;
                case "install":
                    WriteLine($"Do you want to install MultiTool (v{MultiTool.Version})");
                    switch (Input("[Y / N] ").ToLower())
                    {
                        case "y" or "yes":
                            Setup();
                            break;
                        case "n" or "no":
                            WriteLine("You missed the installation");
                            ReadKey(true);
                            break;
                    }
                    break;
                default:
                    SetColor(ConsoleColor.Red); WriteLine($"Wrong command \"{command}\"!"); ResetColor();
                    break;
            }
        }

        public static void Execute(string str)
        {
            string command = str.Split(' ')[0];
            string args = "";

            for (int i = command.Length + 1; i < str.Length; i++)
            {
                args += str[i];
            }

            switch (command)
            {
                case "cd":
                    if (args != ".." && args != ".")
                    {
                        if (Directory.Exists(Path.TrimEndingDirectorySeparator(args)))
                        {
                            CurrentPath = Path.TrimEndingDirectorySeparator(args);
                        }
                        else if (Directory.Exists(Path.Combine(CurrentPath, args)))
                        {
                            CurrentPath = Path.Combine(CurrentPath, args);
                        }
                        else
                        {
                            SetColor(ConsoleColor.Red); WriteLine($"Unkown directory \"{args}\"!"); ResetColor();
                        }
                    }
                    else if(args == ".." && args == ".")
                    {
                        if(Directory.Exists(new DirectoryInfo(CurrentPath).Parent.FullName))
                        {
                            CurrentPath = new DirectoryInfo(CurrentPath).Parent.FullName;
                        }
                        else
                        {
                            SetColor(ConsoleColor.Red); WriteLine($"Unkown directory \"{args}\"!"); ResetColor();
                        }
                    }
                    break;
                case "create":
                    if (!File.Exists(Path.Combine(CurrentPath, args)))
                    {
                        File.Create(Path.Combine(CurrentPath, args)).Close();
                    }
                    break;
                case "cmd":
                    WriteLine($"{Title}\r\n(c) Amongasik's studio.\r\n");
                    break;
                case "clear" or "cls":
                    Clear();
                    WriteLine();
                    break;
                case "delete" or "del":
                    if (File.Exists(args))
                    {
                        File.Delete(args);
                    }
                    else if (File.Exists(Path.Combine(CurrentPath, args)))
                    {
                        File.Delete(Path.Combine(CurrentPath, args));
                    }
                    break;
                case "edit":
                    if (File.Exists(args))
                    {
                        File.WriteAllText(args, Input($"Write: "));
                    }
                    else if (File.Exists(Path.Combine(CurrentPath, args)))
                    {
                        File.WriteAllText(Path.Combine(CurrentPath, args), Input($"Write: "));
                    }
                    break;
                case "format" or "fr":
                    FileSystem.Disks.ForEach(disk =>
                    {
                        
                    });
                    break;
                case "help":
                    WriteLine("Supported command:");
                    SetColor(ConsoleColor.Red); WriteLine("  CD\r\n  CREATE\r\n  DELETE\r\n  EDIT\r\n  FORMAT\r\n  HELP\r\n  LS\r\n  LS/D\r\n  READ\r\n  REBOOT\r\n  SHUTDOWN"); ResetColor();
                    break;
                case "dir" or "ls":
                    foreach (var o in new DirectoryInfo(CurrentPath).GetDirectories())
                    {
                        SetColor(ConsoleColor.Yellow); WriteLine($"    <DIR>          {o.Name}"); ResetColor();
                    }
                    foreach (var o in new DirectoryInfo(CurrentPath).GetFiles())
                    {
                        SetColor(ConsoleColor.White); WriteLine($"                   {o.Name}"); ResetColor();
                    }
                    break;
                case "ls/d":
                    FileSystem.Disks.ForEach(disk =>
                    {
                        disk.DisplayInformation();
                        WriteLine("\r\n");
                    });
                    break;
                case "read" or "rd":
                    if (File.Exists(args))
                    {
                        WriteLine(File.ReadAllText(args));
                    }
                    else if (File.Exists(Path.Combine(CurrentPath, args)))
                    {
                        WriteLine(File.ReadAllText(Path.Combine(CurrentPath, args)));
                    }
                    break;
                case "beep":
                    Beep(49, 5);
                    break;
                case "mouse":
                    uint x_ = 0;
                    uint y_ = 0;
                    Clear();
                    SetCursorPosition(0, 0);
                    WriteLine($"{0} | {0}");
                    CursorVisible = false;
                    while (true)
                    {
                        uint x = MouseManager.X;
                        uint y = MouseManager.Y;
                        WriteLine($"{x} | {y}");
                        x_ = x; y_ = y;
                    }
                    break;
                case "reboot":
                    Cosmos.System.Power.Reboot();
                    break;
                case "shutdown":
                    Cosmos.System.Power.Shutdown();
                    break;
                default:
                    SetColor(ConsoleColor.Red); WriteLine($"Wrong command \"{command}\"!"); ResetColor();
                    break;
            }
        }  
    }
}
