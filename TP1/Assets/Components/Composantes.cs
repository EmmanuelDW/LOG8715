using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Entite
{
    uint id;
}


public class Composante 
{
    public Dictionary<uint, Taille> taille = new Dictionary<uint, Taille>();
    public Dictionary<uint, Vitesse> vitesse = new Dictionary<uint, Vitesse>();
    public Dictionary<uint, Position> position = new Dictionary<uint, Position>();
    public Dictionary<uint, Couleur> couleur = new Dictionary<uint, Couleur>();
    public Dictionary<uint, Protection> protection = new Dictionary<uint, Protection>();
    public Dictionary<uint, Mobilite> mobilite = new Dictionary<uint, Mobilite>();

}

public struct Taille
{
    public float taille;
    
    
}

public struct Vitesse
{
    public Vector2 vitesse;
}

public struct Position
{
    public Vector2 position;
}

public struct Couleur
{
    public Color couleur;
}

public struct Protection
{
    public bool protege;
    public float timeleft;
}

public struct Mobilite
{
    public string mobilite;
}
