using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Test
{
    class PrintFactory
    {
        public const short FILE_ATTRIBUTE_NORMAL = 0x80;
        public const short INVALID_HANDLE_VALUE = -1;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint CREATE_NEW = 1;
        public const uint CREATE_ALWAYS = 2;
        public const uint OPEN_EXISTING = 3;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess,
            uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition,
            uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        public static void SendCMDToLPT1(String receiptText)
        {
            IntPtr ptr = CreateFile("LPT1", GENERIC_WRITE, 0,
                     IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            if (ptr.ToInt32() == -1)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            else
            {
                FileStream lpt = new FileStream(ptr, FileAccess.ReadWrite);
                Byte[] buffer = System.Text.Encoding.Default.GetBytes(receiptText);
                lpt.Write(buffer, 0, buffer.Length);
                lpt.Close();
            }
        }
    }
}
