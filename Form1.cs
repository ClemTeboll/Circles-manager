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
            new LinkspotUser(330, 120, false, true, false, 330, 120),
            new LinkspotUser(308, 97, false, true, false, 308, 97),
            new LinkspotUser(300, 100, false, true, false, 300, 100),
            new LinkspotUser(310, 110, false, true, false, 310, 110),
            new LinkspotUser(300, 104, false, true, false, 300, 104),
            //new LinkspotUser(320, 96, false, true, false, 320, 96)
        };

        LinkspotUser _currentLinkspotUser = new LinkspotUser(320, 95, false, true, true, 320, 95); // L'utilisateur principal, en train d'utiliser l'appli.
        List<LinkspotUser> currentUserlist = new List<LinkspotUser>();
        List<LinkspotUser> _finalLinkspotUsersList = new List<LinkspotUser>(); // Liste où seront copiés tous les profils et dont se servira la fonction _drawUsers

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
            public bool isSimilarToCurrentUser, isVisible, isCurrentUser;

            public LinkspotUser(
                float xPosition,
                float yPosition,
                bool ifIsSimilarToCurrentUser,
                bool visibility,
                bool ifIsCurrentUser,
                float thisInitialLatLngPositionX,
                float thisInitialLatLngPositionY
            )
            {
                LatLngPositionX = xPosition;
                LatLngPositionY = yPosition;
                isSimilarToCurrentUser = ifIsSimilarToCurrentUser;
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
            currentUserlist.Add(_currentLinkspotUser);

            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                double distanceToCurrentLinkspotUser = Math.Sqrt(Math.Pow((_currentLinkspotUser.LatLngPositionX - linkspotUser.LatLngPositionX), 2) + Math.Pow((_currentLinkspotUser.LatLngPositionY - linkspotUser.LatLngPositionY), 2));
                float floatedDistanceToCurrentLinkspotUser = Convert.ToSingle(distanceToCurrentLinkspotUser);

                if (floatedDistanceToCurrentLinkspotUser <= _defaultRadius * 2)
                {
                    if (currentUserlist.Count() >= 1)
                    {
                        float radian = Convert.ToSingle(degrees * Math.PI / 180);
                        float cosinusSinusMultiplier = currentUserlist.Count();

                        linkspotUser.LatLngPositionX = Convert.ToSingle(linkspotUser.LatLngPositionX + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * (cosinusSinusMultiplier + 1)));
                        linkspotUser.LatLngPositionY = Convert.ToSingle(linkspotUser.LatLngPositionY + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * (cosinusSinusMultiplier + 1)));
                        linkspotUser.isSimilarToCurrentUser = true;
                    }

                    currentUserlist.Add(linkspotUser);
                }

                //List<LinkspotUser> _comparedLinkspotUsersList = new List<LinkspotUser>(_linkspotUsersList);
                //_comparedLinkspotUsersList.Insert(0, _currentLinkspotUser);
                //List<LinkspotUser> list = new List<LinkspotUser>();


                //foreach (LinkspotUser _comparedLinkspotUser in _comparedLinkspotUsersList)
                //{
                //    if (_comparedLinkspotUser.isCurrentUser || _comparedLinkspotUser.isSimilarToCurrentUser)
                //    {
                //        continue;
                //    }
                //    else
                //    {
                //        double distanceToComparedLinkspotUser = Math.Sqrt(Math.Pow((linkspotUser.LatLngPositionX - _comparedLinkspotUser.LatLngPositionX), 2) + Math.Pow((linkspotUser.LatLngPositionY - _comparedLinkspotUser.LatLngPositionY), 2));
                //        float floatedDistanceToComparedLinkspotUser = Convert.ToSingle(distanceToComparedLinkspotUser);

                //        if (floatedDistanceToComparedLinkspotUser <= _defaultRadius * 2)
                //        {
                //            float radian = Convert.ToSingle(degrees * Math.PI / 180);
                //            float cosinusSinusMultiplier = list.Count();

                //            _comparedLinkspotUser.LatLngPositionX = Convert.ToSingle(linkspotUser.LatLngPositionX + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * (cosinusSinusMultiplier)));
                //            _comparedLinkspotUser.LatLngPositionY = Convert.ToSingle(linkspotUser.LatLngPositionY + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * (cosinusSinusMultiplier)));
                //        }
                //    }
                //}

                
            }

            foreach (LinkspotUser similarLinkspotUser in currentUserlist)
            {
                _finalLinkspotUsersList.Add(similarLinkspotUser);
                _linkspotUsersList.Remove(similarLinkspotUser);
            }

            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                List<LinkspotUser> _comparedLinkspotUsersList = new List<LinkspotUser>(_finalLinkspotUsersList);


                foreach (LinkspotUser _comparedLinkspotUser in _comparedLinkspotUsersList)
                {
                    double distanceToComparedLinkspotUser = Math.Sqrt(Math.Pow((_comparedLinkspotUser.LatLngPositionX - linkspotUser.LatLngPositionX), 2) + Math.Pow((_comparedLinkspotUser.LatLngPositionY - linkspotUser.LatLngPositionY), 2));
                    float floatedDistanceToFinalLinkspotUser = Convert.ToSingle(distanceToComparedLinkspotUser);

                    if (floatedDistanceToFinalLinkspotUser <= _defaultRadius * 4)
                    {
                        float radian = Convert.ToSingle(degrees * Math.PI / 180);
                        linkspotUser.LatLngPositionX = Convert.ToSingle(linkspotUser.LatLngPositionX + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * -1));
                        linkspotUser.LatLngPositionY = Convert.ToSingle(linkspotUser.LatLngPositionY + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * -1));
                    }

                }

                Console.WriteLine(linkspotUser);
                _finalLinkspotUsersList.Add(linkspotUser);

            }

            Console.WriteLine(_finalLinkspotUsersList);
        }

        private void _drawUsers()
        {
            foreach (LinkspotUser linkspotUser in _finalLinkspotUsersList)
            {
                if (linkspotUser.isCurrentUser)
                {
                    PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
                    RectangleF rectangle = new RectangleF(location, _defaultSize);

                    _panel.DrawEllipse(_redPen, rectangle);
                    _panel.FillEllipse(_brushForRedCircle, rectangle);
                }
                else
                {
                    PointF location = new PointF(linkspotUser.LatLngPositionX, linkspotUser.LatLngPositionY);
                    RectangleF rectangle = new RectangleF(location, _defaultSize);

                    _panel.DrawEllipse(_greenPen, rectangle);
                    _panel.FillEllipse(_brushForGreenCircle, rectangle);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            _changeUsersPosition();
            _drawUsers();
        }

        private void BtnCreateCircle_Click(object sender, EventArgs e)
        {}

        public void panel1_Paint(object sender, PaintEventArgs e)
        {}
    }
}

