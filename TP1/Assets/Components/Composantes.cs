using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Version Générique qui ne fonctionne pas
{

    //public struct Taille
    //{
    //    float taille;
    //}

    //public struct Vitesse
    //{ 
    //    Vector2 vitesse;
    //}

    //public struct Position
    //{
    //    Vector2 position;
    //}

    //public struct Couleur
    //{
    //    Color couleur;
    //}

    //public struct Protection
    //{
    //    bool protege;
    //    float timeleft;
    //}


    ////public struct Composante<T> where T : struct
    ////{
    ////    T composante; // Quand on déclare une struct on lui donne le type du component (Ex: Taille, Couleur, etc...)
    ////}

    ////public struct Entite<T> where T : struct
    ////{
    ////    uint id;
    ////    List<Composante<T>> composantes;
    ////}

    ////public struct Entites<T> where T : struct
    ////{
    ////    List<Entite<T>> entites;
    ////}
}
//version orienté object?
{ 
//public class Composante { }
//public struct Entite
//{
//    uint id;
//    List<Composante> composantes;
//}

//public struct Entites
//{
//    List<Entite> entites;
//}

//public class Taille : Composante
//{
//    float taille;
//}

//public class Vitesse : Composante
//{
//    Vector2 vitesse;
//}

//public class Position : Composante
//{
//    Vector2 position;
//}

//public class Couleur : Composante
//{
//    Color couleur;
//}

//public class Protection : Composante
//{
//    bool protege;
//    float timeleft;
//}
}

//Version Mal optimisé

public struct Composante
{
    Taille taile;
    Position position;
    Vitesse vitesse;
    Couleur couleur;
    Protection protection;
}
public struct Entite
{
    uint id;
    List<Composante> composantes;
}

public struct Entites
{
    List<Entite> entites;
}

public struct Taille 
{
    float taille;
}

public struct Vitesse
{
    Vector2 vitesse;
}

public struct Position
{
    Vector2 position;
}

public struct Couleur
{
    Color couleur;
}

public struct Protection
{
    bool protege;
    float timeleft;
}