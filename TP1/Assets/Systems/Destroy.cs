using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {

        //Debug.Log("Explosion");

        Dictionary<uint, Taille> taille = new Dictionary<uint, Taille>(Composante.taille);

        foreach (KeyValuePair<uint, Taille> i in taille)
        {
            if (i.Value.taille == 0)
            {
                manager.DestroyShape(i.Key);
                Composante.taille.Remove(i.Key);
                Composante.position.Remove(i.Key);
                Composante.vitesse.Remove(i.Key);
                Composante.couleur.Remove(i.Key);
                Composante.protection.Remove(i.Key);
                Composante.hit.Remove(i.Key);
            }

        }



    }
    public string Name { get { return "Destroy"; } }

}

