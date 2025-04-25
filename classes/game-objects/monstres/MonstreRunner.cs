
using System;
using System.Collections.Generic;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;


namespace desktop.gameobjects;

public class MonstreRunner : Monstre
{
    const int vitesse = 160; 
    const int rayon = 10; 
    const int hp = 15; 
    const int dmg = 1; 
    const string type = "runner";
    const int vitesseRot = 7;
    const float exp = 1;
    public MonstreRunner(int sides, EcranJeu ecranJeu, float ennemiDifficultee)
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
