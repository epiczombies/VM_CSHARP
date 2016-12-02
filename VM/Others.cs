using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 32-Bits
    /// </summary>
    public enum x32Register
    {
        EAX = 0,
        ECX = 1,
        EDX = 2,
        EBX = 3,
        ESP = 4,
        EBP = 5,
        ESI = 6,
        EDI = 7,
    };

    /// <summary>
    /// 16-Bits
    /// </summary>
    public enum x16Register
    {
        AX = 0,
        CX = 1,
        DX = 2,
        BX = 3,
        SP = 4,
        BP = 5,
        SI = 6,
        DI = 7,
    };

    /// <summary>
    /// 8-Bits
    /// </summary>
    public enum cRegister
    {
        AL = 0,
        CL = 1,
        DL = 2,
        BL = 3,
        AH = 4,
        CH = 5,
        DH = 6,
        BH = 7,
    };
    class Utils
    {
        /// <summary>
        /// Returns the register name
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string RegisterToString(x32Register r)
        {
            switch (r)
            {
                case x32Register.EAX:
                return "EAX";
                case x32Register.ECX:
                return "ECX";
                case x32Register.EDX:
                return "EDX";
                case x32Register.EBX:
                return "EBX";
                case x32Register.ESP:
                return "ESP";
                case x32Register.EBP:
                return "EBP";
                case x32Register.ESI:
                return "ESI";
                case x32Register.EDI:
                return "EDI";
                default:
                return "none";
            }
        }
        /// <summary>
        /// Returns the register name
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string RegisterToString(x16Register r)
        {
            switch (r)
            {
                case x16Register.AX:
                return "AX";
                case x16Register.CX:
                return "CX";
                case x16Register.DX:
                return "DX";
                case x16Register.BX:
                return "BX";
                case x16Register.SP:
                return "SP";
                case x16Register.BP:
                return "BP";
                case x16Register.SI:
                return "SI";
                case x16Register.DI:
                return "DI";
                default:
                return "none";
            }
        }
        /// <summary>
        /// Returns the register name
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string RegisterToString(cRegister r)
        {
            switch (r)
            {
                case cRegister.AL:
                return "AL";
                case cRegister.CL:
                return "CL";
                case cRegister.DL:
                return "DL";
                case cRegister.BL:
                return "BL";
                case cRegister.AH:
                return "AH";
                case cRegister.CH:
                return "CH";
                case cRegister.DH:
                return "DH";
                case cRegister.BH:
                return "BH";
                default:
                return "none";
            }
        }



        /// <summary>
        /// get the Byte Value of a register
        /// and for this code here we should die a shame for vs that i cant add  ((byte)r) + aditional  a fucking shame
        /// </summary>
        /// <param name="r"></param>
        /// <param name="aditional"></param>
        /// <returns></returns>
        public static byte RegisterToByte(cRegister r, byte aditional)
        {
            return (byte)((byte)r + aditional);
        }
        /// <summary>
        /// get the Byte Value of a register
        /// and for this code here we should die a shame for vs that i cant add  ((byte)r) + aditional  a fucking shame
        /// </summary>
        /// <param name="r"></param>
        /// <param name="aditional"></param>
        /// <returns></returns>
        public static byte RegisterToByte(x16Register r, byte aditional)
        {
            return (byte)((byte)r + aditional);
        }
        /// <summary>
        /// get the Byte Value of a register
        /// and for this code here we should die a shame for vs that i cant add  ((byte)r) + aditional  a fucking shame
        /// </summary>
        /// <param name="r"></param>
        /// <param name="aditional"></param>
        /// <returns></returns>
        public static byte RegisterToByte(x32Register r, byte aditional)
        {
            return (byte)((byte)r + aditional);
        }




        /// <summary>
        /// Calculate the byte that contains one register
        /// </summary>
        /// <param name="StartByte"></param>
        /// <param name="Register"></param>
        /// <returns></returns>
        public static byte CalculateRegister(byte StartByte, x32Register Register)
        {
            return (byte)(StartByte + Register);
        }

        /// <summary>
        /// Calculate the byte that contains 2 register
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte CalculateRegister(byte StartByte, x32Register c, x32Register t)
        {
            return CalculateRegister(c, CalculateRegister(StartByte, t));
        }

        /// <summary>
        /// Calculate the byte from Register + startbyte
        /// </summary>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte CalculateRegister(x32Register r, byte s)
        {
            return (byte)((Enum.GetNames(typeof(x32Register)).Length * (byte)r) + s);
        }


        /// <summary>
        /// Convert a Byte to the register with a start byte or no start byte.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static x32Register ByteToRegister(byte s, byte b)
        {
            return (x32Register)(s - b);
        }
    }
}