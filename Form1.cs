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
            public float x, y, width, height, initialXLocation, initalYLocation;
            public bool isMovedFromInitialLocation;

            public Circle(
                float thisX,
                float thisY,
                float thisWidth,
                float thisHeight, 
                bool ifIsMovedFromInitialLocation,
                float thisInitialXLocation,
                float thisInitalYLocation
            )
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

        readonly IList<Circle> circlesList = new List<Circle>();

        private void BtnCreateCircle_Click(object sender, EventArgs e)
        {
            const float radius = 50;
            float radian = Convert.ToSingle(45 * Math.PI / 180);

            float xInputBoxValue = float.Parse(XInputBox.Text);
            float yInputBoxValue = float.Parse(YInputBox.Text);

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
                        IList<Circle> circlesWithIdenticalInitialCoordinatesList = new List<Circle>();

                        for (int j = 0; j < circlesList.Count; j++)
                        {
                            if (circlesList[j].initialXLocation == newCircle.initialXLocation && circlesList[j].initalYLocation == newCircle.initalYLocation)
                            {
                                circlesWithIdenticalInitialCoordinatesList.Add(circlesList[j]);
                            }
                        }

                        float multiplier = circlesWithIdenticalInitialCoordinatesList.Count();

                        float newX = Convert.ToSingle(circlesList[i].x + radius * Math.Cos(radian * (multiplier)));
                        float newY = Convert.ToSingle(circlesList[i].y + radius * Math.Sin(radian * (multiplier)));

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
