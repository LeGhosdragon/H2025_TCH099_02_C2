using System;
using System.Collections.Generic;
using System.Linq;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.gameobjects;

public class GenerateurExplosion : IGameObject
{

    public Joueur _joueur { get; set; }
    public List<Explosion> _explosions { get; set; } = new List<Explosion>();
    public float _rayonMax { get; set; } = 0;
    public float _recul { get; set; } = 20;
    public float _degat {get;set;} = 10;
    public EcranJeu _ecranJeu {get;set;}
    public int niveau {get;set;} = 0;
    public GenerateurExplosion(Joueur joueur,EcranJeu ecranJeu)
    {
        _ecranJeu = ecranJeu;
        _joueur = joueur;

    }
    public void GenererExplosion()
    {
        _explosions.Add(new Explosion(this));
    }
    public void Ameliorer(){
        switch (niveau){
            case 0:
                _rayonMax = _joueur.getRayon() * 6;
            break;
            case 1:
                _rayonMax *= 1.3f;
            break;
            case 2:
                _rayonMax *= 1.3f;
            break;
            case 3:
                _recul *= 2;
            break;
        }
        niveau++;
    }
    public void Update(float deltaT)
    {
        foreach(Explosion explosion in _explosions.Reverse<Explosion>()){
            explosion.Update(deltaT);
        }
    }
    public void EnleverExplosion(Explosion explosion){
        _explosions.Remove(explosion);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(Explosion explosion in _explosions){
            explosion.Draw(spriteBatch);
        }
    }
    public List<Monstre> GetMonstres(){
        return _ecranJeu.GetMonstres();
    }
}



public class Explosion : IGameObject
{
    private float _rayon = 0;
    private Vector2 _position;
    private GenerateurExplosion _generateur;
    private List<Monstre> _frappes = new List<Monstre>();
    public Explosion(GenerateurExplosion generateur){
        _generateur = generateur;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(_position - Camera.getInstance().getPosition(), _rayon, (int)_rayon, Color.White);
    }

    public void Update(float deltaT)
    {
        if (_rayon >= _generateur._rayonMax)
        {
            _generateur.EnleverExplosion(this);
            return;
        }
        _position = _generateur._joueur.getPosition();

        //Le 3 est pour correspondre a la duree dans le code web qui correspond a 20 frames donc environ 1/3 de secondes
        _rayon += _generateur._rayonMax * deltaT * 3;

        ValiderCollisions(_generateur.GetMonstres());
    }
    public void ValiderCollisions(List<Monstre> monstres)
    {

        foreach (Monstre monstre in monstres)
        {
            if (!_frappes.Contains(monstre) && VerifierCollision(monstre))
            {
                _frappes.Add(monstre);
                monstre.RecevoirDegat(_generateur._degat);
                monstre.AjouterRecul(_position,_generateur._recul);                
            }
        }
    }
    public bool VerifierCollision(Monstre monstre){
        float distance = Vector2.Distance(_position, monstre.getPosition());
        return _rayon + monstre.getRayon() >= distance;
    }
}