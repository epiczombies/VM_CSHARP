// hString::Va (%lx -> DWORD -> tostring   2 * 4 = 8 and not 6)
// DWORD -> tostring -> X16??!?!??
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VM
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Registers
    {
        [FieldOffset(0)]
        public int EAX;

        [FieldOffset(4)]
        public int EBX;

        [FieldOffset(8)]
        public int ECX;

        [FieldOffset(12)]
        public int EDX;

        [FieldOffset(16)]
        public int ESI;

        [FieldOffset(20)]
        public int EDI;

        [FieldOffset(24)]
        public int EBP;

        [FieldOffset(28)]
        public int ESP;

        [FieldOffset(32)]
        public int EIP;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Log.CreateConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RegisterView());
            //Application.Run(new VM.ClassType.ClassMethod());
        }
    }
}