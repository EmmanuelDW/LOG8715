using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartUp : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    static bool firstFrame = true;
    public void UpdateSystem()
    {

        Debug.Log("StartUp");
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

                //Velocity
                Vitesse vitesse;
                vitesse.vitesse = i.initialVelocity;
                Composante.vitesse.Add(j, vitesse);

                //Position
                Position position;
                position.position = i.initialPosition;
                Composante.position.Add(j, position);
                manager.UpdateShapePosition(j, i.initialPosition);

                //Color
                Couleur couleur;
                couleur.couleur = Color.black;
                Composante.couleur.Add(j, couleur);
                manager.UpdateShapeColor(j, Color.black);

                //Protection
                Protection protection;
                protection.protege = false;
                protection.timeleft = 0;
                Composante.protection.Add(j, protection);

                //Mobility
                Mobilite mobilite;
                mobilite.mobilite = "Dynamique";
                Composante.mobilite.Add(j, mobilite);



                j++;
                firstFrame = false;

            }
        }
        
        
         
    }
    public string Name { get { return "StartUp"; }}

}

