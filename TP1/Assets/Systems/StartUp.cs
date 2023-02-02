using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartUp
{
    ECSManager manager = ECSManager.Instance;
    Composante composante;

  

    public void UpdateSystem()
    {
        if (Time.frameCount != 0)
        {
            return;
        }


        uint j = 0;
        foreach (Config.ShapeConfig i in manager.Config.circleInstancesToSpawn)
        {
            //Size
            Taille taille;
            taille.taille = i.initialSize;
            composante.taille.Add(j, taille);
            manager.CreateShape(j, i.initialSize);

            //Velocity
            Vitesse vitesse;
            vitesse.vitesse = i.initialVelocity;
            composante.vitesse.Add(j, vitesse);

            //Position
            Position position;
            position.position = i.initialPosition;
            composante.position.Add(j, position);
            manager.UpdateShapePosition(j, i.initialPosition);

            //Color
            Couleur couleur;
            couleur.couleur = Color.white;
            composante.couleur.Add(j, couleur);
            manager.UpdateShapeColor(j, Color.white);

            //Protection
            Protection protection;
            protection.protege = false;
            protection.timeleft = 0;
            composante.protection.Add(j, protection);

            //Mobility
            Mobilite mobilite;
            mobilite.mobilite = "Dynamique";
            composante.mobilite.Add(j, mobilite);



            j++;

        }
        
         
    }

}

