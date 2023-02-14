using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Explosion : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    int count = 0;
    static bool firstFrame = true;
    static bool doOnce = true;
    float startTime;
    public void UpdateSystem()
    {

        Debug.Log("Explosion");

        Dictionary<uint, Taille> taille = new Dictionary<uint, Taille>(Composante.taille);
        uint nextId = (uint) taille.Count;
        uint firstCircleId = (uint) taille.Count + 1;
        uint secondCircleId = (uint) taille.Count + 2;
        count++;

        if (doOnce) {
            startTime = Time.time;
            doOnce = false;
        }

        if (firstFrame)
        {
            if (Time.time - startTime > 3f)
            {
                firstFrame = false;
            }
        }
        else 
        {
            foreach (KeyValuePair<uint, Taille> i in taille)
            {
                int explosionSize = manager.Config.explosionSize;
                Debug.Log(explosionSize);
                if (i.Value.taille == explosionSize)
                {
                    //Debug.Log("taille5");
                    Position p = Composante.position[i.Key];
                    Vitesse v = Composante.vitesse[i.Key];
                    Vitesse v2;
                    v2.vitesse.x = -(v.vitesse.x);
                    v2.vitesse.y = -(v.vitesse.y);
                    Debug.Log(v.vitesse);
                    Debug.Log(v2.vitesse);

                    manager.DestroyShape(i.Key);
                    Composante.taille.Remove(i.Key);
                    Composante.position.Remove(i.Key);
                    Composante.vitesse.Remove(i.Key);
                    Composante.couleur.Remove(i.Key);
                    Composante.protection.Remove(i.Key);
                    //Debug.Log("taille5_2");

                    Taille taille1;
                    taille1.taille = explosionSize / 2;
                    Composante.taille.Add(firstCircleId, taille1);
                    Composante.position.Add(firstCircleId, p);
                    Composante.vitesse.Add(firstCircleId, v);
                    manager.CreateShape(firstCircleId, (int)taille1.taille);
                    manager.UpdateShapePosition(firstCircleId, p.position);

                    Taille taille2;
                    taille2.taille = explosionSize / 2;
                    Composante.taille.Add(secondCircleId, taille2);
                    Composante.position.Add(secondCircleId, p);
                    Composante.vitesse.Add(secondCircleId, v2);
                    manager.CreateShape(secondCircleId, (int)taille2.taille);
                    manager.UpdateShapePosition(secondCircleId, p.position);
                }

            }
        }

    }
    public string Name { get { return "Explosion"; } }

}

