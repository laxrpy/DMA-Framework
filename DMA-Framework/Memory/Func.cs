using DMA_Framework.EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wrapper;

namespace DMA_Framework.Memory
{
    public class Func
    {
        public static uint _pid = 0;
        public static Vmm vmm = new Vmm("-printf", "-v", "-device", "fpga");
        private static ulong _unityBase;
        public static enums.GameStatus GameStatus = enums.GameStatus.NotFound;

        public static void StartUp()
        {

            try
            {
                while (true)
                {

                    Console.WriteLine("Finding EFT");

                    while (!GetPid() || !GetModuleBase())
                    {
                        Console.WriteLine("EFT StartupFailed...");
                        GameStatus = enums.GameStatus.NotFound;
                        Console.WriteLine("GameStatus:   "+GameStatus);
                        Thread.Sleep(15000);
                    }








                }
            }
            catch { }

        }






        private static bool GetModuleBase()
        {
            try
            {
                _unityBase = vmm.ProcessGetModuleBase(_pid, "UnityPlayer.dll");
                if (_unityBase == 0) throw new DMAException("Unable to obtain Base Module Address. Game may not be running");
                else
                {
                    Console.WriteLine($"Found UnityPlayer.dll at 0x{_unityBase.ToString("x")}");
                    return true;
                }
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR getting module base: {ex}");
                return false;
            }
        }

        private static bool GetPid()
        {

                if (!vmm.PidGetFromName("EscapeFromTarkov.exe", out _pid))
                    throw new DMAException("Unable to obtain PID. Game is not running.");
                else
                {
                    Console.WriteLine($"EscapeFromTarkov.exe is running at PID {_pid}");
                    return true;
                }

        }







    }










    #region Exceptions
    public class DMAException : Exception
    {
        public DMAException()
        {
        }

        public DMAException(string message)
            : base(message)
        {
        }

        public DMAException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class NullPtrException : Exception
    {
        public NullPtrException()
        {
        }

        public NullPtrException(string message)
            : base(message)
        {
        }

        public NullPtrException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class DMAShutdown : Exception
    {
        public DMAShutdown()
        {
        }

        public DMAShutdown(string message)
            : base(message)
        {
        }

        public DMAShutdown(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    #endregion



}
