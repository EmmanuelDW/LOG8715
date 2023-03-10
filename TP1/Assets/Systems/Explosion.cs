using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;



public class Explosion : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    
    public void UpdateSystem()
    {

        Dictionary<uint, Taille> taille = new Dictionary<uint, Taille>(Composante.taille);
        foreach (KeyValuePair<uint, Taille> i in taille)
        {
 
                int explosionSize = manager.Config.explosionSize;
                if (i.Value.taille == explosionSize)
                {

                    Position p = Composante.position[i.Key];
                    Vitesse v = Composante.vitesse[i.Key];
                    Hit h;
                    h.hit = false;
                    Click c;
                    c.click = false;
                    Vitesse v2;
                    v2.vitesse.x = -(v.vitesse.x);
                    v2.vitesse.y = -(v.vitesse.y);
                    Couleur couleur;
                    couleur.couleur = Color.blue;
                    Protection prot;
                    prot.cooldown = 0.0f;
                    prot.timeleft = 0.0f;


                    manager.DestroyShape(i.Key);
                    Composante.taille.Remove(i.Key);
                    Composante.position.Remove(i.Key);
                    Composante.vitesse.Remove(i.Key);
                    Composante.couleur.Remove(i.Key);
                    Composante.hit.Remove(i.Key);
                    Composante.click.Remove(i.Key);
                    Composante.protection.Remove(i.Key);

                    uint firstCircleId = StartUp.count;
                    StartUp.count++;

                    uint secondCircleId = StartUp.count;
                    StartUp.count++;


                    Taille taille1;
                    taille1.taille = explosionSize / 2;
                    Composante.taille.Add(firstCircleId, taille1);
                    Composante.position.Add(firstCircleId, p);
                    Composante.vitesse.Add(firstCircleId, v);
                    Composante.hit.Add(firstCircleId, h);
                    Composante.couleur.Add(firstCircleId, couleur);
                    Composante.click.Add(firstCircleId, c);
                    Composante.protection.Add(firstCircleId, prot);
                    manager.CreateShape(firstCircleId, (int)taille1.taille);
                    manager.UpdateShapePosition(firstCircleId, p.position);


                    Taille taille2;
                    taille2.taille = explosionSize / 2;
                    Composante.taille.Add(secondCircleId, taille2);
                    Composante.position.Add(secondCircleId, p);
                    Composante.vitesse.Add(secondCircleId, v2);
                    Composante.hit.Add(secondCircleId, h);
                    Composante.couleur.Add(secondCircleId, couleur);
                    Composante.click.Add(secondCircleId, c);
                    Composante.protection.Add(secondCircleId, prot);
                    manager.CreateShape(secondCircleId, (int)taille2.taille);
                    manager.UpdateShapePosition(secondCircleId, p.position);
                }
            
        }
                

            
        

    }
    public string Name { get { return "Explosion"; } }

}

