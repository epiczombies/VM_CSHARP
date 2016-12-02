using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//  __  __           _        _             __  __ _      _     _ _____ _____  ___ ___  
// |  \/  |         | |      | |           |  \/  (_)    | |   (_) ____| ____|/ _ \__ \ 
// | \  / | __ _  __| | ___  | |__  _   _  | \  / |_  ___| |__  _| |__ | |__ | | | | ) |
// | |\/| |/ _` |/ _` |/ _ \ | '_ \| | | | | |\/| | |/ __| '_ \| |___ \|___ \| | | |/ / 
// | |  | | (_| | (_| |  __/ | |_) | |_| | | |  | | | (__| | | | |___) |___) | |_| / /_ 
// |_|  |_|\__,_|\__,_|\___| |_.__/ \__, | |_|  |_|_|\___|_| |_|_|____/|____/ \___/____|
//   ____                      _____ __/ |                                              
//  / __ \                    / ____|___/                                               
// | |  | |_ __   ___ _ __   | (___   ___  _   _ _ __ ___ ___                           
// | |  | | '_ \ / _ \ '_ \   \___ \ / _ \| | | | '__/ __/ _ \                          
// | |__| | |_) |  __/ | | |  ____) | (_) | |_| | | | (_|  __/                          
//  \____/| .__/ \___|_| |_| |_____/ \___/ \__,_|_|  \___\___|                          
//        | |                                                                           
//        |_|                                                                           

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.Globalization;
using UnknownType = System.Object;

