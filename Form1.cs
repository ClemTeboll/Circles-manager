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

        public class CircleList
        {
            public float x, y;
            public IList<Circle> list;

            public CircleList(
                float XInitialLocation,
                float YInitialLocation,
                IList<Circle> thisCircleList
            )
            {
                x = XInitialLocation;
                y = YInitialLocation;
                list = thisCircleList;
            }

        }

        IList<CircleList> circleListGroup = new List<CircleList>();

        private void BtnCreateCircle_Click(object sender, EventArgs e)
        {
            // Récupération des données des inputs
            float xInputBoxValue = float.Parse(XInputBox.Text);
            float yInputBoxValue = float.Parse(YInputBox.Text);


            // Paramétrage de la création des cercles et du panel pour les afficher
            Pen redPen = new Pen(Color.Red);
            SolidBrush brushForRedCircle = new SolidBrush(Color.Red);
            Graphics panel = DrawSpace.CreateGraphics();


            if (circleListGroup.Count == 0)
            {
                Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);
                IList<Circle> list = new List<Circle>();
                list.Add(newCircle);
                CircleList circleList = new CircleList(xInputBoxValue, yInputBoxValue, list);
                circleListGroup.Add(circleList);
                
                panel.DrawEllipse(redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
                panel.FillEllipse(brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            }
            else
            {
                Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);

                for (int i = 0; i < circleListGroup.Count(); i++)
                {
                    if (circleListGroup[i].x == newCircle.x && circleListGroup[i].y == newCircle.y)
                    {
                        foreach (Circle element in circleListGroup[i].list)
                        {
                            if (element.x == newCircle.initialXLocation && element.y == newCircle.initalYLocation)
                            {
                                float circleListCount = circleListGroup[i].list.Count();

                                float degrees = 45;
                                //float multiple = 8;
                                float radius = 50 * ((circleListGroup[i].list.Count() - 1) / 8 + 1);
                                //if (circleListCount - 1 % multiple == 0)
                                //{
                                //    radius = 50 * ((circleListCount - 1) / (degrees / multiple) + 1);
                                //}
                                float radian = Convert.ToSingle(degrees * Math.PI / 180);
                                float multiplier = circleListCount;

                                float newX = Convert.ToSingle(element.x + radius * Math.Cos(radian * (multiplier)));
                                float newY = Convert.ToSingle(element.y + radius * Math.Sin(radian * (multiplier)));

                                newCircle.x = newX;
                                newCircle.y = newY;
                                newCircle.isMovedFromInitialLocation = true;

                                circleListGroup[i].list.Add(newCircle);
                                break;
                            }
                        }
                    }
                    
                }

                IList<Circle> list = new List<Circle>();
                list.Add(newCircle);
                CircleList circleList = new CircleList(xInputBoxValue, yInputBoxValue, list);
                circleListGroup.Add(circleList);
                
                panel.DrawEllipse(redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
                panel.FillEllipse(brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            }
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

