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
            new LinkspotUser(320, 95, false),
            new LinkspotUser(320, 95, false),
            new LinkspotUser(315, 100, false),
            new LinkspotUser(320, 95, false),
            new LinkspotUser(315, 100, false),
            new LinkspotUser(331, 96, false),
            new LinkspotUser(330, 94, false),
            new LinkspotUser(331, 98, false),
            new LinkspotUser(331, 50, false)
        };

        LinkspotUser _currentLinkspotUser = new LinkspotUser(320, 95, true);
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
            public float LatLngPositionX, LatLngPositionY;
            public bool isCurrentUser;

            public LinkspotUser(
                float xPosition,
                float yPosition,
                bool ifIsCurrentUser
            )
            {
                LatLngPositionX = xPosition;
                LatLngPositionY = yPosition;
                isCurrentUser = ifIsCurrentUser;
            }
        }
        
        float degrees = 45;
        float defaultRadiusMultiplier = 3;

        private float collisionMath(LinkspotUser _baseLinkspotUser, LinkspotUser _comparedLinkspotUser)
        {
            double distanceToComparedLinkspotUser = Math.Sqrt(Math.Pow((_baseLinkspotUser.LatLngPositionX - _comparedLinkspotUser.LatLngPositionX), 2) + Math.Pow((_baseLinkspotUser.LatLngPositionY - _comparedLinkspotUser.LatLngPositionY), 2));
            float floatedDistanceToFinalLinkspotUser = Convert.ToSingle(distanceToComparedLinkspotUser);

            return floatedDistanceToFinalLinkspotUser;
        }

        private void modifyUserCoordinates(LinkspotUser _baseLinkspotUser, LinkspotUser linkspotUser, float radianMultiplier)
        {
            float radian = Convert.ToSingle(degrees * Math.PI / 180);
            linkspotUser.LatLngPositionX = Convert.ToSingle(_baseLinkspotUser.LatLngPositionX + (_defaultRadius * defaultRadiusMultiplier) * Math.Cos(radian * radianMultiplier));
            linkspotUser.LatLngPositionY = Convert.ToSingle(_baseLinkspotUser.LatLngPositionY + (_defaultRadius * defaultRadiusMultiplier) * Math.Sin(radian * radianMultiplier));
        }

        private void checkFinalLinkspotUserList(LinkspotUser linkspotUser)
        {
            foreach (LinkspotUser _finalLinkspotUser in _finalLinkspotUsersList)
            {
                if (collisionMath(_finalLinkspotUser, linkspotUser) <= _defaultRadius * 2)
                {
                    modifyUserCoordinates(_finalLinkspotUser, linkspotUser, -1);
                    checkFinalLinkspotUserList(linkspotUser);
                }
            }
        }

        private void _changeUsersPosition()
        {
            _finalLinkspotUsersList.Add(_currentLinkspotUser);

            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                if (collisionMath(_currentLinkspotUser, linkspotUser) <= _defaultRadius * 2)
                {
                    modifyUserCoordinates(_currentLinkspotUser, linkspotUser, _finalLinkspotUsersList.Count() + 1);
                    _finalLinkspotUsersList.Add(linkspotUser);
                }
            }

            foreach (LinkspotUser _finalLinkspotUser in _finalLinkspotUsersList)
            {
                _linkspotUsersList.Remove(_finalLinkspotUser);
            }

            foreach (LinkspotUser linkspotUser in _linkspotUsersList)
            {
                checkFinalLinkspotUserList(linkspotUser);
                _finalLinkspotUsersList.Add(linkspotUser);
            }
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

        
        // APPEL DES FONCTIONS AU CLIC
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

