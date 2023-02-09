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

        Vector2 v = entry.Value.position + Composante.vitesse[entry.Key].vitesse*1;
        Position p;
        p.position = v;
        Composante.position[entry.Key] = p;
        manager.UpdateShapePosition(entry.Key, v);

        }
    }
    public string Name { get { return "Move"; } }

}

