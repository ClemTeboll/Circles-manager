using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        Pen _greenPen;
        SolidBrush _brushForRedCircle;
        SolidBrush _brushForGreenCircle;
        Graphics _panel;
        SizeF _defaultSize;

        public Form1()
        {
            InitializeComponent();
            _redPen = new Pen(Color.Red);
            _greenPen = new Pen(Color.Green);
            _brushForRedCircle = new SolidBrush(Color.Red);
            _brushForGreenCircle = new SolidBrush(Color.Green);
            _panel = DrawSpace.CreateGraphics();
            _defaultSize = new SizeF(10, 10);
        }

        public class LinkspotUser
        {
            public float LatLngPositionX, LatLngPositionY;
            public bool isVisible;

            public LinkspotUser(
                float xPosition,
                float yPosition,
                bool visibility
            )
            {
                LatLngPositionX = xPosition;
                LatLngPositionY = yPosition;
                isVisible = visibility;
            }
        }

        float radius = 50;

        private void _placeUsers()
        {
            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                Thread.Sleep(200);
                //PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
                //RectangleF rectangle = new RectangleF(location, _defaultSize);

                List<LinkspotUser> _previousLinkspotUsersList = new List<LinkspotUser>(_linkspotUsersList);
                _previousLinkspotUsersList.Remove(linkspotUser);

                foreach (LinkspotUser previousLinkspotUser in _previousLinkspotUsersList)
                {
                    double distance = Math.Sqrt((linkspotUser.LatLngPositionX - previousLinkspotUser.LatLngPositionX) + (linkspotUser.LatLngPositionY - previousLinkspotUser.LatLngPositionY));
                    
                    //float radius = 50 * ((_previousLinkspotUsersList.Count() - 1) / 8 + 1);
                    //float radian = Convert.ToSingle(45 * Math.PI / 180);
                    //float multiplier = _previousLinkspotUsersList.Count();

                    if (distance <= radius * 2)
                    {
                        //linkspotUser.LatLngPositionX = Convert.ToSingle(previousLinkspotUser.LatLngPositionX + radius * Math.Cos(radian * (multiplier)));
                        //linkspotUser.LatLngPositionY = Convert.ToSingle(previousLinkspotUser.LatLngPositionX + radius * Math.Sin(radian * (multiplier)));


                        PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
                        RectangleF rectangle = new RectangleF(location, _defaultSize);

                        _panel.DrawEllipse(_greenPen, rectangle);
                        _panel.FillEllipse(_brushForGreenCircle, rectangle);

                        break;
                    }
                    else
                    {
                        PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
                        RectangleF rectangle = new RectangleF(location, _defaultSize);

                        _panel.DrawEllipse(_redPen, rectangle);
                        _panel.FillEllipse(_brushForRedCircle, rectangle);
                        break;
                    }
                }
            }

            

            //foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            //{
            //    PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
            //    RectangleF rectangle = new RectangleF(location, _defaultSize);

            //    var _previousLinkspotUsersList = new List<LinkspotUser>(_linkspotUsersList);
            //    _previousLinkspotUsersList.Remove(linkspotUser);

            //    foreach (LinkspotUser previousLinkspotUser in _previousLinkspotUsersList)
            //    {
            //        double distance = Math.Sqrt(Math.Pow((linkspotUser.LatLngPositionX + previousLinkspotUser.LatLngPositionX), 2) + Math.Pow((linkspotUser.LatLngPositionY - previousLinkspotUser.LatLngPositionY), 2));

            //        if (distance <= radius * 2)
            //        {
            //            linkspotUser.LatLngPositionX = previousLinkspotUser.LatLngPositionX + 10;
            //            linkspotUser.LatLngPositionY = previousLinkspotUser.LatLngPositionY + 10;

            //            _panel.DrawEllipse(_redPen, rectangle);
            //            _panel.FillEllipse(_brushForRedCircle, rectangle);
            //        }
            //    }
            //}
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
            float xInputBoxValue = float.Parse(XInputBox.Text);
            float yInputBoxValue = float.Parse(YInputBox.Text);

            if (circleListGroup.Count == 0)
            {
                Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);
                IList<Circle> list = new List<Circle>();
                list.Add(newCircle);
                CircleList circleList = new CircleList(xInputBoxValue, yInputBoxValue, list);
                circleListGroup.Add(circleList);

                _panel.DrawEllipse(_redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
                _panel.FillEllipse(_brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
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

                            if (element.x == newCircle.initialXLocation && element.y == newCircle.initalYLocation)
                            {
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

                _panel.DrawEllipse(_redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
                _panel.FillEllipse(_brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            }
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