public enum LogLevel
{
    LogLevel_None = 0,
    LogLevel_Warning = 1,
    LogLevel_Error = 2,
    LogLevel_Debug = 4,
    LogLevel_Info = 8,
    LogLevel_Trace = 16,
    LogLevel_All = 31,
};
public enum LogColor
{
    LogColor_Black = 0,
    LogColor_Blue = 1,
    LogColor_Green = 2,
    LogColor_Cyan = 3,
    LogColor_Red = 4,
    LogColor_Magenta = 5,
    LogColor_Brown = 6,
    LogColor_LightGrey = 7,
    LogColor_DarkGrey = 8,
    LogColor_LightBlue = 9,
    LogColor_LightGreen = 10,
    LogColor_LightCyan = 11,
    LogColor_LightRed = 12,
    LogColor_LightMagenta = 13,
    LogColor_Yellow = 14,
    LogColor_White = 15,
    LogColor_Blink = 128,
};
public class Log
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
    [DllImport("kernel32.dll")]
    private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);
    [DllImport("kernel32")]
    static extern bool AllocConsole();

    private const UInt32 StdOutputHandle = 0xFFFFFFF5;


    static void Print(LogLevel type, LogColor color, char identifier, string svc, string msg)
    {
        SetConsoleTextColor(LogColor.LogColor_DarkGrey);

        DateTime a1 = DateTime.Now;
        Console.Write(a1.Hour + ":" + a1.Minute + ":" + a1.Second);
        SetConsoleTextColor(color);
        Console.Write(hString.va(" %c ", identifier));
        SetConsoleTextColor(LogColor.LogColor_White);
        Console.Write(hString.va("%s : ", svc));
        SetConsoleTextColor(color);
        Console.Write(hString.va("%s\r\n", msg));
        
        //DEBUG_PRINT(hString.va("%i:%i:%i %c %s : %s\r\n", a1.Hour, a1.Minute, a1.Second, identifier, svc, msg));
    }
    static void SetConsoleTextColor(LogColor color)
    {
        Console.ForegroundColor = (ConsoleColor)color;
    }


    public static void CreateConsole()
    {
        AllocConsole();

        // stdout's handle seems to always be equal to 7
        IntPtr defaultStdout = new IntPtr(7);
        IntPtr currentStdout = GetStdHandle(StdOutputHandle);

        if (currentStdout != defaultStdout)
            // reset stdout
            SetStdHandle(StdOutputHandle, defaultStdout);

        // reopen stdout
        TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
        Console.SetOut(writer);
    }
    public static void DirectPrint(string msg, params object[] args)
    {
        Console.Write(hString.va(msg + "\r\n", args));
    }
    public static void Debug(string svc, string msg, params object[] args)
    {
        Print(LogLevel.LogLevel_Debug, LogColor.LogColor_LightCyan, 'D', svc, hString.va(msg, args));
    }
    public static void Error(string svc, string msg, params object[] args)
    {
        Print(LogLevel.LogLevel_Error, LogColor.LogColor_Red, 'E', svc, hString.va(msg, args));
    }
    public static void Info(string svc, string msg, params object[] args)
    {
        Print(LogLevel.LogLevel_Info, LogColor.LogColor_LightGrey, 'I', svc, hString.va(msg, args));
    }
    public static void Trace(string svc, string msg, params object[] args)
    {
        Print(LogLevel.LogLevel_Trace, LogColor.LogColor_DarkGrey, 'T', svc, hString.va(msg, args));
    }
    public static void Warning(string svc, string msg, params object[] args)
    {
        Print(LogLevel.LogLevel_Warning, LogColor.LogColor_Yellow, 'W', svc, hString.va(msg, args));
    }
}
public class hString
{
    // %s = string
    // %c = char
    // %f = float
    // %i = int
    // %b = bool
    public static string va(string Text, params object[] args)
    {
        int textLength = Text.Length;

        // arguments passed.
        int argC = args.Length;
        int argP = 0;

        for (int i = 0; i < textLength; i++)
        {
            if (argP >= argC)
                break;

            // find the next %(char)
            if (Text[i] == '%')
            {
                // the next char after the current ( i ) is out of the array so fuck all up and return the string.
                if (textLength < i)
                    return Text;

                // Not sure about this! does the P goes 1 over C?
                if (argP >= argC)
                    break;

                if (Text[i + 1] == 's')
                {
                    if (args[argP].GetType() == typeof(string))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                    else if (args[argP].GetType() == typeof(char))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString()[0].ToString());
                    }
                    else if (args[argP].GetType() == typeof(int))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                    else if (args[argP].GetType() == typeof(long))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                    else if (args[argP].GetType() == typeof(byte))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                }
                else if (Text[i + 1] == 'c')
                {
                    if (args[argP].GetType() == typeof(string))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString()[0].ToString());
                    }
                    else if (args[argP].GetType() == typeof(char))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                    else if (args[argP].GetType() == typeof(int))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), Convert.ToChar(args[argP]).ToString());
                    }
                    else if (args[argP].GetType() == typeof(DWORD))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), Convert.ToChar(args[argP]).ToString());
                    }
                    else if (args[argP].GetType() == typeof(byte))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), Convert.ToChar(args[argP]).ToString());
                    }
                }
                else if (Text[i + 1] == 'f')
                {
                    if (args[argP].GetType() == typeof(float))
                    {
                        //Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                    else if (args[argP].GetType() == typeof(int))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                    else if (args[argP].GetType() == typeof(DWORD))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), Convert.ToSingle(args[argP]).ToString());
                    }
                }
                else if (Text[i + 1] == 'i')
                {
                    if (args[argP].GetType() == typeof(float))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), Convert.ToInt32(args[argP]).ToString());
                    }
                    else if (args[argP].GetType() == typeof(int))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                    else if (args[argP].GetType() == typeof(DWORD))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                }
                else if (Text[i + 1] == 'b')
                {
                    if (args[argP].GetType() == typeof(bool))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), args[argP].ToString());
                    }
                    else if (args[argP].GetType() == typeof(int))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), Convert.ToBoolean(args[argP]).ToString());
                    }
                    else if (args[argP].GetType() == typeof(float))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), Convert.ToBoolean(args[argP]).ToString());
                    }
                    else if (args[argP].GetType() == typeof(DWORD))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1]), Convert.ToBoolean(args[argP]).ToString());
                    }
                }
                else if (Text[i + 1] == 'l' && i + 2 <= Text.Length && Text[i + 2] == 'x')
                {
                    if (args[argP].GetType() == typeof(DWORD))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1] + Text[i + 2]), ((DWORD)args[argP]).Address.ToString("X8"));
                    }
                    if (args[argP].GetType() == typeof(int))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1] + Text[i + 2]), ((int)args[argP]).ToString("X8"));
                    }
                    else if (args[argP].GetType() == typeof(byte))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1] + Text[i + 2]), ((byte)args[argP]).ToString("X"));
                    }
                    else if (args[argP].GetType() == typeof(Int16))
                    {
                        Text = ReplaceFirst(Text, ("%" + Text[i + 1] + Text[i + 2]), ((Int16)args[argP]).ToString("X2"));
                    }
                }
                argP++;
            }

            // Update 'textLength'
            textLength = Text.Length;
        }
        return Text;
    }
    static string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
            return text;
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }
}
public class MemoryBuffer
{
    byte[] _Buffer;
    DWORD _Position = 0;

