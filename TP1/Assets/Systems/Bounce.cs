using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Bounce : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {

        
        Dictionary<uint, Position> temp = new Dictionary<uint, Position>(Composante.position);

        foreach (KeyValuePair<uint, Position> entry1 in Composante.position)
        {
            temp.Remove(entry1.Key);

            foreach (KeyValuePair<uint, Position> entry2 in temp)
            {
                
            
                    CollisionResult colRes = CollisionUtility.CalculateCollision(entry1.Value.position, Composante.vitesse[entry1.Key].vitesse, Composante.taille[entry1.Key].taille, entry2.Value.position, Composante.vitesse[entry2.Key].vitesse, Composante.taille[entry2.Key].taille);
                    if (colRes != null)
                    {

                     
                        Debug.Log(Composante.vitesse[entry1.Key].vitesse);

                        Position p1;
                        p1.position = colRes.position1;
                        Composante.position[entry1.Key] = p1;

                        Vitesse v1;
                        v1.vitesse = colRes.velocity1;
                        Composante.vitesse[entry1.Key] = v1;

                        Position p2;
                        p2.position = colRes.position2;
                        Composante.position[entry2.Key] = p2;


                        Vitesse v2;
                        v2.vitesse = colRes.velocity2;
                        Composante.vitesse[entry2.Key] = v2;

                        Debug.Log(Composante.vitesse[entry1.Key].vitesse);

                        manager.UpdateShapeColor(entry1.Key, Color.green);
                        manager.UpdateShapeColor(entry2.Key, Color.green);

                            



                    }


                

            }


        }

    }
    public string Name { get { return "Bounce"; } }
}

