using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {


    Dictionary<uint, Position> pos = new Dictionary<uint, Position>(Composante.position);
    foreach (KeyValuePair<uint, Position> entry in pos)
        {

        Vector2 v;
        v.x = entry.Value.position.x + Composante.vitesse[entry.Key].vitesse.x * Time.deltaTime;
        v.y = entry.Value.position.y + Composante.vitesse[entry.Key].vitesse.y * Time.deltaTime;
        Position p;
        p.position = v;
        Composante.position[entry.Key] = p;
        manager.UpdateShapePosition(entry.Key, v);

        }
        //Simulation pour le ReverseTime
        if (ReverseTime.cooldownReverse == 0f)
        {
            foreach (KeyValuePair<uint, Position> pair in ComposanteReverse.positionReverse)
            {
                Vector2 posRev;
                posRev.x = pair.Value.position.x + ComposanteReverse.vitesseReverse[pair.Key].vitesse.x * Time.deltaTime;
                posRev.y = pair.Value.position.y + ComposanteReverse.vitesseReverse[pair.Key].vitesse.y * Time.deltaTime;
                Position posRevFinal;
                posRevFinal.position = posRev;
                ComposanteReverse.positionReverse[pair.Key] = posRevFinal;
                    
            }

        }

    }
    public string Name { get { return "Move"; } }

}

