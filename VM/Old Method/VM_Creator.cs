// REGISTER A VALUE ADDS THE NAME AND THE ADDRESS WHERE IT IS STORED
// THE DEFAULT VALUE HAVE TO BE SAVED TOO!!!
// ELSE IT NEVER GNNA WORK

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    // Can be a Variable: Name Address Value
    // Can be a Function: Name Address
    class XLabel
    {
        public string LabelName;
        public DWORD Position;
        public DWORD Variable;

        public XLabel(string Name, DWORD Address, DWORD Value)
        {
            LabelName = Name;
            Position = Address;
            Variable = Value;
        }
        public XLabel(string Name, DWORD Address)
        {
            LabelName = Name;
            Position = Address;
            Variable = null;
        }
    }

    // Create a Virtual Machine to run later.
    // VM_V2 can run this and almost works.
    class VM_Creator
    {
        DWORD BaseAddress_Variables;
        DWORD BaseAddress_Assembly;

        /// <summary>
        /// Variables are stored here.
        /// </summary>
        List<XLabel> StoredVariables;
        List<XLabel> variable_patches;

        /// <summary>
        /// All Stored shit to repatch.
        /// </summary>
        List<XLabel> call_patches;
        
        /// <summary>
        /// Function are stored here.
        /// </summary>
        Dictionary<string, DWORD> RegisteredFunctions;




        /// <summary>
        /// The current Address of the assembly where it get writed.
        /// </summary>
        DWORD AssemblyAddress { get { return BaseAddress_Assembly + Assembly.Position; } }

        /// <summary>
        /// The current Address of the Variables and String's get writed.
        /// </summary>
        DWORD VariablesAddress { get { return BaseAddress_Variables + VariableAddress;/*Variables.Position;*/ } }

        /// <summary>
        /// The Memory where the header is stored.
        /// </summary>
        MemoryBuffer Header;

        /// <summary>
        /// The Memory Where op's are stored.
        /// Don't forget to sub. the BaseAddress_Assembly from  VariablesAddress;
        /// </summary>
        MemoryBuffer Assembly;

        /// <summary>
        /// The Memory Where Varaibles and Strings are stored.
        /// Don't forget to sub. the BaseAddress_Variables from  VariablesAddress;
        /// </summary>
        MemoryBuffer Variables;

        /// <summary>
        /// The Address Where the Next Variable get stored.
        /// </summary>
        DWORD VariableAddress;


        public VM_Creator()
        {
            Header = new MemoryBuffer();
            Assembly = new MemoryBuffer();
            Variables = new MemoryBuffer();
            RegisteredFunctions = new Dictionary<string, DWORD>();
            call_patches = new List<XLabel>();
            variable_patches = new List<XLabel>();
            StoredVariables = new List<XLabel>();

            BaseAddress_Variables = 0x401300;//0xD37000; // (virtual address 00937000)
            BaseAddress_Assembly = 0x401000;//0x401000; // (virtual address 00001000)


            // MAGIC    <int>
            Header.WriteInt32(0x947844);

            // Version  <byte>
            Header.WriteByte(147);

            // base addresses
            Header.WriteInt32(BaseAddress_Variables);
            Header.WriteInt32(BaseAddress_Assembly);

            //Header Size
            Header.WriteInt32(0x30);

            // Filename is last else we need a pointer.
            byte[] b = Encoding.UTF8.GetBytes("basic_test.compiled");
            Array.Resize(ref b, 0x30 - 4 - 1 - 4 - 4 - 4);
            Header.WriteBytes(b);
        }
        public void test()
        {
            //MOV(x32Register.EDX, x32Register.ESP, 0x000007B0);
            REGISTER_VARIABLE("set_eax_to_2", 2);


            ///////////////////////////////////////////////////////////////
            REGISTER_FUNCTION("WinMain");
            ///////////////////////////////////////////////////////////////
            PUSH((byte)0);
            CALL("TEST");
            RETN();
            ///////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////////////
            REGISTER_FUNCTION("TEST");
            ///////////////////////////////////////////////////////////////
            MOV_PTR(x32Register.EAX, GET_REGISTERED_VARIABLE("set_eax_to_2"));
            RETN();
            ///////////////////////////////////////////////////////////////



            GenerateFile();
        }
        public void GenerateFile()
        {
            DWORD HeaderSize = 0x30;
            DWORD AssemblySize = BaseAddress_Variables - BaseAddress_Assembly;


            // Load all the Patches.
            foreach (XLabel var in call_patches)
            {
                Log.Info("Patching Call's","Patching at 0x%lx - %s", var.Position, var.LabelName);
                Assembly.WriteInt32(var.Position, GET_REGISTERED_FUNCTION(var.LabelName) - var.Position - 5 + 1, true);
            }

            foreach (XLabel var in variable_patches)
            {
                Log.Info("Patching Variables", "Patching at 0x%lx - %s", var.Position, var.LabelName);
                Variables.WriteInt32(var.Position, var.Variable, true);
            }


            // load up the variables and write them.
            //foreach (DWORD var in Stored_Variable_DefaultValue)
            //    Variables.WriteInt32(var);

            MemoryBuffer File = new MemoryBuffer(HeaderSize + AssemblySize + Variables.GetBufferSize());
            File.WriteBytes(0, Header.Buffer, true);
            File.WriteBytes(HeaderSize, Assembly.Buffer, true);
            File.WriteBytes(HeaderSize + AssemblySize, Variables.Buffer, true);

            System.IO.File.WriteAllBytes(@"E:\VisualStudios\VM\VM\bin\Debug\basic_test.compiled", File.Buffer);
        }


        public void REGISTER_FUNCTION(string Name)
        {
            if (!RegisteredFunctions.ContainsKey(Name))
                RegisteredFunctions.Add(Name, Assembly.Position);
        }
        public void REGISTER_VARIABLE(string Name, DWORD Value)
        {
            bool contains = false;
            foreach (XLabel xl in StoredVariables)
                if (xl.LabelName == Name)
                    contains = true;

            if (contains == false)
            {
                StoredVariables.Add(new XLabel(Name, Header.GetBufferSize() + BaseAddress_Variables - BaseAddress_Assembly + Variables.Position, Value));
                Variables.WriteInt32(Value);
            }
        }
        public DWORD GET_REGISTERED_FUNCTION(string Name)
        {
            if (RegisteredFunctions.ContainsKey(Name))
                return RegisteredFunctions[Name];

            Log.Error("GET_REGISTERED_FUNCTION", "%s could nott be found!", Name);
            AddPatchRequired(Name);
            return -1;
        }
        public DWORD GET_REGISTERED_VARIABLE(string Name)
        {
            foreach (XLabel xl in StoredVariables)
                if (xl.LabelName == Name)
                    return xl.Position + BaseAddress_Assembly;


            Log.Error("GET_REGISTERED_FUNCTION", "%s could not be found!", Name);
            AddPatchRequired(Name, Variables.Position);
            return -1;
        }
        void AddPatchRequired(string Name)
        {
            Log.Error("AddPatchRequired", "%s where added.", Name);
            call_patches.Add(new XLabel(Name, Assembly.Position));
        }
        void AddPatchRequired(string Name, DWORD Address)
        {
            Log.Error("AddPatchRequired", "%s where added.", Name);
            variable_patches.Add(new XLabel(Name, Address, -1));
        }





        /// <summary>
        /// call procedure
        /// </summary>
        public void CALLPTR(DWORD Variable)
        {
            Assembly.WriteByte(0xFF);
            Assembly.WriteByte(0x15);
            Assembly.WriteBytes(BitConverter.GetBytes(Variable));

            Log.Info("CALL", "DWORD PTR [0x%lx]", Variable);
        }

        /// <summary>
        /// Call a Address
        /// </summary>
        public void CALL(DWORD Variable)
        {
            Assembly.WriteByte(0xE8);
            Assembly.WriteBytes(BitConverter.GetBytes(Variable - AssemblyAddress - 5));

            Log.Info("CALL", "0x%lx", Variable - AssemblyAddress - 5);
        }

        /// <summary>
        /// Call a registered function
        /// </summary>
        public void CALL(string FunctionName)
        {
            Assembly.WriteByte(0xE8);

            DWORD F = GET_REGISTERED_FUNCTION(FunctionName);
            Assembly.WriteBytes(BitConverter.GetBytes(F - AssemblyAddress - 5));
            Log.Info("CALL", "0x%lx", F);
        }

        /// <summary>
        /// push word or doubleword onto the stack
        /// max = 0xFFFFFFFF / 4294967295
        /// </summary>
        public void PUSH(DWORD v)
        {
            Assembly.WriteByte(0x68);
            Assembly.WriteBytes(BitConverter.GetBytes(v));
            Log.Info("PUSH", "0x%lx", v);
        }

        /// <summary>
        /// push byte onto the stack
        /// max = 0xFF / 255
        /// </summary>
        public void PUSH(byte v)
        {
            Assembly.WriteByte(0x6A);
            Assembly.WriteByte(v);
            Log.Info("PUSH", "0x%lx", v);
        }

        /// <summary>
        /// Push a Register onto the Stack
        /// </summary>
        /// <param name="r"></param>
        public void PUSH(x32Register r)
        {
            Assembly.WriteByte(Utils.RegisterToByte(r, 0x50));
            Log.Info("PUSH", "%s", Utils.RegisterToString(r));
        }

        /// <summary>
        /// mov r, v
        /// </summary>
        /// <param name="r"></param>
        /// <param name="Variable"></param>
        public void MOV(x32Register r, DWORD v)
        {
            Assembly.WriteByte(Utils.RegisterToByte(r, 0xB8));
            Assembly.WriteBytes(BitConverter.GetBytes(v));
            Log.Info("MOV", "%s, 0x%lx", Utils.RegisterToString(r), v);
        }

        /// <summary>
        /// mov r, [v]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="Variable"></param>
        public void MOV_PTR(x32Register r, DWORD v)
        {
            if (r == x32Register.EAX)
                Assembly.WriteByte(0xA1);
            else
            {
                Assembly.WriteByte(0x8B);
                switch (r)
                {
                    case x32Register.ECX: { Assembly.WriteByte(0x0D); break; };
                    case x32Register.EDX: { Assembly.WriteByte(0x15); break; };
                    case x32Register.EBX: { Assembly.WriteByte(0x1D); break; };
                    case x32Register.ESP: { Assembly.WriteByte(0x25); break; };
                    case x32Register.EBP: { Assembly.WriteByte(0x2D); break; };
                    case x32Register.ESI: { Assembly.WriteByte(0x35); break; };
                    case x32Register.EDI: { Assembly.WriteByte(0x3D); break; };
                }
            }

            Assembly.WriteBytes(BitConverter.GetBytes(v));
            Log.Info("MOV", "%s, [0x%lx]", Utils.RegisterToString(r), v);
        }

        /// <summary>
        /// Change Register + pr Value to Variable
        /// </summary>
        /// <param name="r"></param>
        /// <param name="Variable"></param>
        public void MOV(x32Register r, byte pr, DWORD v)
        {
            Log.Info("MOV", "[%s+0x%lx], 0x%lx", Utils.RegisterToString(r), pr, v);
            if (r == x32Register.ESP)
            {
                Assembly.WriteByte(0xC7);
                Assembly.WriteByte(Utils.RegisterToByte(r, 0x40));
                Assembly.WriteByte(0x24);
                Assembly.WriteByte(pr);
                Assembly.WriteBytes(BitConverter.GetBytes(v));
            }
            else
            {
                Assembly.WriteByte(0xC7);
                Assembly.WriteByte(Utils.RegisterToByte(r, 0x40));
                Assembly.WriteByte(pr);
                Assembly.WriteBytes(BitConverter.GetBytes(v));
            }
        }

        /// <summary>
        /// mov c,[r+pr]
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="pr"></param>
        public void MOV(x32Register c, x32Register r, byte pr)
        {
            Assembly.WriteByte(0x8B);
            Assembly.WriteByte(Utils.CalculateRegister(0x40, c, r));
            Assembly.WriteByte(pr);
            Log.Info("MOV", "%s, [%s+0x%lx]", Utils.RegisterToString(c), r, pr);
        }
        
        /// <summary>
         /// mov c,[r+pr]
         /// </summary>
         /// <param name="c"></param>
         /// <param name="r"></param>
         /// <param name="pr"></param>
        public void MOV(x32Register c, x32Register r, DWORD pr)
        {
            Assembly.WriteByte(0x8B);
            Assembly.WriteByte(Utils.CalculateRegister(0x80, c, r));

            if (r == x32Register.ESP)
                Assembly.WriteByte(0x24);

            Assembly.WriteInt32(pr);
            Log.Info("MOV", "%s, [%s+0x%lx]", Utils.RegisterToString(c), r, pr);
        }

        /// <summary>
        /// Change Register Value to t Register Value
        /// </summary>
        /// <param name="c"></param>
        /// <param name="t"></param>
        public void MOV(x32Register c, x32Register t)
        {
            Assembly.WriteByte(0x8B);
            Assembly.WriteByte(Utils.CalculateRegister(0xC0, c, t));
            Log.Info("MOV", "%s, %s", Utils.RegisterToString(c), Utils.RegisterToString(t));
        }

        /// <summary>
        /// Write the Register r Value to Variable
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="r"></param>
        public void MOV(DWORD Variable, x32Register r)
        {
            Assembly.WriteByte(0x89);
            Assembly.WriteByte(Utils.CalculateRegister(r, 0x05));
            Assembly.WriteBytes(BitConverter.GetBytes(Variable));
            Log.Info("MOV", "0x%lx, %s", Utils.RegisterToString(r));
        }

        /// <summary>
        /// pop a value from the stack
        /// </summary>
        /// <param name="r"></param>
        public void POP(x32Register r)
        {
            Assembly.WriteByte(Utils.CalculateRegister(0x58, r));
            Log.Info("POP", Utils.RegisterToString(r));
        }

        /// <summary>
        /// Return
        /// </summary>
        public void RETN()
        {
            Assembly.WriteByte(0xC3);
            Log.Info("RETN", "");
        }

        /// <summary>
        /// Return
        /// </summary>
        public void RETN(Int16 size)
        {
            Assembly.WriteByte(0xC2);
            Assembly.WriteInt16(size);
            Log.Info("RETN", "0x%lx", size);
        }

        /// <summary>
        /// Return
        /// </summary>
        public void INT3()
        {
            Assembly.WriteByte(0xCC);
            Log.Info("INT3", "");
        }
    }
}