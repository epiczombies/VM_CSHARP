using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    class VM_Executor
    {
        DWORD BaseAddress_Variables; //unsued
        DWORD BaseAddress_Assembly;
        string Filename;
        DWORD HeaderSize;





        MemoryBuffer FileMemory;
        bool _Executing;


        VM_Executor FirstMachine;
        VM_Executor LastMachine;


        // always used shit (mostly on the first machine)
        int MachineIndex = 0;
        public Registers Register;




        public VM_Executor(string path)
        {
            Console.Clear();

            // The first Machine ever is this where the file get opend.
            FirstMachine = this;

            // The last Machine (on Call's)
            LastMachine = null;

            // Load the File into the Memory and Proceed.
            FileMemory = new MemoryBuffer(path);

            // magic check
            if (FileMemory.ReadInt32() == 0x947844)
            {
                // version check
                if (FileMemory.ReadByte() == 147)
                {
                    BaseAddress_Variables = FileMemory.ReadInt32();
                    BaseAddress_Assembly = FileMemory.ReadInt32();
                    HeaderSize = FileMemory.ReadInt32();
                    Filename = FileMemory.ReadString();

                    Log.Info("Executing", "File = %s  V = 0x%lx A = 0x%lx", Filename, BaseAddress_Variables, BaseAddress_Assembly + HeaderSize);

                    Log.DirectPrint("=============================");
                    Log.DirectPrint("Function Called");
                    Log.DirectPrint("Machine = %i", FirstMachine.MachineIndex);
                    Log.DirectPrint("=============================");


                    // set to 0 cause we dont have a header now!
                    FileMemory.Position = BaseAddress_Assembly - BaseAddress_Assembly + HeaderSize;
                    Execute();
                }
            }
        }
        public VM_Executor(VM_Executor last, DWORD Position)
        {
            FirstMachine = last.FirstMachine;
            LastMachine = last;
            FileMemory = last.FileMemory;
            BaseAddress_Variables = last.BaseAddress_Variables;
            BaseAddress_Assembly = last.BaseAddress_Assembly;
            HeaderSize = last.HeaderSize;
            Filename = last.Filename;
            FileMemory.Position = Position;
            FirstMachine.MachineIndex++;


            Log.DirectPrint("=============================");
            Log.DirectPrint("Function Called");
            Log.DirectPrint("Machine = %i", FirstMachine.MachineIndex);
            Log.DirectPrint("=============================");
        }
        public void Execute()
        {
            _Executing = true;

            while (_Executing)
            {
                byte opcode = FileMemory.ReadByte();

                //Log.Info("Execute()", "0x%lx 0x%lx", FileMemory.Position, opcode);







                #region CALL
                if (opcode == 0xE8)
                {
                    DWORD PROCEDURE = FileMemory.ReadInt32();
                    Log.Info("CALL", "0x%lx", PROCEDURE + FileMemory.Position);

                    UpdateRegister();


                    LastMachine = this;

                    //save the positino of the file.
                    DWORD LastPosition = FileMemory.Position;

                    // Create a 'VM_Executor'
                    VM_Executor vme = new VM_Executor(this, PROCEDURE + FileMemory.Position);
                    vme.Execute();

                    // recover the position of the file
                    FileMemory.Position = LastPosition;
                }
                #endregion

                #region CALL DWORD PTR[...]
                else if (opcode == 0xFF)
                {
                    byte next = FileMemory.ReadByte();

                    // call dword ptr
                    if (next == 0x15)
                    {
                        DWORD READ_FROM = FileMemory.ReadInt32() - BaseAddress_Assembly + HeaderSize;
                        DWORD PROCEDURE = FileMemory.ReadInt32(READ_FROM) - BaseAddress_Assembly + HeaderSize;
                        UpdateRegister();

                        Log.Info("CALL", "[0x%lx] => 0x%lx", READ_FROM, PROCEDURE);


                        LastMachine = this;

                        //save the positino of the file.
                        DWORD LastPosition = FileMemory.Position;

                        // Create a 'VM_Executor'
                        VM_Executor vme = new VM_Executor(this, PROCEDURE);
                        vme.Execute();

                        // recover the position of the file
                        FileMemory.Position = LastPosition;
                    }
                }
                #endregion

                #region PUSH REGISTER
                else if (opcode == 0x50 ||
                    opcode == 0x51 ||
                    opcode == 0x52 ||
                    opcode == 0x53 ||
                    opcode == 0x54 ||
                    opcode == 0x55 ||
                    opcode == 0x56 ||
                    opcode == 0x57)
                {
                    UpdateRegister();
                    Log.Info("PUSH", "%s", Utils.RegisterToString(Utils.ByteToRegister(opcode, 0x50)));
                }
                #endregion

                #region MOV

                // MOV REGISTER, VARIABLE
                else if (opcode == 0xB8 | // EAX
                    opcode == 0xB9 || // ECX
                    opcode == 0xBA || // EDX
                    opcode == 0xBB || // EBX
                    opcode == 0xBC || // ESP
                    opcode == 0xBD || // EBP
                    opcode == 0xBE || // ESI
                    opcode == 0xBF) // EDI
                {
                    DWORD VALUE = FileMemory.ReadInt32();
                    UpdateRegister();
                    SetRegisterValue(Utils.ByteToRegister(opcode, 0xB8), VALUE);

                    Log.Info("MOV", "%s, 0x%lx", Utils.RegisterToString(Utils.ByteToRegister(opcode, 0xB8)), VALUE);
                }
                else if (opcode == 0x8B || opcode == 0xA1)
                {
                    if(opcode == 0xA1)
                    {
                        DWORD READ_FROM = FileMemory.ReadInt32() - BaseAddress_Assembly;
                        DWORD PROCEDURE = FileMemory.ReadInt32(READ_FROM);

                        UpdateRegister();

                        Log.Info("MOV", "EAX, [0x%lx] => 0x%lx", READ_FROM, PROCEDURE);

                        SetRegisterValue(x32Register.EAX, PROCEDURE);
                        break;
                    }


                    int next = FileMemory.ReadByte();
                    UpdateRegister();

                    //todo
                    switch (next)
                    {
                        // EAX
                        case 0x80: // eax eax
                        case 0x81: // eax ecx
                        case 0x82: // eax edx
                        case 0x83: // eax ebx
                        case 0x84: // eax esp => next should be 24 then
                        case 0x85: // eax ebp
                        case 0x86: // eax esi
                        case 0x87: // eax edi

                        // ECX
                        case 0x88: // ecx eax
                        case 0x89: // ecx ecx
                        case 0x8A: // ecx edx
                        case 0x8B: // ecx ebx
                        case 0x8C: // ecx esp => next should be 24 then
                        case 0x8D: // ecx ebp
                        case 0x8E: // ecx esi
                        case 0x8F: // ecx edi



                        // EDX
                        case 0x90: // edx eax
                        case 0x91: // edx ecx
                        case 0x92: // edx edx
                        case 0x93: // edx ebx
                        case 0x94: // edx esp => next should be 24 then
                        case 0x95: // edx ebp
                        case 0x96: // edx esi
                        case 0x97: // edx edi

                        // EBX
                        case 0x98: // ebx eax
                        case 0x99: // ebx ecx
                        case 0x9A: // ebx edx
                        case 0x9B: // ebx ebx
                        case 0x9C: // ebx esp => next should be 24 then
                        case 0x9D: // ebx ebp
                        case 0x9E: // ebx esi
                        case 0x9F: // ebx edi


                        // ESP
                        case 0xA0: // esp eax
                        case 0xA1: // esp ecx
                        case 0xA2: // esp edx
                        case 0xA3: // esp ebx
                        case 0xA4: // esp esp => next should be 24 then
                        case 0xA5: // esp ebp
                        case 0xA6: // esp esi
                        case 0xA7: // esp edi

                        // EBP
                        case 0xA8: // ebp eax
                        case 0xA9: // ebp ecx
                        case 0xAA: // ebp edx
                        case 0xAB: // ebp ebx
                        case 0xAC: // ebp esp => next should be 24 then
                        case 0xAD: // ebp ebp
                        case 0xAE: // ebp esi
                        case 0xAF: // ebp edi

                        // ESI
                        case 0xB0: // esi eax
                        case 0xB1: // esi ecx
                        case 0xB2: // esi edx
                        case 0xB3: // esi ebx
                        case 0xB4: // esi esp => next should be 24 then
                        case 0xB5: // esi ebp
                        case 0xB6: // esi esi
                        case 0xB7: // esi edi

                        // EDI 
                        case 0xB8: // edi eax
                        case 0xB9: // edi ecx
                        case 0xBA: // edi edx
                        case 0xBB: // edi ebx
                        case 0xBC: // edi esp => next should be 24 then
                        case 0xBD: // edi ebp
                        case 0xBE: // edi esi
                        case 0xBF: // edi edi
                        {
                            int s = next - 0xC0;
                            x32Register firstRegister = (x32Register)(s / 8);
                            x32Register secondRegister = (x32Register)((int)(s - ((byte)firstRegister * 8)));
                            DWORD VARIABLE;

                            if (next == 0x84 || next == 0x8C ||
                                next == 0x94 || next == 0x9C ||
                                next == 0xA4 || next == 0xAC ||
                                next == 0xB4 || next == 0xBC)
                            {
                                int next2 = FileMemory.ReadByte();
                                if(next2 == 0x24)
                                {
                                    VARIABLE = FileMemory.ReadInt32();
                                    UpdateRegister();
                                    SetRegisterValue(firstRegister, GetRegisterValueByEnum(secondRegister));
                                    Log.Info("MOV", "%s, [%s+0x%lx]", Utils.RegisterToString(firstRegister), Utils.RegisterToString(secondRegister), VARIABLE);
                                    return;
                                }
                            }
                            VARIABLE = FileMemory.ReadInt32();
                            UpdateRegister();
                            SetRegisterValue(firstRegister, GetRegisterValueByEnum(secondRegister));
                            Log.Info("MOV", "%s, [%s+0x%lx]", Utils.RegisterToString(firstRegister), Utils.RegisterToString(secondRegister), VARIABLE);
                            break;
                        }


                        // MOV REGISTER, [VARIABLE]
                        case 0x0D:
                        case 0x15:
                        case 0x1D:
                        case 0x25:
                        case 0x2D:
                        case 0x35:
                        case 0x3D:
                        {
                            DWORD READ_FROM = FileMemory.ReadInt32() - BaseAddress_Assembly + HeaderSize;
                            DWORD PROCEDURE = FileMemory.ReadInt32(READ_FROM);

                            UpdateRegister();
                            Log.Info("MOV", "EAX, [0x%lx] => 0x%lx", READ_FROM, PROCEDURE);

                            SetRegisterValue(x32Register.ECX, PROCEDURE);
                            break;
                        }


                        // MOV REGISTER, REGISTER
                        // EAX
                        case 0xC0:
                        case 0xC1:
                        case 0xC2:
                        case 0xC3:
                        case 0xC4:
                        case 0xC5:
                        case 0xC6:
                        case 0xC7:

                        // ECX
                        case 0xC8:
                        case 0xC9:
                        case 0xCA:
                        case 0xCB:
                        case 0xCC:
                        case 0xCD:
                        case 0xCE:
                        case 0xCF:

                        // EDX
                        case 0xD0:
                        case 0xD1:
                        case 0xD2:
                        case 0xD3:
                        case 0xD4:
                        case 0xD5:
                        case 0xD6:
                        case 0xD7:

                        // EBX
                        case 0xD8:
                        case 0xD9:
                        case 0xDA:
                        case 0xDB:
                        case 0xDC:
                        case 0xDD:
                        case 0xDE:
                        case 0xDF:


                        // ESP
                        case 0xE0:
                        case 0xE1:
                        case 0xE2:
                        case 0xE3:
                        case 0xE4:
                        case 0xE5:
                        case 0xE6:
                        case 0xE7:

                        // EBP
                        case 0xE8:
                        case 0xE9:
                        case 0xEA:
                        case 0xEB:
                        case 0xEC:
                        case 0xED:
                        case 0xEE:
                        case 0xEF:

                        // ESI
                        case 0xF0:
                        case 0xF1:
                        case 0xF2:
                        case 0xF3:
                        case 0xF4:
                        case 0xF5:
                        case 0xF6:
                        case 0xF7:

                        // EDI
                        case 0xF8:
                        case 0xF9:
                        case 0xFA:
                        case 0xFB:
                        case 0xFC:
                        case 0xFD:
                        case 0xFE:
                        case 0xFF:
                        {
                            int s = next - 0xC0;
                            UpdateRegister();
                            x32Register firstRegister = (x32Register)(s / 8);
                            x32Register secondRegister = (x32Register)((int)(s - ((byte)firstRegister * 8)));
                            SetRegisterValue(firstRegister, GetRegisterValueByEnum(secondRegister));
                            Log.Info("MOV", "%s, %s", Utils.RegisterToString(firstRegister), Utils.RegisterToString(secondRegister));
                            break;
                        }
                    }
                }
                #endregion

                #region PUSH VALUE
                else if (opcode == 0x6A)
                {
                    byte VALUE = FileMemory.ReadByte();
                    UpdateRegister();

                    Log.Info("PUSH", "0x%lx", VALUE);
                }
                #endregion

                #region PUSH VARIABLE
                else if (opcode == 0x68)
                {
                    DWORD VALUE = FileMemory.ReadInt32();
                    UpdateRegister();
                    Log.Info("PUSH", "0x%lx", VALUE);
                }
                #endregion

                #region POP REGISTER
                else if (opcode == 0x58 ||
                    opcode == 0x59 ||
                    opcode == 0x5A ||
                    opcode == 0x5B ||
                    opcode == 0x5C ||
                    opcode == 0x5D ||
                    opcode == 0x5E ||
                    opcode == 0x5F)
                {
                    UpdateRegister();
                    Log.Info("POP", "%s", Utils.RegisterToString(Utils.ByteToRegister(opcode, 0x58)));
                }
                #endregion

                #region RETURN
                else if (opcode == 0xC3)
                {
                    UpdateRegister();
                    Log.Info("RETN", "");

                    if (LastMachine == null)
                    {
                        Log.Info("Virtual Machine", "The Virtual Machine ended the current File.");
                        _Executing = false;
                        return;
                    }
                    else
                    {
                        Log.DirectPrint("=============================");
                        Log.DirectPrint("Returning to the Last Virtual Machine.");
                        Log.DirectPrint("Machine = %i", FirstMachine.MachineIndex);
                        Log.DirectPrint("=============================");

                        FirstMachine.MachineIndex--;
                        _Executing = false;
                        LastMachine._Executing = true;
                        return;
                    }
                }
                #endregion

                #region RETURN VALUE
                else if (opcode == 0xC2)
                {
                    Int16 VALUE = FileMemory.ReadInt16();
                    UpdateRegister();

                    Log.Info("RETN", "0x%lx", VALUE);

                    if (LastMachine == null)
                    {
                        Log.Info("Virtual Machine", "The Virtual Machine ended the current File.");
                        _Executing = false;
                        return;
                    }
                    else
                    {
                        Log.DirectPrint("=============================");
                        Log.DirectPrint("Returning to the Last Virtual Machine.");
                        Log.DirectPrint("Machine = %i", FirstMachine.MachineIndex);
                        Log.DirectPrint("=============================");

                        FirstMachine.MachineIndex--;
                        _Executing = false;
                        LastMachine._Executing = true;
                        return;
                    }
                }
                #endregion

                #region INT3
                else if (opcode == 0xCC)
                {
                    UpdateRegister();

                    Log.Info("INT3", "");

                    _Executing = false;
                }
                #endregion

                else
                {
                    Log.Error("Virtual Machine", "0x%lx is a Unknown Byte at 0x%lx!", opcode, FileMemory.Position);
                    _Executing = false;
                    return;
                }
            }
        }


        public DWORD RealMemoryAddress(DWORD Address)
        {
            //Log.Trace("GetRealFileMemoryAddress", "Address = 0x%lx", Address);
            //Log.Trace("GetRealFileMemoryAddress", "BaseAddress_Assembly = 0x%lx", BaseAddress_Assembly);
            //Log.Trace("GetRealFileMemoryAddress", "Result = 0x%lx", Address - BaseAddress_Assembly);
            return Address - BaseAddress_Assembly + HeaderSize;
        }
        public void UpdateRegister()
        {
            FirstMachine.Register.EIP = FileMemory.Position + BaseAddress_Assembly + HeaderSize;
        }        
        public DWORD GetRegisterValueByEnum(x32Register sRegister)
        {
            switch (sRegister)
            {
                case x32Register.EAX: { return Register.EAX; }
                case x32Register.ECX: { return Register.ECX; }
                case x32Register.EDX: { return Register.EDX; }
                case x32Register.EBX: { return Register.EBX; }
                case x32Register.ESP: { return Register.ESP; }
                case x32Register.EBP: { return Register.EBP; }
                case x32Register.ESI: { return Register.ESI; }
                case x32Register.EDI: { return Register.EDI; }
            }
            return null;
        }
        public void SetRegisterValue(x32Register sRegister, DWORD Value)
        {
            switch (sRegister)
            {
                case x32Register.EAX: { FirstMachine.Register.EAX = Value; break; }
                case x32Register.ECX: { FirstMachine.Register.ECX = Value; break; }
                case x32Register.EDX: { FirstMachine.Register.EDX = Value; break; }
                case x32Register.EBX: { FirstMachine.Register.EBX = Value; break; }
                case x32Register.ESP: { FirstMachine.Register.ESP = Value; break; }
                case x32Register.EBP: { FirstMachine.Register.EBP = Value; break; }
                case x32Register.ESI: { FirstMachine.Register.ESI = Value; break; }
                case x32Register.EDI: { FirstMachine.Register.EDI = Value; break; }
            }
        }
    }
}