    public byte[] Buffer
    {
        get { return _Buffer; }
        set { _Buffer = value; }
    }
    public DWORD Position
    {
        get { return _Position; }
        set { _Position = value; }
    }

    public MemoryBuffer()
    {
        _Position = 0;
        _Buffer = new byte[] { };
    }
    public MemoryBuffer(byte[] buffer)
    {
        //_Position = buffer.Length;
        _Buffer = buffer;
    }
    public MemoryBuffer(byte buffer)
    {
        //_Position = 1;

        _Buffer = new byte[1];
        _Buffer[0] = buffer;
    }
    public MemoryBuffer(string path)
    {
        _Buffer = File.ReadAllBytes(path);
        //_Position = _Buffer.Length;
    }
    public MemoryBuffer(DWORD size)
    {
        _Buffer = new byte[size];
        //_Position = size;
    }
    public MemoryBuffer(MemoryBuffer buffer)
    {
        _Buffer = buffer._Buffer;
        _Position = _Buffer.Length;
    }



    public DWORD GetBufferSize()
    {
        return _Buffer.Length;
    }
    public DWORD AllocMem(int Size)
    {
        if (Size < 1)
            throw new ArgumentException("Argument \"Size\" is Smaller than 1");

        Array.Resize(ref _Buffer, GetBufferSize() + Size);
        return GetBufferSize() - Size;
    }




    public byte[] ReadBytes(int Length)
    {
        if (_Position + Length >= GetBufferSize() || _Position >= GetBufferSize())
            throw new ArgumentException("Cursor out of Array.");

        byte[] pBuffer = new byte[Length];
        Array.Copy(_Buffer, _Position, pBuffer, 0, Length);
        _Position += Length;
        return pBuffer;
    }
    public byte ReadByte()
    {
        return ReadBytes(1)[0];
    }
    public string ReadString()
    {
        StringBuilder sb = new StringBuilder();

        byte i;
        while ((i = ReadByte()) != 0)
        {
            sb.Append(Convert.ToChar(i));
        }
        return sb.ToString();
    }
    public string ReadString(int Length)
    {
        return new ASCIIEncoding().GetString(ReadBytes(Length));
    }
    public float ReadFloat()
    {
        return BitConverter.ToSingle(ReadBytes(4), 0);
    }
    public bool ReadBool()
    {
        return ReadByte() == 1 ? true : false;
    }
    public short ReadInt16()
    {
        return BitConverter.ToInt16(ReadBytes(2), 0);
    }
    public ushort ReadUInt16()
    {
        return BitConverter.ToUInt16(ReadBytes(2), 0);
    }
    public int ReadInt32()
    {
        return BitConverter.ToInt32(ReadBytes(4), 0);
    }
    public uint ReadUInt32()
    {
        return BitConverter.ToUInt32(ReadBytes(4), 0);
    }
    public long ReadInt64()
    {
        return BitConverter.ToInt64(ReadBytes(8), 0);
    }
    public ulong ReadUInt64()
    {
        return BitConverter.ToUInt64(ReadBytes(8), 0);
    }





