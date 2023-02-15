using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {
        Vector2 UpperBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        Vector2 LowerBounds = Camera.main.ScreenToWorldPoint(Vector2.zero);


        Dictionary<uint, Position> pos = new Dictionary<uint, Position>(Composante.position);
        foreach (KeyValuePair<uint, Position> entry1 in pos)
        {
            Taille T ;
            T = Composante.taille[entry1.Key];
            float t = T.taille/2;

            if (entry1.Value.position.x + t  >= UpperBounds.x )
            {
                Vitesse v;
                v.vitesse = Composante.vitesse[entry1.Key].vitesse;
                v.vitesse.x = -v.vitesse.x;
                Composante.vitesse[entry1.Key] = v;
            }

            if (entry1.Value.position.y + t >= UpperBounds.y)
            {
                Vitesse v;
                v.vitesse = Composante.vitesse[entry1.Key].vitesse;
                v.vitesse.y = -v.vitesse.y;
                Composante.vitesse[entry1.Key] = v;
            }
            if (entry1.Value.position.x - t <= LowerBounds.x)
            {
                Vitesse v;
                v.vitesse = Composante.vitesse[entry1.Key].vitesse;
                v.vitesse.x = -v.vitesse.x;
                Composante.vitesse[entry1.Key] = v;
            }

            if (entry1.Value.position.y - t <= LowerBounds.y)
            {
                Vitesse v;
                v.vitesse = Composante.vitesse[entry1.Key].vitesse;
                v.vitesse.y = -v.vitesse.y;
                Composante.vitesse[entry1.Key] = v;
            }
        }
    }
    public string Name { get { return "WallBounce"; } }

}

