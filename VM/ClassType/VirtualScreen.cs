using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace VM.ClassType
{
    class VirtualScreen : UserControl
    {
        internal Thread UpdateThread;
        public Brush ClearColor;


        List<VS_Button> VS_Buttons;

        public VirtualScreen()
        {
            VS_Buttons = new List<VS_Button>();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            return;
            //base.OnPaint(e);
        }
    }
    class VS_Button
    {
        int index;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }


        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }




        public bool Contains(Vector2 Point)
        {
            if (Point.x >= Position.x && Point.x <= Position.x + Size.x &&
                Point.y <= Position.y + Size.y && Point.y >= Position.y)
                return true;
            else
                return false;
        }
    }
}
