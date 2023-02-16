using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;



public class Explosion : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    

   
  
    
    float startTime;
    public void UpdateSystem()
    {
        Dictionary<uint, Position> position = new Dictionary<uint, Position>(Composante.position);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click2");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            //Debug.Log(ray);
            Vector2 pos = (Vector2)ray.origin;
            Debug.Log(pos);
            foreach (KeyValuePair<uint, Position> p in position)
            {
                if (pos.x <= p.Value.position.x + (Composante.taille[p.Key].taille / 2)
                    && pos.x >= p.Value.position.x - (Composante.taille[p.Key].taille / 2)
                    && pos.y <= p.Value.position.y + (Composante.taille[p.Key].taille / 2)
                    && pos.y >= p.Value.position.y - (Composante.taille[p.Key].taille / 2))
                {
                    Debug.Log(p.Key);
                    if (Composante.taille[p.Key].taille >= 2)
                    {
                        Debug.Log(Composante.taille[p.Key].taille);
                        Vitesse v = Composante.vitesse[p.Key];
                        Hit h;
                        h.hit = false;
                        Click c;
                        c.click = true;
                        Protection protection;
                        protection.cooldown = 0f;
                        protection.timeleft = 0f;
                        Vitesse v2;
                        v2.vitesse.x = -(v.vitesse.x);
                        v2.vitesse.y = -(v.vitesse.y);
                        Couleur couleur;
                        couleur.couleur = Color.blue;

                        uint firstCircleId = StartUp.count;
                        StartUp.count++;

                        uint secondCircleId = StartUp.count;
                        StartUp.count++;

                        Taille taille1;
                        taille1.taille = Composante.taille[p.Key].taille / 2;
                        Composante.taille.Add(firstCircleId, taille1);
                        Composante.position.Add(firstCircleId, p.Value);
                        Composante.protection.Add(firstCircleId, protection);
                        Composante.vitesse.Add(firstCircleId, v);
                        Composante.hit.Add(firstCircleId, h);
                        Composante.couleur.Add(firstCircleId, couleur);
                        Composante.click.Add(firstCircleId, c);
                        manager.CreateShape(firstCircleId, (int)taille1.taille);
                        manager.UpdateShapePosition(firstCircleId, p.Value.position);


                        Taille taille2;
                        taille2.taille = Composante.taille[p.Key].taille / 2;
                        Composante.taille.Add(secondCircleId, taille2);
                        Composante.position.Add(secondCircleId, p.Value);
                        Composante.protection.Add(secondCircleId, protection);
                        Composante.vitesse.Add(secondCircleId, v2);
                        Composante.hit.Add(secondCircleId, h);
                        Composante.couleur.Add(secondCircleId, couleur);
                        Composante.click.Add(secondCircleId, c);
                        manager.CreateShape(secondCircleId, (int)taille2.taille);
                        manager.UpdateShapePosition(secondCircleId, p.Value.position);


                    }
                    manager.DestroyShape(p.Key);
                    Composante.taille.Remove(p.Key);
                    Composante.position.Remove(p.Key);
                    Composante.vitesse.Remove(p.Key);
                    Composante.couleur.Remove(p.Key);
                    Composante.protection.Remove(p.Key);
                    Composante.hit.Remove(p.Key);
                    Composante.click.Remove(p.Key);

                }
            }
        }

        Dictionary<uint, Taille> taille = new Dictionary<uint, Taille>(Composante.taille);
        //Debug.Log("Explosion");
        foreach (KeyValuePair<uint, Taille> i in taille)
        {
 
                int explosionSize = manager.Config.explosionSize;
                //Debug.Log(explosionSize);
                if (i.Value.taille == explosionSize)
                {

                    Position p = Composante.position[i.Key];
                    Vitesse v = Composante.vitesse[i.Key];
                    Hit h;
                    h.hit = false;
                    Click c;
                    c.click = false;
                    Vitesse v2;
                    Protection protection;
                    protection.cooldown = 0f;
                    protection.timeleft = 0f;
                    v2.vitesse.x = -(v.vitesse.x);
                    v2.vitesse.y = -(v.vitesse.y);
                    Couleur couleur;
                    couleur.couleur = Color.blue;


                    manager.DestroyShape(i.Key);
                    Composante.taille.Remove(i.Key);
                    Composante.position.Remove(i.Key);
                    Composante.vitesse.Remove(i.Key);
                    Composante.couleur.Remove(i.Key);
                    Composante.protection.Remove(i.Key);
                    Composante.hit.Remove(i.Key);
                    Composante.click.Remove(i.Key);
                    Debug.Log("taille5_2");

                    uint firstCircleId = StartUp.count;
                    StartUp.count++;

                    uint secondCircleId = StartUp.count;
                    StartUp.count++;


                    Taille taille1;
                    taille1.taille = explosionSize / 2;
                    Composante.taille.Add(firstCircleId, taille1);
                    Composante.position.Add(firstCircleId, p);
                    Composante.protection.Add(firstCircleId, protection);
                    Composante.vitesse.Add(firstCircleId, v);
                    Composante.hit.Add(firstCircleId, h);
                    Composante.couleur.Add(firstCircleId, couleur);
                    Composante.click.Add(firstCircleId, c);
                    manager.CreateShape(firstCircleId, (int)taille1.taille);
                    manager.UpdateShapePosition(firstCircleId, p.position);


                    Taille taille2;
                    taille2.taille = explosionSize / 2;
                    Composante.taille.Add(secondCircleId, taille2);
                    Composante.position.Add(secondCircleId, p);
                    Composante.protection.Add(secondCircleId, protection);
                    Composante.vitesse.Add(secondCircleId, v2);
                    Composante.hit.Add(secondCircleId, h);
                    Composante.couleur.Add(secondCircleId, couleur);
                    Composante.click.Add(secondCircleId, c);
                    manager.CreateShape(secondCircleId, (int)taille2.taille);
                    manager.UpdateShapePosition(secondCircleId, p.position);
                }
            
        }
                

            
        

    }
    public string Name { get { return "Explosion"; } }

}

