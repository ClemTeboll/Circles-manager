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
        List<LinkspotUser> _linkspotUsersList = new List<LinkspotUser> {
            new LinkspotUser(320, 95, true),
            new LinkspotUser(305, 100, true),
            new LinkspotUser(297, 102, true),
            new LinkspotUser(315, 109, true),
            new LinkspotUser(290, 113, true),
            new LinkspotUser(330, 120, true),
            new LinkspotUser(308, 97, true),
            new LinkspotUser(300, 100, false),
            new LinkspotUser(310, 110, true),
            new LinkspotUser(300, 104, true),
            new LinkspotUser(350, 170, true),
            new LinkspotUser(380, 140, true),
            new LinkspotUser(350, 150, true),
            new LinkspotUser(396, 205, true),
            new LinkspotUser(403, 210, true),
            new LinkspotUser(388, 195, false),
            new LinkspotUser(415, 209, true),
            new LinkspotUser(398, 207, true),
            new LinkspotUser(400, 200, true),
            new LinkspotUser(408, 197, true),
            new LinkspotUser(399, 200, false),
            new LinkspotUser(410, 210, true),
            new LinkspotUser(400, 204, true),
            new LinkspotUser(370, 95, true),
            new LinkspotUser(375, 100, true),
            new LinkspotUser(367, 102, true),
            new LinkspotUser(385, 109, true),
            new LinkspotUser(360, 113, true),
            new LinkspotUser(400, 120, true),
            new LinkspotUser(378, 97, true),
            new LinkspotUser(370, 100, false),
            new LinkspotUser(380, 110, true),
            new LinkspotUser(370, 104, true),
        };

        Pen _redPen;
        SolidBrush _brushForRedCircle;
        Graphics _panel;
        SizeF _defaultSize;

        public Form1()
        {
            InitializeComponent();
            _redPen = new Pen(Color.Red);
            _brushForRedCircle = new SolidBrush(Color.Red);
            _panel = DrawSpace.CreateGraphics();
            _defaultSize = new SizeF(10, 10);
        }

        public class LinkspotUser
        {
            public float LatLngPositionX, LatLngPositionY;
            public bool isVisible;

            public LinkspotUser(
                float posX,
                float pozY,
                bool visibility
            )
            {
                LatLngPositionX = posX;
                LatLngPositionY = pozY;
                isVisible = visibility;
            }
        }

        private void _placeUsers()
        {
            _linkspotUsersList.ForEach((LinkspotUser user) =>
            {
                Thread.Sleep(200);
                PointF loc = new PointF(user.LatLngPositionX, user.LatLngPositionY);
                RectangleF rect = new RectangleF(loc, _defaultSize);

                _panel.DrawEllipse(_redPen, rect);
                _panel.FillEllipse(_brushForRedCircle, rect);
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _placeUsers();
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

