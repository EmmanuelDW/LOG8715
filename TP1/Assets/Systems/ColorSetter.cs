using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.EventSystems.EventTrigger;

public class ColorSetter : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {
        string priority;
        Couleur couleur;
        Hit hit;
        hit.hit = false;
        int aboutToExplodeSize = manager.Config.explosionSize - 1;
        Color aboutToExplodeColor = new Color(1.0f, 0.64f, 0.0f);
        Color dynamiqueClickColor = new Color(1f, 0.75f, 0.8f);
        Vector2 statiqueVector = new Vector2(0.0f, 0.0f);


        Dictionary<uint, Hit> h = new Dictionary<uint, Hit>(Composante.hit);
        foreach (KeyValuePair<uint, Hit> pair in h)
        {
            
            if (Composante.vitesse[pair.Key].vitesse == statiqueVector)
            {
                priority = "Statique";
            }
            else
            {
                if(Composante.hit[pair.Key].hit == true)
                {
                    priority = "Hit";
                    Composante.hit[pair.Key] = hit;
                }
                else
                {
                    if(Composante.hit[pair.Key].hit == true)
                    {
                        priority = "Click";
                    }
                    else
                    {
                        if (Composante.taille[pair.Key].taille >= aboutToExplodeSize)
                        {
                            priority = "Explosion";
                        }
                        else
                        {
                            if (Composante.protection[pair.Key].timeleft >= 0f)
                            {
                                priority = "Protege";
                            }
                            else
                            {
                                priority = "Dynamique";
                            }
                        }
                    }
                }
            }
                

            
            switch (priority)
            {
                case "Protege":
                    couleur.couleur = Color.yellow;
                    manager.UpdateShapeColor(pair.Key, couleur.couleur);
                    Composante.couleur[pair.Key] = couleur;
                    break;

                case "Explosion":
                    couleur.couleur = aboutToExplodeColor;
                    manager.UpdateShapeColor(pair.Key, couleur.couleur);
                    Composante.couleur[pair.Key] = couleur;
                    break;

                case "DynamiqueClick":
                    couleur.couleur = dynamiqueClickColor;
                    manager.UpdateShapeColor(pair.Key, couleur.couleur);
                    Composante.couleur[pair.Key] = couleur;
                    break;

                case "Statique":
                    couleur.couleur = Color.red;
                    manager.UpdateShapeColor(pair.Key, couleur.couleur);
                    Composante.couleur[pair.Key] = couleur;
                    break;

                case "Dynamique":
                    couleur.couleur = Color.blue;
                    manager.UpdateShapeColor(pair.Key, couleur.couleur);
                    Composante.couleur[pair.Key] = couleur;
                    break;

                case "Hit":
                    couleur.couleur = Color.green;
                    manager.UpdateShapeColor(pair.Key, couleur.couleur);
                    Composante.couleur[pair.Key] = couleur;
                    Composante.hit[pair.Key] = hit;
                    break;

            }
        }
    }
    public string Name { get { return "ColorSetter"; } }
}