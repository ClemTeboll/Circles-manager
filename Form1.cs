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
            new LinkspotUser(320, 95, false, true, 320, 95),
            new LinkspotUser(305, 100, false, true, 305, 100),
            new LinkspotUser(297, 102, false, true, 297, 102),
            new LinkspotUser(315, 109, false, true, 315, 109),
            new LinkspotUser(290, 113, false, true, 290, 113),
            new LinkspotUser(330, 120, false, true, 330, 120),
            new LinkspotUser(308, 97, false, true, 308, 97),
            new LinkspotUser(300, 100, false, false, 300, 100),
            new LinkspotUser(310, 110, false, true, 310, 110),
            new LinkspotUser(300, 104, false, true, 300, 104),
            new LinkspotUser(350, 170, false, true, 350, 170),
            new LinkspotUser(380, 140, false, true, 380, 140),
            new LinkspotUser(350, 150, false, true, 350, 150),
            new LinkspotUser(396, 205, false, true, 396, 205),
            new LinkspotUser(403, 210, false, true, 403, 210),
            new LinkspotUser(388, 195, false, false, 388, 195),
            new LinkspotUser(415, 209, false, true, 415, 209),
            new LinkspotUser(398, 207, false, true, 398, 207),
            new LinkspotUser(400, 200, false, true, 400, 200),
            new LinkspotUser(408, 197, false, true, 408, 197),
            new LinkspotUser(399, 200, false, false, 399, 200),
            new LinkspotUser(410, 210, false, true, 410, 210),
            new LinkspotUser(400, 204, false, true, 400, 204),
            new LinkspotUser(370, 95, false, true, 370, 95),
            new LinkspotUser(375, 100, false, true, 375, 100),
            new LinkspotUser(367, 102, false, true, 367, 102),
            new LinkspotUser(385, 109, false, true, 385, 109),
            new LinkspotUser(360, 113, false, true, 360, 113),
            new LinkspotUser(400, 120, false, true, 400, 120),
            new LinkspotUser(378, 97, false, true, 378, 97),
            new LinkspotUser(370, 100, false, false, 370, 100),
            new LinkspotUser(380, 110, false, true, 380, 110),
            new LinkspotUser(370, 104, false, true, 370, 104)
        };


        Pen _redPen;
        Pen _greenPen;
        SolidBrush _brushForRedCircle;
        SolidBrush _brushForGreenCircle;
        Graphics _panel;
        float _defaultRadius;
        SizeF _defaultSize;

        public Form1()
        {
            InitializeComponent();
            _redPen = new Pen(Color.Red);
            _greenPen = new Pen(Color.Green);
            _brushForRedCircle = new SolidBrush(Color.Red);
            _brushForGreenCircle = new SolidBrush(Color.Green);
            _panel = DrawSpace.CreateGraphics();
            _defaultRadius = 5;
            _defaultSize = new SizeF(_defaultRadius * 2, _defaultRadius * 2);
        }

        public class LinkspotUser
        {
            public float LatLngPositionX, LatLngPositionY, initialLatLngPositionX, initialLatLngPositionY;
            public bool isMovedFromInitialLocation;
            public bool isVisible;

            public LinkspotUser(
                float xPosition,
                float yPosition,
                bool ifIsMovedFromInitialLocation,
                bool visibility,
                float thisInitialLatLngPositionX,
                float thisInitialLatLngPositionY
            )
            {
                LatLngPositionX = xPosition;
                LatLngPositionY = yPosition;
                isMovedFromInitialLocation = ifIsMovedFromInitialLocation;
                initialLatLngPositionX = thisInitialLatLngPositionX;
                initialLatLngPositionY = thisInitialLatLngPositionY;
                isVisible = visibility;
            }
        }

        public class UserProfileList
        {
            public float x, y;
            public IList<LinkspotUser> list;

            public UserProfileList(
                float XInitialLocation,
                float YInitialLocation,
                IList<LinkspotUser> thisUserProfileList
            )
            {
                x = XInitialLocation;
                y = YInitialLocation;
                list = thisUserProfileList;
            }

        }

        IList<UserProfileList> userProfileListGroup = new List<UserProfileList>();
        
        float degrees = 45;
        float defaultRadiusMultiplier = 3;

        private void _calculateUserPositionInACircle(LinkspotUser thisUser)
        {
            foreach (UserProfileList userProfileList in userProfileListGroup.ToList())
            {
                double newDistance = Math.Sqrt(Math.Pow((thisUser.LatLngPositionX - userProfileList.x), 2) + Math.Pow((thisUser.LatLngPositionY - userProfileList.y), 2));
                float floatedNewDistance = Convert.ToSingle(newDistance);


                if (floatedNewDistance < _defaultRadius * 4)
                {
                    float radian = Convert.ToSingle(degrees * Math.PI / 180);
                    float cosinusSinusMultiplier = userProfileList.list.Count();

                    thisUser.LatLngPositionX = Convert.ToSingle(userProfileList.x + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * (cosinusSinusMultiplier)));
                    thisUser.LatLngPositionY = Convert.ToSingle(userProfileList.y + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * (cosinusSinusMultiplier)));
                    thisUser.isMovedFromInitialLocation = true;

                //    userProfileList.list.Add(thisUser);
                //}
                //else
                //{
                //    IList<LinkspotUser> list = new List<LinkspotUser>();
                //    list.Add(thisUser);
                //    UserProfileList profileList = new UserProfileList(thisUser.LatLngPositionX, thisUser.LatLngPositionY, list);
                //    userProfileListGroup.Add(profileList);
                }
            }
        }

        private void _changeUsersPosition()
        {
            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                List<LinkspotUser> _previousLinkspotUsersList = new List<LinkspotUser>(_linkspotUsersList);
                _previousLinkspotUsersList.Remove(linkspotUser);

                IList<LinkspotUser> list = new List<LinkspotUser>();
                UserProfileList profileList = new UserProfileList(0, 0, null);

                foreach (LinkspotUser previousLinkspotUser in _previousLinkspotUsersList)
                {
                    double distance = Math.Sqrt(Math.Pow((linkspotUser.LatLngPositionX - previousLinkspotUser.LatLngPositionX), 2) + Math.Pow((linkspotUser.LatLngPositionY - previousLinkspotUser.LatLngPositionY), 2));
                    float floatedDistance = Convert.ToSingle(distance);

                    //if (userProfileListGroup.Count() == 0)
                    //{
                    //    list.Add(previousLinkspotUser);
                    //    UserProfileList userProfileList = new UserProfileList(previousLinkspotUser.LatLngPositionX, previousLinkspotUser.LatLngPositionY, list);
                    //    userProfileListGroup.Add(userProfileList);
                    //    continue;
                    //}
                    //else
                    if (floatedDistance <= _defaultRadius * 4)
                    {
                        _calculateUserPositionInACircle(previousLinkspotUser);
                        list.Add(previousLinkspotUser);
                        profileList.x = previousLinkspotUser.LatLngPositionX;
                        profileList.y = previousLinkspotUser.LatLngPositionY;
                        profileList.list = list;
                        userProfileListGroup.Add(profileList);
                    }
                }
                continue;
            }
        }

        private void _drawUsers()
        {
            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                //Thread.Sleep(400);

                PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
                RectangleF rectangle = new RectangleF(location, _defaultSize);

                _panel.DrawEllipse(_greenPen, rectangle);
                _panel.FillEllipse(_brushForGreenCircle, rectangle);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _changeUsersPosition();
            _drawUsers();
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

