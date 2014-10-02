namespace AjMessages.Console
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;

    using AjMessages;
    using AjMessages.Wcf;

    class Program
    {
        static WcfServer server;

        static string[] GetCommand()
        {
            Console.Write("Enter command (help for help): ");
            string line = Console.ReadLine();
            string[] words = line.Split(' ');
            List<string> words2 = new List<string>();

            foreach (string word in words)
                if (word.Length > 0)
                    words2.Add(word);

            return words2.ToArray();
        }

        static void SendMessage(string action, string value)
        {
            Message msg;

            if (value[0] >= '0' && value[0] <= '9')
                msg = MessageUtilities.Create(action, Convert.ToInt32(value));
            else
                msg = MessageUtilities.Create(action, value);
            server.Post(msg);
        }

        static void LoadServer(string filename)
        {
            if (server == null)
            {
                server = new WcfServer();
                server.Configure(filename);
                server.Start();
                return;
            }

            StreamReader reader = File.OpenText(filename);
            string conf = reader.ReadToEnd();
            reader.Close();
            SendMessage("AjMessages/Administration/Configure", conf);
        }

        static void UnloadServer()
        {
            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }

        static void ForkProcess()
        {
            System.Diagnostics.Process.Start("AjMessages.Console.exe");
        }

        static void ShowHelp()
        {
            Console.WriteLine("Command Help:");
            Console.WriteLine("exit");
            Console.WriteLine("  stop the server and exit application");
            Console.WriteLine("load <configurationfilename>");
            Console.WriteLine("  start or reconfigure the server using a configuration file");
            Console.WriteLine("unload");
            Console.WriteLine("  stop the server");
            Console.WriteLine("fork");
            Console.WriteLine("  launch other console application like this");
            Console.WriteLine("send <actionname> <string or integer>");
            Console.WriteLine("  send a message (string or integer value) to an action");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("AjMessages.Console Demo");
            Console.WriteLine();

            string[] command;

            while (true)
            {
                try
                {
                    command = GetCommand();

                    if (command.Length == 0)
                        continue;

                    if (command[0] == "exit")
                        break;

                    switch (command[0])
                    {
                        case "load":
                            LoadServer(command[1]);
                            break;
                        case "unload":
                            UnloadServer();
                            break;
                        case "send":
                            SendMessage(command[1], command[2]);
                            break;
                        case "fork":
                            ForkProcess();
                            break;
                        default:
                            ShowHelp();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ": " + ex.StackTrace);
                }
            }

            if (server != null)
                server.Stop();
        }
    }
}
