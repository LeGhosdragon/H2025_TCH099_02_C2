
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

public class BossGunner : Monstre
{
    static List<ProjectileEnnemi> _projectiles = new List<ProjectileEnnemi>();
    const int vitesse = 20; 
    const int rayon = 10; 
    const int hp = 15; 
    const int dmg = 1; 
    const string type = "bossGunner";
    const int vitesseRot = 0;
    const float exp = 4;
    public int _shootInterval;
    public int _lastShotTime;
    public int _currentTime;
    public bool _isOnCooldown;
    public int _vitesseBalle;

    public BossGunner(int sides, EcranJeu ecranJeu, float ennemiDifficultee)
    : base(
        PolyGen.GetPoly(sides, rayon), 
        GenerateurMonstre.GenererPositionMonstreBordures(rayon, ecranJeu), 
        ecranJeu, 
        rayon, 
        type, 
        vitesse, 
        vitesseRot, 
        (float)Math.Round(hp * Math.Pow(ennemiDifficultee, 1.2) * 100), 
        (float)Math.Round(exp + 4 * ennemiDifficultee/3 )* 100, 
        (float)Math.Round(dmg * ennemiDifficultee)
    ){
        _shootInterval = 1250;
        _lastShotTime = 0;
        _currentTime = 0;
        _isOnCooldown = false;
        _rayonBalles = 8 * 3;
        _vitesseBalle = 100;
        AjouterBoss(this);
    }
    public override void Update(float deltaT)
    {
        if((_ecranJeu._joueur.getPosition() - _position).Length() <= 450)
        {
            _currentTime += (int)(deltaT * 1000); // Convertir deltaT en millisecondes
            Vector2 dir = _ecranJeu._joueur.getPosition() - _position;
            if (_currentTime - _lastShotTime >= _shootInterval && !_isOnCooldown && dir.Length() <= 450)
            {
                dir.Normalize();
                new ProjectileEnnemi(_position, this, dir * _vitesseBalle);
                _lastShotTime = _currentTime;
            }
        }
        base.Update(deltaT);
    }

    public override void bouger(Vector2 position, float deltaT)
    {
        Vector2 dir = position - _position;
        if(dir.Length() >= 400){
            dir.Normalize();
            _position += dir * _vitesse * deltaT;}
    }
}