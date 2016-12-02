using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace VM
{
    public delegate void VS_Button(object sender, VirtualScreen e);
    public partial class RegisterView : Form
    {
        VirtualScreen VS;

        // Fonts
        Font MainFont;


        Point MousePosition = new Point(0,0);
        bool isMouseLeftDown;
        int Button_count;
        List<string> VS_Button_Name;
        List<Rectangle> VS_Button_Rect;
        List<VS_Button> VS_Button_Click;
        public void AddButton(string name, Vector2 position, Vector2 size, VS_Button click)
        {
            Button_count++;
            VS_Button_Name.Add(name);
            VS_Button_Rect.Add(new Rectangle((int)position.x, (int)position.y, (int)size.x, (int)size.y));
            VS_Button_Click.Add(click);
        }

        List<string> Code = new List<string>();




        //compiler shit
        VM_Executor vme;


        public RegisterView()
        {
            InitializeComponent();
            VS_Button_Name = new List<string>();
            VS_Button_Rect = new List<Rectangle>();
            VS_Button_Click = new List<VS_Button>();

            AddButton("Compile Test", new Vector2(this.Width - 150, 10), new Vector2(200, 100), Button_CTest);
            AddButton("Run Test", new Vector2(this.Width - 150, 125), new Vector2(200, 100), Button_RTest);
            AddButton("Scripter", new Vector2(this.Width - 150, 250), new Vector2(200, 100), Button_OpenScripter);


            MainFont = new Font("Arial", 12);
            VirtualScreenEventHandler.UpdateFrame += VirtualScreenEventHandler_UpdateFrame;
            VS = new VirtualScreen(Handle, Width, Height, 120);
            VS.ClearColor = Brushes.Black;
            VS.RunRendererThread();
        }
        private void Button_CTest(object sender, VirtualScreen e)
        {
            VM_Creator vm = new VM_Creator();
            vm.test();
        }
        private void Button_RTest(object sender, VirtualScreen e)
        {
            vme = new VM_Executor(@"E:\VisualStudios\VM\VM\bin\Debug\basic_test.compiled");
            //auto run. :)
        }
        private void Button_OpenScripter(object sender, VirtualScreen e)
        {

        }
        private void VirtualScreenEventHandler_UpdateFrame(object sender, VirtualScreen e)
        {   
            Vector2 Mouse = new Vector2(MousePosition.X, MousePosition.Y);

            for (int i = 0; i < Button_count; i++)
            {
                Vector2 position_button = new Vector2(VS_Button_Rect[i].X, VS_Button_Rect[i].Y);
                Vector2 size_button = new Vector2(VS_Button_Rect[i].Width, VS_Button_Rect[i].Height);

                VS.UI_DrawFilledRectangle(position_button, size_button, Brushes.White);
                VS.UI_DrawText(VS_Button_Name[i], position_button, MainFont, Brushes.Black);

                // check if the cursor btw mouse position is in the rect of position and size.../
                if (MousePosition.X >= VS_Button_Rect[i].Left && MousePosition.X <= VS_Button_Rect[i].Right && MousePosition.Y <= VS_Button_Rect[i].Bottom && MousePosition.Y >= VS_Button_Rect[i].Top)
                {
                    if(isMouseLeftDown)
                        VS_Button_Click[i](sender, e);

                    isMouseLeftDown = false;
                }
            }
            isMouseLeftDown = false;

            VS.UI_DrawFilledRectangle(Mouse, new Vector2(15,15), Brushes.Gray);
            VS.UI_DrawText("Register View:", new Vector2(0, 0), MainFont, Brushes.White);
            
            if (vme != null)
            {
                string vme_registers = hString.va("%s\n%lx\n%s\n%lx\n%s\n%lx\n%s\n%lx\n%s\n%lx\n%s\n%lx\n%s\n%lx\n%s\n%lx\n%s\n%lx\n", "EAX", vme.Register.EAX, "EBX", vme.Register.EBX, "ECX", vme.Register.ECX, "EDX", vme.Register.EDX, "ESI", vme.Register.ESI, "EDI", vme.Register.EDI, "EBP", vme.Register.EBP, "ESP", vme.Register.ESP, "EIP", vme.Register.EIP);
                VS.UI_DrawText(vme_registers, new Vector2(0, 25), MainFont, Brushes.White);

                //string Code_Draw = "";
                //Vector2 DrawPoint = new Vector2(100,25);
                //for(int i = 0; i < Code.Count; i++)
                //{
                //    Vector2 info = VS.GetTextInfo(Code[i], MainFont);
                //    if ((info.y * i) + 25 > Width)
                //        DrawPoint.y += info.y;
                //    else
                //        DrawPoint.y -= info.y*3;

                //    Code_Draw += Code[i] + "\r";
                //}
                //VS.UI_DrawText(Code_Draw, DrawPoint, MainFont, Brushes.White);
            }
        }
        private void RegisterView_MouseClick(Object sender, MouseEventArgs e)
        {
            isMouseLeftDown = true;
        }
        private void RegisterView_MouseMove(Object sender, MouseEventArgs e)
        {
            Cursor.Hide();
            MousePosition = PointToClient(Cursor.Position);
        }
    }
}
