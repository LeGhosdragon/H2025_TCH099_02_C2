
using System;
using System.Collections.Generic;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using desktop.gameobjects;
using MonoGame;

namespace desktop.gameobjects;

public class MonstreGunner : Monstre
{
    const int vitesse = 20;
    const int rayon = 15;
    const int hp = 15;
    const int dmg = 1;
    const string type = "gunner";
    const int vitesseRot = 0;
    const float exp = 4;
    public int _shootInterval;
    public int _lastShotTime;
    public int _currentTime;
    public bool _isOnCooldown;
    public int _vitesseBalle;

    public MonstreGunner(int sides, EcranJeu ecranJeu, float ennemiDifficultee)
    : base(
        getForme(),
        GenerateurMonstre.GenererPositionMonstreBordures(rayon, ecranJeu),
        ecranJeu,
        rayon,
        type,
        vitesse,
        vitesseRot,
        (float)Math.Round(hp * Math.Pow(ennemiDifficultee, 1.2)),
        (float)Math.Round(exp + 4 * ennemiDifficultee / 3),
        (float)Math.Round(dmg * ennemiDifficultee)
    )
    {
        _shootInterval = 2500;
        _lastShotTime = 0;
        _currentTime = 0;
        _isOnCooldown = false;
        _rayonBalles = 8;
        _vitesseBalle = 100;

    }
    protected static Vector2[] getForme(){
        return PolyGen.GetPoly(5, rayon);
    }
    public override void Update(float deltaT)
    {
        if((_ecranJeu._joueur.getPosition() - _position).Length() <= 450)
        {
            _currentTime += (int)(deltaT * 1000); // Convertir deltaT en millisecondes
            if (_currentTime - _lastShotTime >= _shootInterval && !_isOnCooldown)
            {
                Vector2 dir = _ecranJeu._joueur.getPosition() - _position;
                dir.Normalize();
                new ProjectileEnnemi(_position, this, dir * _vitesseBalle);
                _lastShotTime = _currentTime;
            }
        }
        base.Update(deltaT);
    }

}



public class ProjectileEnnemi
{
    private Vector2 _vitesse;
    private Vector2 _position;
    private Monstre _monstre;

    /// <summary>
    /// Verifie si il y a une collision avec le monstre
    /// </summary>
    /// <param name="monstre">monstre avec lequel la collision doit etre verifie</param>
    /// <returns>true si il y a une collision</returns>
    public void VerifierCollision(Joueur joueur)
    {
        Vector2 posM = joueur.getPosition();
        float rayM = joueur.getRayon();
        float dist = (posM - _position).Length();
        if (rayM + _monstre._rayonBalles > dist)
        {
            joueur.collision(_monstre.getDmg());
            MonstreGunner.EnleverProjectile(this);
        }
    }
    public ProjectileEnnemi(Vector2 position, Monstre monstre, Vector2 vitesse)
    {
        _vitesse = vitesse;
        _position = position;
        _monstre = monstre;
        MonstreGunner.AjouterProjectile(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(_position - Camera.getInstance().getPosition(), _monstre._rayonBalles, _monstre._rayonBalles, Color.Red, _monstre._rayonBalles);
    }

    public void Update(float deltaT, Joueur joueur)
    {
        _position += _vitesse * deltaT;
        VerifierCollision(joueur);
    }
}