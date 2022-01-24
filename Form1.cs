using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Circle
        {
            public int x, y, width, height, initialXLocation, initalYLocation;
            public bool isMovedFromInitialLocation;

            public Circle(int thisX, int thisY, int thisWidth, int thisHeight, bool ifIsMovedFromInitialLocation, int thisInitialXLocation, int thisInitalYLocation)
            {
                x = thisX;
                y = thisY;
                width = thisWidth;
                height = thisHeight;
                isMovedFromInitialLocation = ifIsMovedFromInitialLocation;
                initialXLocation = thisInitialXLocation;
                initalYLocation = thisInitalYLocation;
            }
        }

        IList<Circle> circlesList = new List<Circle>();

        private void BtnCreateCircle_Click(object sender, EventArgs e)
        {
            const int radius = 20;
            const double radian = 45 * Math.PI / 180;

            int xInputBoxValue = Int32.Parse(XInputBox.Text);
            int yInputBoxValue = Int32.Parse(YInputBox.Text);

            Graphics panel = DrawSpace.CreateGraphics();
            Pen redPen = new Pen(Color.Red);
            SolidBrush brushForRedCircle = new SolidBrush(Color.Red);

            if (circlesList.Count == 0)
            {
                Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);
                circlesList.Add(newCircle);

                panel.DrawEllipse(redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
                panel.FillEllipse(brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            }
            else
            {
                Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);

                for (int i = 0; i < circlesList.Count; i++)
                {
                    if (circlesList[i].x == newCircle.x && circlesList[i].y == newCircle.y)
                    {
                        int newX = circlesList[i].x + radius * Convert.ToInt32(Math.Cos(radian * (i + 1)));
                        int newY = circlesList[i].y + radius * Convert.ToInt32(Math.Cos(radian * (i + 1)));

                        newCircle.x = newX;
                        newCircle.y = newY;
                        newCircle.isMovedFromInitialLocation = true;

                        circlesList.Add(newCircle);
                        break;
                    }
                }

                panel.DrawEllipse(redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
                panel.FillEllipse(brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            }
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }     
}
