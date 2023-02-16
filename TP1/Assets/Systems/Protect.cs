using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protect : ISystem
{
    readonly ECSManager manager = ECSManager.Instance;
    public void UpdateSystem()
    {
        Couleur couleurProtection;
        couleurProtection.couleur = Color.yellow;

        Dictionary<uint, Protection> temp = new Dictionary<uint, Protection>(Composante.protection);
        foreach (KeyValuePair<uint, Protection> pair in temp)
        {

            if (UnityEngine.Random.value < manager.Config.protectionProbability && pair.Value.timeleft == 0 && pair.Value.cooldown == 0)
            {

                Protection p;
                p.cooldown = 0f;
                p.timeleft = manager.Config.protectionDuration/Time.deltaTime;
                Composante.protection[pair.Key] = p;

            }
            if ( pair.Value.timeleft > 0)
            {
                Protection p;
                p.timeleft = pair.Value.timeleft - 1;
                p.cooldown = 0;
                if (p.timeleft == 0)
                {
                    p.cooldown = manager.Config.protectionCooldown / Time.deltaTime;
                }
                Composante.protection[pair.Key] = p;

            }
            if (pair.Value.cooldown > 0)
            {
                Protection p;
                p.timeleft = pair.Value.timeleft;
                p.cooldown = pair.Value.cooldown - 1; ;
                Composante.protection[pair.Key] = p;

            }

        }   
    }


    public string Name { get { return "Protection Done"; } }
}
