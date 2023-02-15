using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartUp : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    static bool firstFrame = true;
    public static uint count = 0;


    public void UpdateSystem()
    {

        
        UnityEngine.Random.InitState(manager.Config.seed);
        uint j = 0;
        if (firstFrame == true)
        {

            foreach (Config.ShapeConfig i in manager.Config.circleInstancesToSpawn)
            {
                count++;

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
                protection.cooldown = 0f;
                protection.timeleft = 0f;
                Composante.protection.Add(j, protection);

                //Collision
                Hit collision;
                collision.hit = false;
                Composante.hit.Add(j, collision);

                j++;
                firstFrame = false;

            }
        }



    }
    public string Name { get { return "StartUp"; }}

}