    /// <summary>
    /// Add buffer to the mem
    /// </summary>
    /// <param name="buffer"></param>
    public void WriteBytes(byte[] buffer)
    {
        DWORD allocMemPos = AllocMem(buffer.Length);
        Array.Copy(buffer, 0, _Buffer, allocMemPos, buffer.Length);
        _Position += buffer.Length;
    }
    public void WriteByte(byte value)
    {
        WriteBytes(new byte[] { value });
    }
    public void WriteString(string Text, bool ZeroTerminating)
    {
        byte[] b = Encoding.UTF8.GetBytes(Text);
        if (ZeroTerminating)
        {
            Array.Resize(ref b, b.Length + 1);
            b[b.Length - 1] = 0x00;
        }
        WriteBytes(b);
    }
    public void WriteFloat(float value)
    {
        WriteBytes(BitConverter.GetBytes(value));
    }
    public void WriteBool(bool value)
    {
        WriteByte((value ? (byte)1 : (byte)0));
    }
    public void WriteInt16(short value)
    {
        WriteBytes(BitConverter.GetBytes(value));
    }
    public void WriteUInt16(short value)
    {
        WriteBytes(BitConverter.GetBytes(value));
    }
    public void WriteInt32(int value)
    {
        WriteBytes(BitConverter.GetBytes(value));
    }
    public void WriteUInt32(uint value)
    {
        WriteBytes(BitConverter.GetBytes(value));
    }
    public void WriteInt64(long value)
    {
        WriteBytes(BitConverter.GetBytes(value));
    }
    public void WriteUInt64(ulong value)
    {
        WriteBytes(BitConverter.GetBytes(value));
    }





    /// <summary>
    /// Write buffer to Position / if OverStrikeMode else add
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Buffer"></param>
    /// <param name="OverStrikeMode"></param>
    /// <param name="CountPosition"></param>
    public void WriteBytes(DWORD Position, byte[] Buffer, bool OverStrikeMode)
    {
        DWORD BufferLength = GetBufferSize();


        if (Buffer.Length > GetBufferSize())
            AllocMem(Buffer.Length);


        if (OverStrikeMode)
            Array.Copy(Buffer, 0, _Buffer, Position, Buffer.Length);
        else
        {
            AllocMem(Buffer.Length);
            Array.Copy(_Buffer, Position, _Buffer, Position + Buffer.Length, BufferLength - Position);

            Array.Copy(Buffer, 0, _Buffer, Position, Buffer.Length);
        }
    }
    public void WriteByte(DWORD Position, byte value, bool OverStrikeMode)
    {
        WriteBytes(Position, new byte[] { value }, OverStrikeMode);
    }
    public void WriteString(DWORD Position, string Text, bool ZeroTerminating, bool OverStrikeMode)
    {
        byte[] b = Encoding.UTF8.GetBytes(Text);
        if (ZeroTerminating)
        {
            Array.Resize(ref b, b.Length + 1);
            b[b.Length - 1] = 0x00;
        }
        WriteBytes(Position, b, OverStrikeMode);
    }
    public void WriteFloat(DWORD Position, float value, bool OverStrikeMode)
    {
        WriteBytes(Position, BitConverter.GetBytes(value), OverStrikeMode);
    }
    public void WriteBool(DWORD Position, bool value, bool OverStrikeMode)
    {
        WriteByte(Position, (value ? (byte)1 : (byte)0), OverStrikeMode);
    }
    public void WriteInt16(DWORD Position, short value, bool OverStrikeMode)
    {
        WriteBytes(Position, BitConverter.GetBytes(value), OverStrikeMode);
    }
    public void WriteUInt16(DWORD Position, short value, bool OverStrikeMode)
    {
        WriteBytes(Position, BitConverter.GetBytes(value), OverStrikeMode);
    }
    public void WriteInt32(DWORD Position, int value, bool OverStrikeMode)
    {
        WriteBytes(Position, BitConverter.GetBytes(value), OverStrikeMode);
    }
    public void WriteUInt32(DWORD Position, uint value, bool OverStrikeMode)
    {
        WriteBytes(Position, BitConverter.GetBytes(value), OverStrikeMode);
    }
    public void WriteInt64(DWORD Position, long value, bool OverStrikeMode)
    {
        WriteBytes(Position, BitConverter.GetBytes(value), OverStrikeMode);
    }
    public void WriteUInt64(DWORD Position, ulong value, bool OverStrikeMode)
    {
        WriteBytes(Position, BitConverter.GetBytes(value), OverStrikeMode);
    }


