using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Entite
{
    uint id;
}


static public class Composante
{
    static public Dictionary<uint, Taille> taille = new Dictionary<uint, Taille>();
    static public Dictionary<uint, Vitesse> vitesse = new Dictionary<uint, Vitesse>();
    static public Dictionary<uint, Position> position = new Dictionary<uint, Position>();
    static public Dictionary<uint, Couleur> couleur = new Dictionary<uint, Couleur>();
    static public Dictionary<uint, Protection> protection = new Dictionary<uint, Protection>();
    static public Dictionary<uint, Hit> hit = new Dictionary<uint, Hit>();
    static public Dictionary<uint, Click> click = new Dictionary<uint, Click>();
}
static public class ComposanteReverse
{
    static public Dictionary<uint, Taille> tailleReverse = new Dictionary<uint, Taille>();
    static public Dictionary<uint, Vitesse> vitesseReverse = new Dictionary<uint, Vitesse>();
    static public Dictionary<uint, Position> positionReverse = new Dictionary<uint, Position>();
    static public Dictionary<uint, Couleur> couleurReverse = new Dictionary<uint, Couleur>();
    static public Dictionary<uint, Protection> protectionReverse = new Dictionary<uint, Protection>();
    static public Dictionary<uint, Hit> hitReverse = new Dictionary<uint, Hit>();
    static public Dictionary<uint, Click> click = new Dictionary<uint, Click>();
}
    

public struct Taille
{
    public float taille;

}

public struct Couleur
{
    public Color couleur;
}
public struct Position
{
    public Vector2 position;
}
public struct Vitesse
{
    public Vector2 vitesse;
}

public struct Protection
{
    public float cooldown;
    public float timeleft;
}

public struct Hit
{
    public bool hit;
}
public struct Click
{
    public bool click;
}