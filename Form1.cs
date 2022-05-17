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
            new LinkspotUser(320, 95, false, true, false, 320, 95),
            new LinkspotUser(315, 69, false, true, false, 315, 69),
            new LinkspotUser(326, 75, false, true, false, 326, 75),
            new LinkspotUser(329, 110, false, true, false, 329, 110),
            new LinkspotUser(318, 97, false, true, false, 318, 97),
            new LinkspotUser(322, 110, false, true, false, 322, 110),
            new LinkspotUser(328, 90, false, true, false, 328, 90),
            new LinkspotUser(323, 104, false, true, false, 323, 104),
            new LinkspotUser(320, 96, false, true, false, 320, 96)
        };

        LinkspotUser _currentLinkspotUser = new LinkspotUser(320, 95, false, true, true, 320, 95); // L'utilisateur principal, en train d'utiliser l'appli.
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
        
        float degrees = 45;
        float defaultRadiusMultiplier = 3;


        private void isCollidingWithFinalUsers(LinkspotUser user)
        {
            foreach (LinkspotUser finalLinkspotUser in _finalLinkspotUsersList)
            {
                double distanceToFinalLinkspotUser = Math.Sqrt(Math.Pow((finalLinkspotUser.LatLngPositionX - user.LatLngPositionX), 2) + Math.Pow((finalLinkspotUser.LatLngPositionY - user.LatLngPositionY), 2));
                float floatedDistanceToFinalLinkspotUser = Convert.ToSingle(distanceToFinalLinkspotUser);

                if (floatedDistanceToFinalLinkspotUser <= _defaultRadius * 2)
                {
                    float radian = Convert.ToSingle(degrees * Math.PI / 180);

                    user.LatLngPositionX = Convert.ToSingle(user.LatLngPositionX + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * 1));
                    user.LatLngPositionY = Convert.ToSingle(user.LatLngPositionY + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * 1));
                }
            }
        }

        private void isCollidingWithUsers(LinkspotUser user)
        {
            foreach (LinkspotUser LastlinkspotUser in _linkspotUsersList)
            {
                double distanceToLastLinkspotUser = Math.Sqrt(Math.Pow((LastlinkspotUser.LatLngPositionX - user.LatLngPositionX), 2) + Math.Pow((LastlinkspotUser.LatLngPositionY - user.LatLngPositionY), 2));
                float floatedDistanceToLastLinkspotUser = Convert.ToSingle(distanceToLastLinkspotUser);

                if (floatedDistanceToLastLinkspotUser <= _defaultRadius * 2)
                {
                    float radian = Convert.ToSingle(degrees * Math.PI / 180);

                    user.LatLngPositionX = Convert.ToSingle(user.LatLngPositionX + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * 1));
                    user.LatLngPositionY = Convert.ToSingle(user.LatLngPositionY + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * 1));
                    //lastComparison(user);
                    isCollidingWithFinalUsers(user);
                }
            }
        }

        private void lastComparison(LinkspotUser linkspotUser)
        {
            isCollidingWithFinalUsers(linkspotUser);
            isCollidingWithUsers(linkspotUser);
            _finalLinkspotUsersList.Add(linkspotUser);
        }

        private void _changeUsersPosition()
        {
            _finalLinkspotUsersList.Add(_currentLinkspotUser);

            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                double distanceToCurrentLinkspotUser = Math.Sqrt(Math.Pow((_currentLinkspotUser.LatLngPositionX - linkspotUser.LatLngPositionX), 2) + Math.Pow((_currentLinkspotUser.LatLngPositionY - linkspotUser.LatLngPositionY), 2));
                float floatedDistanceToCurrentLinkspotUser = Convert.ToSingle(distanceToCurrentLinkspotUser);

                if (floatedDistanceToCurrentLinkspotUser <= _defaultRadius * 2 && _finalLinkspotUsersList.Count() >= 1)
                {
                    float radian = Convert.ToSingle(degrees * Math.PI / 180);
                    float cosinusSinusMultiplier = _finalLinkspotUsersList.Count();

                    linkspotUser.LatLngPositionX = Convert.ToSingle(linkspotUser.LatLngPositionX + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * (cosinusSinusMultiplier + 1)));
                    linkspotUser.LatLngPositionY = Convert.ToSingle(linkspotUser.LatLngPositionY + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * (cosinusSinusMultiplier + 1)));
                    linkspotUser.isSimilarToCurrentUser = true;

                    _finalLinkspotUsersList.Add(linkspotUser);
                }
            }

            foreach (LinkspotUser _finalLinkspotUser in _finalLinkspotUsersList)
            {
                _linkspotUsersList.Remove(_finalLinkspotUser);
            }

            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                lastComparison(linkspotUser);
            }

            //foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            //{
            //    foreach (LinkspotUser _finalLinkspotUser in _finalLinkspotUsersList)
            //    {
            //        double distanceToComparedLinkspotUser = Math.Sqrt(Math.Pow((_finalLinkspotUser.LatLngPositionX - linkspotUser.LatLngPositionX), 2) + Math.Pow((_finalLinkspotUser.LatLngPositionY - linkspotUser.LatLngPositionY), 2));
            //        float floatedDistanceToFinalLinkspotUser = Convert.ToSingle(distanceToComparedLinkspotUser);

            //        if (floatedDistanceToFinalLinkspotUser <= _defaultRadius * 4)
            //        {
            //            float radian = Convert.ToSingle(degrees * Math.PI / 180);
            //            linkspotUser.LatLngPositionX = Convert.ToSingle(linkspotUser.LatLngPositionX + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * -1));
            //            linkspotUser.LatLngPositionY = Convert.ToSingle(linkspotUser.LatLngPositionY + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * -1));
            //        }
            //    }

            //    _finalLinkspotUsersList.Add(linkspotUser);
            //    Console.WriteLine(_finalLinkspotUsersList);
            //}

            Console.WriteLine(_finalLinkspotUsersList);
        }

        // AFFICHAGE DES PROFILS SUR LE PANEL
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

        // NE PAS UTILISER
        private void BtnCreateCircle_Click(object sender, EventArgs e)
        {}

        public void panel1_Paint(object sender, PaintEventArgs e)
        {}
    }
}

