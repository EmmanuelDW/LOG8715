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
        int explosionSize = manager.Config.explosionSize;
        Color orange = new Color(1.0f, 0.64f, 0.0f);


        Dictionary<uint, Hit> h = new Dictionary<uint, Hit>(Composante.hit);
        foreach (KeyValuePair<uint, Hit> pair in h)
        {
            if (Composante.hit[pair.Key].hit == false)
            {
                if (Composante.taille[pair.Key].taille == explosionSize - 1)
                {
                    couleur.couleur = orange;
                    manager.UpdateShapeColor(pair.Key, couleur.couleur);
                    Composante.couleur[pair.Key] = couleur;
                }
                else
                {
                    couleur.couleur = Color.blue;
                    manager.UpdateShapeColor(pair.Key, couleur.couleur);
                    Composante.couleur[pair.Key] = couleur;
                }

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
    public string Name { get { return "ColorSetter"; } }
}