    public byte[] ReadBytes(DWORD Position, int Length)
    {
        //if (Position + Length >= GetBufferSize() || Position >= GetBufferSize())
        //    throw new ArgumentException("Cursor out of Array.");

        byte[] pBuffer = new byte[Length];
        Array.Copy(_Buffer, Position, pBuffer, 0, Length);
        return pBuffer;
    }
    public byte ReadByte(DWORD Position)
    {
        return ReadBytes(Position, 1)[0];
    }
    public string ReadString(DWORD Position)
    {
        StringBuilder sb = new StringBuilder();
        int s = 0;
        byte i;
        while ((i = ReadByte(Position + s)) != 0)
        {
            s++;
            sb.Append(Convert.ToChar(i));
        }
        return sb.ToString();
    }
    public string ReadString(DWORD Position, int Length)
    {
        return new ASCIIEncoding().GetString(ReadBytes(Position, Length));
    }
    public float ReadFloat(DWORD Position)
    {
        return BitConverter.ToSingle(ReadBytes(Position, 4), 0);
    }
    public bool ReadBool(DWORD Position)
    {
        return ReadByte(Position) == 1 ? true : false;
    }
    public short ReadInt16(DWORD Position)
    {
        return BitConverter.ToInt16(ReadBytes(Position, 2), 0);
    }
    public ushort ReadUInt16(DWORD Position)
    {
        return BitConverter.ToUInt16(ReadBytes(Position, 2), 0);
    }
    public int ReadInt32(DWORD Position)
    {
        return BitConverter.ToInt32(ReadBytes(Position, 4), 0);
    }
    public uint ReadUInt32(DWORD Position)
    {
        return BitConverter.ToUInt32(ReadBytes(Position, 4), 0);
    }
    public long ReadInt64(DWORD Position)
    {
        return BitConverter.ToInt64(ReadBytes(Position, 8), 0);
    }
    public ulong ReadUInt64(DWORD Position)
    {
        return BitConverter.ToUInt64(ReadBytes(Position, 8), 0);
    }


    
    public void RemoveLast()
    {
        RemoveAt(GetBufferSize() - 1);
    }
    public void RemoveAt(DWORD Position, int Length = 1)
    {
        if (GetBufferSize() <= 0 || Position < 0)
            return;

        if (Position == 0)
        {
            _Buffer[0] = 0x00;
            _Position = 0;
            return;
        }

        Array.Copy(_Buffer, Position, _Buffer, Position - Length + 1, GetBufferSize() - Position);

        _Buffer[GetBufferSize() - 1] = 0x00;

        Array.Resize(ref _Buffer, GetBufferSize() - Length);

        _Position -= Length;
    }
}
public class DWORD
{
    public static readonly DWORD Zero = 0;
    public int Address = 0;

