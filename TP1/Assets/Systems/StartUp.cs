using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartUp : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    static bool firstFrame = true;
    public void UpdateSystem()
    {

        //Debug.Log("ok");

        uint j = 0;
        if (firstFrame == true)
        {

            foreach (Config.ShapeConfig i in manager.Config.circleInstancesToSpawn)
            {

                //Size
                Taille taille;
                taille.taille = i.initialSize;
                Composante.taille.Add(j, taille);
                manager.CreateShape(j, i.initialSize);

                //Position
                Position position;
                position.position = i.initialPosition;
                Composante.position.Add(j, position);
                manager.UpdateShapePosition(j, i.initialPosition);

                //Color
                Couleur couleur;
                couleur.couleur = Color.red;
                Composante.couleur.Add(j, couleur);
                manager.UpdateShapeColor(j, Color.red);

                //Velocity
                Vitesse vitesse;
                vitesse.vitesse = i.initialVelocity;
                Composante.vitesse.Add(j, vitesse);

                //Protection
                Protection protection;
                protection.protege = false;
                protection.timeleft = 0;
                Composante.protection.Add(j, protection);

                j++;
                firstFrame = false;

            }
        }



    }
    public string Name { get { return "StartUp"; }}

}

