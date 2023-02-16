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
        Protection protection;
        protection.cooldown = 0f;
        protection.timeleft = 0f;

        string etatProtection;
        Dictionary<uint, Protection> temp = new Dictionary<uint, Protection>(Composante.protection);
        Debug.Log("entre dans protect");
        
        foreach (KeyValuePair<uint, Protection> pair in temp)
        {

            Debug.Log("entre dans protect boucle");
            if (Composante.protection[pair.Key].cooldown == 0f && Composante.protection[pair.Key].timeleft == 0f)
            {
                etatProtection = "Inactif";
            }
            else
            {
                if (Composante.protection[pair.Key].cooldown != 0f && Composante.protection[pair.Key].timeleft == 0f)
                {
                    etatProtection = "Cooldown";
                }
                else
                {
                    etatProtection = "Actif";
                }
            }
 
            
            

            switch (etatProtection)
            {
                case "Inactif":
                    
                    float random = UnityEngine.Random.value;
                    Debug.Log("entre dans inactif");
                    if (random <= manager.Config.protectionProbability)
                    {
                        protection.timeleft = manager.Config.protectionDuration;
                        Debug.Log(protection.timeleft);
                        protection.cooldown = manager.Config.protectionCooldown;
                        Composante.protection[pair.Key] = protection;

                    }
                    break;

                case "Cooldown":

                    Debug.Log("entre dans protect cooldown");
                    protection.cooldown -= Time.deltaTime;
                    if (protection.cooldown < 0f)
                    {
                        protection.cooldown = 0f;
                        
                    }

                    break;

                case "Actif":
                    Debug.Log("entre dans protect actif");

                    protection.timeleft -= Time.deltaTime;
                    if (protection.timeleft < 0f)
                    {
                        protection.timeleft = 0f;

                    }
                    break;

                
            }

            
        }   
    }


    public string Name { get { return "Protection Done"; } }
}
