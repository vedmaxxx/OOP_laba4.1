using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP4
{
    public partial class Form_main : Form
    {
        Mylist list;
        bool isCTRL;
        Bitmap bitmap;
        Graphics gr ;
        public Form_main()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            
            list = new Mylist();
            isCTRL = false;
            pictureBox1.Image = GetBitmap();
        }
        public void CreateCCircle(object sender,MouseEventArgs e)
        {
            clearSheet();
            CCircle circle = new CCircle(e.X, e.Y, list, isCTRL);
            PaintDraw();
            pictureBox1.Image = GetBitmap();
        }

        private void PaintAll()
        {
            clearSheet();
            PaintDraw();
            pictureBox1.Image = GetBitmap();
            
        }

        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public void PaintDraw()
        //отрисовка всех объектов
        {
            if (list.getSize() == 0)
                return;
            for (int i = 0; i < list.getSize(); i++)
            {
                list.getObj(i).print(i, gr);
            }
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                isCTRL = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                for (int j = list.getSize() - 1; j >= 0; j--)
                {
                    ((CCircle)list.getObj(j)).deleteSelected(list);
                }
                PaintAll();
            }
            else isCTRL = false;
        }
    }
}
