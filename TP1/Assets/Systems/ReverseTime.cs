using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTime : ISystem
{
    public static bool firstBackUpDone = false;
    readonly ECSManager manager = ECSManager.Instance;
    static float cooldownReverse = 3f;
    public void UpdateSystem()
    {
        //Reéduction du cooldown
        if (cooldownReverse !=0)
        {

            cooldownReverse -= Time.deltaTime;
            
            if (cooldownReverse < 0f)
            {
                cooldownReverse = 0f;
            }
        }

        //Sauvegarde Initiale
        if (firstBackUpDone == false)
        {
            ComposanteReverse.tailleReverse = Composante.taille;
            ComposanteReverse.positionReverse = Composante.position;
            ComposanteReverse.vitesseReverse = Composante.vitesse;
            ComposanteReverse.couleurReverse = Composante.couleur;
            ComposanteReverse.protectionReverse = Composante.protection;
            firstBackUpDone= true;
        }

        //Retour 3 secondes dans le passé
        if (Input.GetKey(KeyCode.Space) == true && cooldownReverse!= 0f)
        {
            Debug.Log("Le retour en arrière est en cooldown");
        }

        if (Input.GetKey(KeyCode.Space) == true && cooldownReverse == 0f)
        {
            cooldownReverse = 3f;

            foreach (uint t in Composante.taille.Keys)
            {
                manager.DestroyShape(t);
                         
            }

            Composante.taille = ComposanteReverse.tailleReverse;
            Composante.position = ComposanteReverse.positionReverse;
            Composante.vitesse = ComposanteReverse.vitesseReverse;
            Composante.couleur = ComposanteReverse.couleurReverse;
            Composante.protection = ComposanteReverse.protectionReverse;
            
            foreach (uint id in Composante.taille.Keys) 
            {
                //Rendering
                manager.CreateShape(id, (int)Composante.taille[id].taille);
                manager.UpdateShapePosition(id, Composante.position[id].position);
                manager.UpdateShapeColor(id, Composante.couleur[id].couleur);
            }


        }


    }

   
    public string Name { get { return "StartUp"; } }
}
