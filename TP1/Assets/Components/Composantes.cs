using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Entite
{
    uint id;
    uint version;
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

public struct Protrection
{
    bool protege;
    float timeleft;
}


