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
        Protection protectionBuffer;

        foreach (KeyValuePair<uint, Protection> pair in Composante.protection)
        {
            
            protectionBuffer.cooldown = Composante.protection[pair.Key].cooldown;
            protectionBuffer.timeleft = Composante.protection[pair.Key].timeleft;
            

            //Reduction du cooldown
            if (protectionBuffer.cooldown != 0f)
            {
                protectionBuffer.cooldown -= Time.deltaTime;
                if (protectionBuffer.cooldown < 0f)
                {
                    protectionBuffer.cooldown = 0f;
                    //manager.UpdateShapeColor(pair.Key, Color.);
                }
            }
            //Reduction du temps de protection
            if (protectionBuffer.timeleft != 0f)
            {
                protectionBuffer.timeleft -= Time.deltaTime;
                if (protectionBuffer.timeleft < 0f)
                {
                    protectionBuffer.timeleft = 0f;
                    protectionBuffer.cooldown = manager.Config.protectionCooldown;
                    Couleur couleurBuffer;

                    if (Composante.taille[pair.Key].taille == manager.Config.explosionSize)
                    {
                        couleurBuffer.couleur = Color.yellow;
                    }
                    else
                    {
                        couleurBuffer.couleur = Color.yellow;
                    }
                    couleurBuffer.couleur = Color.yellow;
                    Composante.couleur[pair.Key] = couleurProtection;
                    manager.UpdateShapeColor(pair.Key, couleurProtection.couleur);

                   
                }
            }
            //Attribution de la protection si la taille est assez petite en fonction de la probabilite de protection
            if (Composante.taille[pair.Key].taille < manager.Config.protectionSize)
            {
                if (protectionBuffer.timeleft == 0f && protectionBuffer.cooldown == 0f)
                {
                    if (UnityEngine.Random.value <= manager.Config.protectionProbability)
                    {
                        protectionBuffer.timeleft = manager.Config.protectionDuration;
                        
                    }

                }
            }
        }   

 

        
        //Simulation ReverseTIme
        if (ReverseTime.cooldownReverse == 0f)
        {
            foreach (KeyValuePair<uint, Protection> pair in ComposanteReverse.protectionReverse)
            {
                if (pair.Value.timeleft != 0f)
                {
                    Protection bufferProt;
                    bufferProt.timeleft = pair.Value.timeleft - Time.deltaTime;
                    if (bufferProt.timeleft < 0f)
                    {
                        bufferProt.timeleft = 0f;
                    }
                    //ComposanteReverse.protectionReverse[pair.Key] = bufferProt;
                }
                if (ComposanteReverse.protectionReverse[pair.Key].timeleft == 0f)
                {


                }

            }
        }
    }


    public string Name { get { return "Protection Done"; } }
}
