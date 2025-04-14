
using System;
using System.Collections.Generic;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;


namespace desktop.gameobjects;

public class BossNormal : Monstre
{
    const int vitesse = 40; 
    const int rayon = 80; 
    const int hp = 25; 
    const int dmg = 10; 
    const string type = "bossNormal";
    const int vitesseRot = 1;
    const float exp = 2;
    public BossNormal(int sides, EcranJeu ecranJeu, float ennemiDifficultee)
    : base(
        PolyGen.GetPoly(sides, rayon), 
        GenerateurMonstre.GenererPositionMonstreBordures(rayon, ecranJeu), 
        ecranJeu, 
        rayon, 
        type, 
        vitesse, 
        vitesseRot, 
        (float)Math.Round(hp * Math.Pow(ennemiDifficultee, 1.2)) * 100, 
        (float)Math.Round(exp + ennemiDifficultee/3) * 100, 
        (float)Math.Round(dmg * ennemiDifficultee)
    ){}
    public override void Update(float deltaT)
    {
        
        base.Update(deltaT);
    }
}
