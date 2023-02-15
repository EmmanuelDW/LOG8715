using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.EventSystems.EventTrigger;

public class ColorSetter : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {
        Couleur couleur;
        Hit hit;
        hit.hit = false;
        Color orange = new Color(0.9f, 0.5f, 0.11f,1);
        Vector2 statique = new Vector2(0.0f, 0.0f);

        Dictionary<uint, Hit> h = new Dictionary<uint, Hit>(Composante.hit);
        foreach (KeyValuePair<uint,Hit> pair in h)
        {

            if (Composante.vitesse[pair.Key].vitesse == statique)
            {
                couleur.couleur = Color.red;
                Composante.couleur.Add(pair.Key, couleur);
                manager.UpdateShapeColor(pair.Key, Color.red);
            }
            else if (Composante.hit[pair.Key].hit == false)
            {
                couleur.couleur = Color.blue;
                Composante.couleur.Add(pair.Key, couleur);
                manager.UpdateShapeColor(pair.Key, Color.blue);
            }
            if (Composante.hit[pair.Key].hit == false)
            {
                couleur.couleur = Color.blue;
                manager.UpdateShapeColor(pair.Key, couleur.couleur);
                Composante.couleur[pair.Key] = couleur;
                
            }
            if (Composante.hit[pair.Key].hit == true)
            {
                
                couleur.couleur = Color.green;
                manager.UpdateShapeColor(pair.Key, couleur.couleur);
                Composante.couleur[pair.Key] = couleur;
                Composante.hit[pair.Key] = hit;
            }
            //if (Composante.taille[pair.Key].taille == manager.Config.explosionSize-1)
            //{
            //    couleur.couleur = orange;
            //    manager.UpdateShapeColor(pair.Key, orange);
            //    Composante.couleur[pair.Key] = couleur;
                
            //}

        }
    }
    public string Name { get { return "Bounce"; } }
}

