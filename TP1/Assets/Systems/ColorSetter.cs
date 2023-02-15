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

        Dictionary<uint, Hit> h = new Dictionary<uint, Hit>(Composante.hit);
        foreach (KeyValuePair<uint,Hit> pair in h)
        {
            if (Composante.hit[pair.Key].hit == false)
            {
                couleur.couleur = Color.red;
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
        }
    }
    public string Name { get { return "Bounce"; } }
}

