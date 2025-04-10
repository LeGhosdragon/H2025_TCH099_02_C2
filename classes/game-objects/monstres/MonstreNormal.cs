
using System;
using System.Collections.Generic;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;


namespace desktop.gameobjects;

public class MonstreNormal : Monstre
{
    const int vitesse = 20; 
    const int rayon = 20; 
    const int hp = 25; 
    const int dmg = 1; 
    const string type = "normal";
    const int vitesseRot = 1;
    const float exp = 2;
    public MonstreNormal(int sides, EcranJeu ecranJeu, float ennemiDifficultee)
    : base(
        PolyGen.GetPoly(sides, rayon), 
        GenerateurMonstre.GenererPositionMonstreBordures(rayon, ecranJeu), 
        ecranJeu, 
        rayon, 
        type, 
        vitesse, 
        vitesseRot, 
        (float)Math.Round(hp * Math.Pow(ennemiDifficultee, 1.2)), 
        (float)Math.Round(exp + ennemiDifficultee/3), 
        (float)Math.Round(dmg * ennemiDifficultee)
    ){}
    public override void Update(float deltaT)
    {
        base.Update(deltaT);
    }
}
