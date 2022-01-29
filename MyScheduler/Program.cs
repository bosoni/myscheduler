using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MyScheduler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MyScheduler v0.4 by mjt 2022");
            if (args.Length < 3)
            {
                Console.WriteLine("params:  startTime  endTime  program  parameters");
                Console.WriteLine("ie  MyScheduler.exe 21:00 22:00 program.exe hoi");
                return;
            }

            string all = "";
            for (int q = 0; q < args.Length; q++)
            {
                Console.WriteLine("param " + q + ": " + args[q]);
                all += args[q];
            }

            if (all.Contains(":") == false)
            {
                Console.WriteLine("Use : with times, ie  21:00 22:00");
                return;
            }

            string startTime = args[0];
            int sh = Int32.Parse(startTime.Substring(0, startTime.IndexOf(':')));
            int sm = Int32.Parse(startTime.Substring(startTime.IndexOf(':') + 1));
            //Console.WriteLine("starttime int>>> " + sh + "  ::  " + sm);
            string endTime = args[1];

            int eh = Int32.Parse(endTime.Substring(0, endTime.IndexOf(':')));
            int em = Int32.Parse(endTime.Substring(endTime.IndexOf(':') + 1));

            Console.WriteLine("scheduled program: " + args[2]);
            Console.Write("  with parameters:\n");
            string parms = "";
            for (int q = 3; q < args.Length; q++)
            {
                Console.Write(" " + args[q]);
                parms += " " + args[q];
            }
            //Console.WriteLine("::" + parms);

            Console.WriteLine("at time " + sh + ":" + sm);

            /*Console.WriteLine("-----------------------");
            Console.WriteLine("time now: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            Console.WriteLine("-----------------------");*/

            Console.WriteLine("\nwaiting...");

            bool started = false;
            while (true)
            {
                Thread.Sleep(1000);
                Console.Write(".");

                if (started == false)
                {
                    if (DateTime.Now.Hour == sh)
                    {
                        if (DateTime.Now.Minute == sm)
                        {
                            Console.WriteLine("start " + args[2]);
                            started = true;

                            try
                            {
                                Process process = new Process();
                                process.StartInfo.FileName = args[2];
                                process.StartInfo.Arguments = parms;
                                process.Start();
                                //process.WaitForExit();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                //Console.ReadKey();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (DateTime.Now.Hour == eh)
                        if (DateTime.Now.Minute == em)
                        {
                            Console.WriteLine("end " + args[2]);
                            started = false;

                            try
                            {
                                Process process = new Process();
                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                                {
                                    process.StartInfo.FileName = "taskkill";
                                    process.StartInfo.Arguments = "/im " + args[2];
                                }
                                else
                                {
                                    process.StartInfo.FileName = "pkill";
                                    process.StartInfo.Arguments = args[2];
                                }
                                process.Start();
                                //process.WaitForExit();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                //Console.ReadKey();
                                return;
                            }

                        }
                }
            }
        }
    }
}
