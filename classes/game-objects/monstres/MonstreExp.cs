
using System;
using System.Collections.Generic;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace desktop.gameobjects;

public class MonstreExp : Monstre
{
    const int vitesse = 20;
    const int rayon = 40;
    const int hp = 250;
    const int dmg = 1;
    const string type = "exp";
    const int vitesseRot = 1;
    const float exp = 2;
    public MonstreExp(EcranJeu ecranJeu, float ennemiDifficultee)
    : base(
        PolyGen.GetPoly(3, rayon),
        GenerateurMonstre.GenererPositionMonstreBordures(rayon, ecranJeu),
        ecranJeu,
        rayon,
        type,
        vitesse,
        vitesseRot,
        (float)Math.Round(hp * Math.Pow(ennemiDifficultee, 1.2)),
        (float)Math.Round(exp + ennemiDifficultee / 3),
        (float)Math.Round(dmg * ennemiDifficultee)
    )
    { }

    public override void Mourrir()
    {
        _ecranJeu.EnleverObjet(this);
        _ecranJeu._score._ennemisEnleve += 1;
        new Experience(_position, _ecranJeu._banqueExp / 2, _ecranJeu);
        _ecranJeu._banqueExp -= _ecranJeu._banqueExp / 2;
    }
    public override void Update(float deltaT)
    {
        base.Update(deltaT);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        DateTimeOffset date = DateTimeOffset.UtcNow;
        double time = date.ToUnixTimeMilliseconds()/100;
        double red = Math.Sin(time) * 127 + 128;
        double green = Math.Sin(time + 2) * 127 + 128;
        double blue = Math.Sin(time + 4) * 127 + 128;
        Color color = new Color((int)red, (int)green, (int)blue);
        Peintre.dessinerForme(spriteBatch, _forme, _position,color,3);
    }
}
