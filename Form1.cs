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
            new LinkspotUser(320, 95, false, true, false, 297, 102),
            new LinkspotUser(320, 95, false, true, false, 315, 109),
            new LinkspotUser(320, 95, false, true, false, 290, 113),
            new LinkspotUser(320, 95, false, true, false, 330, 120),
            new LinkspotUser(320, 95, false, true, false, 308, 97),
            new LinkspotUser(320, 95, false, true, false, 300, 100),
            new LinkspotUser(320, 95, false, true, false, 310, 110),
            new LinkspotUser(320, 95, false, true, false, 300, 104),
            new LinkspotUser(320, 95, false, true, true, 320, 95)
        };

        LinkspotUser currentLinkspotUser = new LinkspotUser(320, 95, false, true, true, 305, 100);

        List<LinkspotUser> _finalLinkspotUsersList = new List<LinkspotUser>();

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
            _greenPen = new Pen(Color.Green);
            _brushForGreenCircle = new SolidBrush(Color.Green);
            _redPen = new Pen(Color.Red);
            _brushForRedCircle = new SolidBrush(Color.Red);
            _panel = DrawSpace.CreateGraphics();
            _defaultRadius = 5;
            _defaultSize = new SizeF(_defaultRadius * 2, _defaultRadius * 2);
        }

        public class LinkspotUser
        {
            public float LatLngPositionX, LatLngPositionY, initialLatLngPositionX, initialLatLngPositionY;
            public bool isMovedFromInitialLocation, isVisible, isCurrentUser;

            public LinkspotUser(
                float xPosition,
                float yPosition,
                bool ifIsMovedFromInitialLocation,
                bool visibility,
                bool ifIsCurrentUser,
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
                isCurrentUser = ifIsCurrentUser;
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

        //private void _calculateUserPositionInACircle(LinkspotUser thisUser)
        //{

        //    foreach (UserProfileList userProfileList in userProfileListGroup)
        //    {
        //        double distanceToUserProfile = Math.Sqrt(Math.Pow((thisUser.LatLngPositionX - userProfileList.x), 2) + Math.Pow((thisUser.LatLngPositionY - userProfileList.y), 2));
        //        float floatedDistanceToUserProfile = Convert.ToSingle(distanceToUserProfile);

        //        if (floatedDistanceToUserProfile <= _defaultRadius * 4)
        //        {
        //            float radian = Convert.ToSingle(degrees * Math.PI / 180);
        //            float cosinusSinusMultiplier = userProfileList.list.Count();

        //            thisUser.LatLngPositionX = Convert.ToSingle(userProfileList.x + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * (cosinusSinusMultiplier)));
        //            thisUser.LatLngPositionY = Convert.ToSingle(userProfileList.y + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * (cosinusSinusMultiplier)));
        //            thisUser.isMovedFromInitialLocation = true;
        //        }
        //    }     
        //}


        //private void _changeUsersPosition()
        //{
        //    //_linkspotUsersList.Insert(0, currentLinkspotUser);
        //    //Console.WriteLine(_linkspotUsersList);

        //    foreach (LinkspotUser linkspotUser in _linkspotUsersList)
        //    {
        //        List<LinkspotUser> _comparedLinkspotUsersList = new List<LinkspotUser>(_linkspotUsersList);
        //        _comparedLinkspotUsersList.Remove(linkspotUser);

        //        IList<LinkspotUser> list = new List<LinkspotUser>();
        //        UserProfileList profileList = new UserProfileList(0, 0, null);

        //        foreach (LinkspotUser _comparedLinkspotUser in _comparedLinkspotUsersList)
        //        {
        //            double distanceToComparedLinkspotUser = Math.Sqrt(Math.Pow((linkspotUser.LatLngPositionX - _comparedLinkspotUser.LatLngPositionX), 2) + Math.Pow((linkspotUser.LatLngPositionY - _comparedLinkspotUser.LatLngPositionY), 2));
        //            float floatedDistanceToComparedLinkspotUser = Convert.ToSingle(distanceToComparedLinkspotUser);

        //            if (floatedDistanceToComparedLinkspotUser <= _defaultRadius * 2)
        //            {
        //                _calculateUserPositionInACircle(_comparedLinkspotUser);
        //                list.Add(_comparedLinkspotUser);
        //                profileList.x = _comparedLinkspotUser.LatLngPositionX;
        //                profileList.y = _comparedLinkspotUser.LatLngPositionY;
        //                profileList.list = list;
        //                userProfileListGroup.Add(profileList);
        //            }
        //        }
        //    }
        //}

        //private void _drawUsers()
        //{
        //    Console.WriteLine(_linkspotUsersList);

        //    foreach (LinkspotUser linkspotUser in _linkspotUsersList)
        //    {
        //        if (linkspotUser.isCurrentUser)
        //        {
        //            PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
        //            RectangleF rectangle = new RectangleF(location, _defaultSize);

        //            _panel.DrawEllipse(_redPen, rectangle);
        //            _panel.FillEllipse(_brushForRedCircle, rectangle);
        //        }
        //        else
        //        {
        //            PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
        //            RectangleF rectangle = new RectangleF(location, _defaultSize);

        //            _panel.DrawEllipse(_greenPen, rectangle);
        //            _panel.FillEllipse(_brushForGreenCircle, rectangle);
        //        }
        //    }
        //}

        private void _changeUsersPosition()
        {

        }

        private void _drawUsers()
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            _changeUsersPosition();
            _drawUsers();
        }

        //public class Circle
        //{
        //    public float x, y, width, height, initialXLocation, initalYLocation;
        //    public bool isMovedFromInitialLocation;

        //    public Circle(
        //        float thisX,
        //        float thisY,
        //        float thisWidth,
        //        float thisHeight,
        //        bool ifIsMovedFromInitialLocation,
        //        float thisInitialXLocation,
        //        float thisInitalYLocation
        //    )
        //    {
        //        x = thisX;
        //        y = thisY;
        //        width = thisWidth;
        //        height = thisHeight;
        //        isMovedFromInitialLocation = ifIsMovedFromInitialLocation;
        //        initialXLocation = thisInitialXLocation;
        //        initalYLocation = thisInitalYLocation;
        //    }
        //}

        //public class CircleList
        //{
        //    public float x, y;
        //    public IList<Circle> list;

        //    public CircleList(
        //        float XInitialLocation,
        //        float YInitialLocation,
        //        IList<Circle> thisCircleList
        //    )
        //    {
        //        x = XInitialLocation;
        //        y = YInitialLocation;
        //        list = thisCircleList;
        //    }

        //}

        //IList<CircleList> circleListGroup = new List<CircleList>();

        private void BtnCreateCircle_Click(object sender, EventArgs e)
        {
            //float xInputBoxValue = float.Parse(XInputBox.Text);
            //float yInputBoxValue = float.Parse(YInputBox.Text);

            //if (circleListGroup.Count == 0)
            //{
            //    Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);
            //    IList<Circle> list = new List<Circle>();
            //    list.Add(newCircle);
            //    CircleList circleList = new CircleList(xInputBoxValue, yInputBoxValue, list);
            //    circleListGroup.Add(circleList);

            //    _panel.DrawEllipse(_redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            //    _panel.FillEllipse(_brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            //}
            //else
            //{
            //    Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);

            //    for (int i = 0; i < circleListGroup.Count(); i++)
            //    {
            //        if (circleListGroup[i].x == newCircle.x && circleListGroup[i].y == newCircle.y)
            //        {
            //            foreach (Circle element in circleListGroup[i].list)
            //            {
            //                float circleListCount = circleListGroup[i].list.Count();

            //                float degrees = 45;
            //                //float multiple = 8;
            //                float radius = 50 * ((circleListGroup[i].list.Count() - 1) / 8 + 1);
            //                //if (circleListCount - 1 % multiple == 0)
            //                //{
            //                //    radius = 50 * ((circleListCount - 1) / (degrees / multiple) + 1);
            //                //}
            //                float radian = Convert.ToSingle(degrees * Math.PI / 180);
            //                float multiplier = circleListCount;

            //                if (element.x == newCircle.initialXLocation && element.y == newCircle.initalYLocation)
            //                {
            //                    float newX = Convert.ToSingle(element.x + radius * Math.Cos(radian * (multiplier)));
            //                    float newY = Convert.ToSingle(element.y + radius * Math.Sin(radian * (multiplier)));

            //                    newCircle.x = newX;
            //                    newCircle.y = newY;
            //                    newCircle.isMovedFromInitialLocation = true;

            //                    circleListGroup[i].list.Add(newCircle);
            //                    break;
            //                }

            //            }
            //        }
            //    }
            //    IList<Circle> list = new List<Circle>();
            //    list.Add(newCircle);
            //    CircleList circleList = new CircleList(xInputBoxValue, yInputBoxValue, list);
            //    circleListGroup.Add(circleList);

            //    _panel.DrawEllipse(_redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            //    _panel.FillEllipse(_brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            //}
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