    public DWORD(DWORD binary)
    {
        Address = binary;
    }
    public DWORD(Int32 binary)
    {
        Address = Convert.ToInt32(binary);
    }
    public DWORD(IntPtr binary)
    {
        Address = binary.ToInt32();
    }

    // Create a DWORD64 with 8 bytes from a byte array, offset if needed
    public DWORD(byte[] binary, int offset)
    {
#if _DEBUG
        if (binary.Length < 4)
            throw new Exception("The Binary Array is smaller than 4 bytes.");

        // at offset we start reading 4 bytes for the DWORD.
        // if offset + 4 is out of array thow a exeption.
        if (offset + 4 > binary.Length) // do we need equalent too?
            throw new Exception("The Binary Array at the given offset is out of Array.");
#else
        if (binary.Length < 4)
            Address = 0;

        if (offset + 4 > binary.Length) // do we need equalent too?
            Address = 0;
#endif
        Address = BitConverter.ToInt32(binary, offset);
    }
    public DWORD(byte[] binary)
    {
#if _DEBUG
        if (binary.Length < 4)
            throw new Exception("The Binary Array is smaller than 4 bytes.");

        // at offset we start reading 4 bytes for the DWORD.
        // if offset + 4 is out of array thow a exeption.
        if (4 > binary.Length) // do we need equalent too?
            throw new Exception("The Binary Array at the given offset is out of Array.");
#else
        if (binary.Length < 4)
            Address = 0;

        if (4 > binary.Length) // do we need equalent too?
            Address = 0;
#endif
        Address = BitConverter.ToInt32(binary, 0);
    }



    // int to DWORD
    public static implicit operator DWORD(int binary)
    {
        return new DWORD(binary);
    }

    // DWORD to int
    public static implicit operator int(DWORD binary)
    {
        return binary.Address;
    }

    // DWORD to string
    public static implicit operator string(DWORD binary)
    {
        if (binary == null)
            return "<null>";

        if (binary == 0)
            return "0";

        if (binary < 0)
            return "0x" + ((int)binary).ToString("X");

        if (binary <= 10)
            return binary.ToString();

        return "0x" + binary.ToString();
    }

    // DWORD to IntPtr
    public static implicit operator IntPtr(DWORD binary)
    {
        return new IntPtr(binary.Address);
    }

    // IntPtr to DWORD
    public static implicit operator DWORD(IntPtr binary)
    {
        return new DWORD(binary.ToInt32());
    }

    // DWORD to byte[]
    public static implicit operator byte[] (DWORD binary)
    {
        return BitConverter.GetBytes(binary);
    }


    public static DWORD operator +(DWORD a1, DWORD a2)
    {
        if (a1 == null)
            if (a2 == null)
                return 0;
            else
                return a2;
        else
            if (a2 == null)
            return a1;
        else
            return new DWORD(a1.Address + a2.Address);
    }
    public static DWORD operator -(DWORD a1, DWORD a2)
    {
        if (a1 == null)
            if (a2 == null)
                return 0;
            else
                return a2;
        else
            if (a2 == null)
            return a1;
        else
            return new DWORD(a1.Address - a2.Address);
    }
    public static DWORD operator /(DWORD a1, DWORD a2)
    {
        if (a1 == null)
            if (a2 == null)
                return 0;
            else
                return a2;
        else
            if (a2 == null)
            return a1;
        else
            return new DWORD(a1.Address / a2.Address);
    }
    public static DWORD operator *(DWORD a1, DWORD a2)
    {
        if (a1 == null)
            if (a2 == null)
                return 0;
            else
                return a2;
        else
            if (a2 == null)
            return a1;
        else
            return new DWORD(a1.Address * a2.Address);
    }

    public override string ToString()
    {
        return Address.ToString();
    }
    public string ToString(string fmt)
    {
        if (fmt == "X" || fmt == "x")
            return "0x" + Address.ToString("X12");
        return Address.ToString(fmt);
    }
}