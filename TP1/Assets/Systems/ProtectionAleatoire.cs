using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.PlayerLoop;


public class ProtectionAleatoire
{
   
    static void Update()
    {

        ECSManager manager = GameObject.Find("ECSManagerObj").GetComponent<ECSManager>();

        manager.UpdateShapeColor(3, Color.blue);
            }
    


}
