using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Bounce : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {

        
        Dictionary<uint, Position> temp1 = new Dictionary<uint, Position>(Composante.position);
        Dictionary<uint, Position> temp2 = new Dictionary<uint, Position>(Composante.position);

        foreach (KeyValuePair<uint, Position> entry1 in temp1)
        {
            temp2.Remove(entry1.Key);

            foreach (KeyValuePair<uint, Position> entry2 in temp2)
            {
            
                CollisionResult colRes = CollisionUtility.CalculateCollision(entry1.Value.position, Composante.vitesse[entry1.Key].vitesse, Composante.taille[entry1.Key].taille, entry2.Value.position, Composante.vitesse[entry2.Key].vitesse, Composante.taille[entry2.Key].taille);
                if (colRes != null)
                {


                    Position p1;
                    p1.position = colRes.position1;
                    Composante.position[entry1.Key] = p1;

                    Vitesse v1;
                    v1.vitesse = colRes.velocity1;
                    Composante.vitesse[entry1.Key] = v1;

                    Hit h1;
                    h1.hit = true;
                    Composante.hit[entry1.Key] = h1;


                    Position p2;
                    p2.position = colRes.position2;
                    Composante.position[entry2.Key] = p2;


                    Vitesse v2;
                    v2.vitesse = colRes.velocity2;
                    Composante.vitesse[entry2.Key] = v2;

                    Hit h2;
                    h2.hit = true;
                    Composante.hit[entry2.Key] = h2;

                    Taille taille1;
                    Taille taille2;


                    if ((int)Composante.taille[entry1.Key].taille > (int)Composante.taille[entry2.Key].taille)
                    {
                        
                        taille1.taille = Composante.taille[entry1.Key].taille - 1f;
                        Composante.taille[entry1.Key] = taille1;
                        manager.UpdateShapeSize(entry1.Key, Composante.taille[entry1.Key].taille);

                        taille2.taille = Composante.taille[entry2.Key].taille + 1f;
                        Composante.taille[entry2.Key] = taille2;
                        manager.UpdateShapeSize(entry2.Key, Composante.taille[entry2.Key].taille);
                        
                    }
                    else if ((int)Composante.taille[entry1.Key].taille < (int)Composante.taille[entry2.Key].taille)
                    {
                        taille1.taille = Composante.taille[entry1.Key].taille + 1f;
                        Composante.taille[entry1.Key] = taille1;
                        manager.UpdateShapeSize(entry1.Key, Composante.taille[entry1.Key].taille);


                        taille2.taille = Composante.taille[entry2.Key].taille - 1f;
                        Composante.taille[entry2.Key] = taille2;
                        manager.UpdateShapeSize(entry2.Key, Composante.taille[entry2.Key].taille);

                    }
                }
            }
        }
    }
    public string Name { get { return "Bounce"; } }
}

