using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {


        Dictionary<uint, Position> pos = new Dictionary<uint, Position>(Composante.position);
        foreach (KeyValuePair<uint, Position> entry1 in pos)
        {
            foreach (KeyValuePair<uint, Position> entry2 in pos)
            {
                if (entry1.Key != entry2.Key){
                    CollisionResult colRes = CollisionUtility.CalculateCollision(entry1.Value.position, Composante.vitesse[entry1.Key].vitesse, Composante.taille[entry1.Key].taille, entry2.Value.position, Composante.vitesse[entry2.Key].vitesse, Composante.taille[entry2.Key].taille);
                    if (colRes != null)
                    {
                        Position p1;
                        p1.position = colRes.position1;
                        Composante.position[entry1.Key] = p1;

                        Vitesse v1;
                        v1.vitesse = colRes.velocity1;
                        Composante.vitesse[entry1.Key] = v1;

                        //Position p2;
                        //p2.position = colRes.position2;
                        //Composante.position[entry2.Key] = p2;


                        //Vitesse v2;
                        //v2.vitesse = colRes.velocity2;
                        //Composante.vitesse[entry2.Key] = v2;
                    }


                }

            }

        }
    }
    public string Name { get { return "Bounce"; } }

}

