using System.Collections.Generic;
using UnityEngine;

public class RegisterSystems
{
    public static List<ISystem> GetListOfSystems()
    {
        // determine order of systems to add
        var toRegister = new List<ISystem>
        {
            new StartUp(),
            new Move(),
            new Bounce()
        };
 
        Debug.Log("EndRegister");

        
        // Add your systems here


        return toRegister;
    }
}