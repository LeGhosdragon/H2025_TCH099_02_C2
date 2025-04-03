using System;
using System.Collections.Generic;
using System.Linq;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.gameobjects;

public class Experience : IGameObject
{
    protected int _valeur;
    protected Vector2 _position;
    protected EcranJeu _ecranJeu;

    /// <summary>
    /// Utilise pour eviter d'updater si lexperience a deja ete fusionne ou collecte
    /// </summary>
    protected bool _doitUpdate = true;
    public Experience(Vector2 position, int valeur, EcranJeu ecranJeu)
    {
        this._valeur = valeur;
        this._position = position;
        this._ecranJeu = ecranJeu;
        ecranJeu.AjouerObjet(this);
    }
    public void Update(float deltaT)
    {

        if(_doitUpdate){
            UpdatePos(deltaT);
            Cleanup(deltaT);
        }
        if(_doitUpdate & sortJeu()){
            _ecranJeu.ajouteBanqueExp(_valeur);
            Enlever();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(_position - Camera.getInstance().getPosition(), _valeur, _valeur, Color.Blue, _valeur);
    }
    public void UpdatePos(float deltaT)
    {
        //TODO devrait etre une statistique du joueur
        float distanceDattraction = 1000;

        Joueur joueur = _ecranJeu._joueur;
        Vector2 posJoueur = joueur.getPosition();
        Vector2 dir = posJoueur - _position;

        if (dir.Length() < joueur.getRayon() + _valeur)
        {
            Collect(joueur);
        }

        // Application de la force d'attraction vers le joueur
        if (dir.Length() < distanceDattraction + _valeur)
        {

            double attraction = 0.5 / Math.Max(dir.Length(), 100);

            // Application de la velocité avec damping
            _position += dir * (float)attraction * deltaT * 95f;
        }



        // Changer la couleur dynamiquement (effet arc-en-ciel pulsant)
        /* const time = Date.now() / 100;
         const red = Math.sin(time) * 127 + 128;
         const green = Math.sin(time + 2) * 127 + 128;
         const blue = Math.sin(time + 4) * 127 + 128;
         const color = (red << 16) | (green << 8) | blue;*/

    }
    public void Cleanup(float deltaT)
    {
        foreach(Experience exp in _ecranJeu.GetExperiences()){
            if(exp != this){
                validerFusion(exp,deltaT);
            }
        }
    }
    public bool validerFusion(Experience exp,float deltaT){
        if(exp._doitUpdate){
            Vector2 diff = exp._position - _position;


                if (diff.Length() < 100) { // Attraction entre les EXPs
                    float attraction = (float) 0.2 / Math.Max(diff.Length(), 10);
                    exp._position += diff * attraction * deltaT;
                    _position += diff * attraction * deltaT;
                }

                if (diff.Length() < exp._valeur + _valeur) { // Fusion
                    if(_valeur > exp._valeur){
                        _valeur += exp._valeur;
                        exp.Enlever();
                    }else{
                        exp._valeur +=_valeur;
                        this.Enlever();
                    }

                }
        }
        return false;
    }
    protected bool sortJeu(){
        float marges = 0.4f;
        int largeur =_ecranJeu.GraphicsDevice.Viewport.Width;
        int hauteur =_ecranJeu.GraphicsDevice.Viewport.Height;
        //Depasse a droite
        if(_position.X >  Camera.getInstance().getPosition().X +largeur + largeur * marges){
         return true;
        }
        //Depasse a gauche
        if(_position.X < Camera.getInstance().getPosition().X - largeur* marges){
            return true;
        }
        //Depasse en haut
        if (_position.Y > Camera.getInstance().getPosition().Y +hauteur + hauteur* marges){
            return true;
        }
        //Deppase en bas
        if(_position.Y < Camera.getInstance().getPosition().Y -hauteur * marges){
            return true;
        }

        return false;
    }
    public void EmpecherUpdate(){
        _doitUpdate = false;
    }
    protected void Enlever(){
        _ecranJeu.EnleverObjet(this);
        _doitUpdate = false;
    }

    public void Collect(Joueur joueur)
    {
        joueur.ajouterExperience(_valeur);
        Enlever();
    }
}