/*
 * RNGPlaysVG.cs 
 * Author: ipavl <https://github.com/ipavl/RNGPlaysVG>
 */

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace RNGPlaysVG
{
    internal static class AutoItX3Declarations
    {
        //AU3_API void WINAPI AU3_Send(LPCWSTR szSendText, long nMode);
        [DllImport("AutoItX3.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static public extern void AU3_Send([MarshalAs(UnmanagedType.LPWStr)] string SendText, int Mode);

        //AU3_API long WINAPI AU3_Shutdown(long nFlags);
        [DllImport("AutoItX3.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static public extern int AU3_Shutdown(int Flags);

        //AU3_API void WINAPI AU3_Sleep(long nMilliseconds);
        [DllImport("AutoItX3.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static public extern void AU3_Sleep(int Milliseconds);
    }

    class RNGPlaysVG
    {
        [DllImport("USER32.DLL")]
        static extern int SetForegroundWindow(IntPtr ptr);
        enum KeyBank { UP, DOWN, LEFT, RIGHT, A, B, START, SELECT }

        static void Main(string[] args)
        {
            Console.Title = "RNGPlaysVG <https://github.com/ipavl/RNGPlaysVG>";

            int inputDelay = 0;
            Process proc = null;
            String procName = null;
            bool attachedToProcess = false;

            while (!attachedToProcess)
            {
                Console.Write("Attach to which process? ");
                procName = Console.ReadLine();

                try
                {
                    proc = Process.GetProcessesByName(procName).FirstOrDefault();
                    Console.Title = "RNGPlaysVG (attached to " + proc.ProcessName + " (" + proc.Id + "))";
                    Console.WriteLine("Successfully attached to process " + proc.ProcessName + " (" + proc.Id + ").");
                    attachedToProcess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not attach to process " + procName + ": " + ex.Message);
                }
            }

            try
            {
                Console.Write("Delay between inputs (ms): ");
                inputDelay = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Input delay is not valid: " + inputDelay + ", defaulting to 100.");
                inputDelay = 100;
            }

            if (proc != null)
            {
                IntPtr handle = proc.MainWindowHandle;
                SetForegroundWindow(handle);

                while (proc != null)
                {
                    var rng = new Random();
                    int key = rng.Next(8);

                    if (key == (int)KeyBank.UP)
                    {
                        Console.WriteLine("rng=" + key + " => UP");
                        AutoItX3Declarations.AU3_Send("{UP}", 0);
                        //SendKeys.SendWait("{UP}");
                    }
                    else if (key == (int)KeyBank.DOWN)
                    {
                        Console.WriteLine("rng=" + key + " => DOWN");
                        AutoItX3Declarations.AU3_Send("{DOWN}", 0);
                        //SendKeys.SendWait("{DOWN}");
                    }
                    else if (key == (int)KeyBank.LEFT)
                    {
                        Console.WriteLine("rng=" + key + " => LEFT");
                        AutoItX3Declarations.AU3_Send("{LEFT}", 0);
                        //SendKeys.SendWait("{RIGHT}");
                    }
                    else if (key == (int)KeyBank.RIGHT)
                    {
                        Console.WriteLine("rng=" + key + " => RIGHT");
                        AutoItX3Declarations.AU3_Send("{RIGHT}", 0);
                        //SendKeys.SendWait("{RIGHT}");
                    }
                    else if (key == (int)KeyBank.A)
                    {
                        Console.WriteLine("rng=" + key + " => A");
                        AutoItX3Declarations.AU3_Send("{Z}", 0);
                        //SendKeys.SendWait("Z");
                    }
                    else if (key == (int)KeyBank.B)
                    {
                        Console.WriteLine("rng=" + key + " => B");
                        AutoItX3Declarations.AU3_Send("{X}", 0);
                        //SendKeys.SendWait("X");
                    }
                    else if (key == (int)KeyBank.START)
                    {
                        var startRNG = new Random();
                        int startThrottle = rng.Next(100);

                        if (startThrottle > 90)
                        {
                            Console.WriteLine("rng=" + key + " => START");
                            AutoItX3Declarations.AU3_Send("{ENTER}", 0);
                        }
                        //SendKeys.SendWait("{ENTER}");
                    }
                    /*else if (key == (int)KeyBank.SELECT)
                    {
                        Console.WriteLine("rng=" + key + " => SELECT");
                        AutoItX3Declarations.AU3_Send("{BACKSPACE}", 0);
                        //SendKeys.SendWait("{BKSP}");
                    }*/

                    AutoItX3Declarations.AU3_Sleep(inputDelay);
                }
            }

            Console.ReadLine();
            AutoItX3Declarations.AU3_Shutdown(0);
        }
    }
}
