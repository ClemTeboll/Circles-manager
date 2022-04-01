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
            new LinkspotUser(320, 96, false, true, false, 320, 96)
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
        
        float degrees = 45;
        float defaultRadiusMultiplier = 3;

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

