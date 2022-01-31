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


        private void BtnCreateCircle_Click(object sender, EventArgs e)
        {

            // Création d'un dictionnaire qui contiendra, en clé : les coordonnées X et Y d'un objet, et en valeur : la liste contenant tous les objets ayant ces mêmes coordonnées X et Y.
            IList<List<Circle>> CircleGroup = new List<List<Circle>>();


            // Définition des dimensions de chaque cercle
            float radius = 50 * ((circlesList.Count - 1) / 8 + 1);
            //int radius = 50;
            float degrees = 45;
            float multiple = 8;


            // Données pour le calcul de la position des cercles

            //if (circlesList.Count - 1 % multiple == 0)
            //{
            //     radius = 50 * ((circlesList.Count - 1) / (degrees / multiple) + 1);
            //}
            float radian = Convert.ToSingle(degrees * Math.PI / 180);


            // Récupération des données des inputs
            float xInputBoxValue = float.Parse(XInputBox.Text);
            float yInputBoxValue = float.Parse(YInputBox.Text);


            // Paramétrage de la création des cercles et du panel pour les afficher
            Pen redPen = new Pen(Color.Red);
            SolidBrush brushForRedCircle = new SolidBrush(Color.Red);
            Graphics panel = DrawSpace.CreateGraphics();

            IList<Circle> circlesList = new List<Circle>();

            if (CircleGroup.Count == 0)
            {
                Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);
                circlesList.Add(newCircle);

                CircleGroup.Add((List<Circle>)circlesList);

                panel.DrawEllipse(redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
                panel.FillEllipse(brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            }
            else
            {
                Circle newCircle = new Circle(xInputBoxValue, yInputBoxValue, 10, 10, false, xInputBoxValue, yInputBoxValue);



                //foreach (KeyValuePair<float[], List<Circle>> entry in CircleDictionary)
                //{
                //    if(newCircle.x == entry.ContainsKey()
                //    {

                //    }
                //}
            }

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

                        break;
                    }
                }
                circlesList.Add(newCircle);

                panel.DrawEllipse(redPen, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
                panel.FillEllipse(brushForRedCircle, newCircle.x, newCircle.y, newCircle.width, newCircle.height);
            }
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

