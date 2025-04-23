using System;
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
    private Color cAvant = Color.Red;
    public void Draw(SpriteBatch spriteBatch)
    {
        //Changer la couleur dynamiquement (effet arc-en-ciel pulsant)



        DateTimeOffset date = DateTimeOffset.UtcNow;
        double time = date.ToUnixTimeMilliseconds()/100;
        double red = Math.Sin(time) * 127 + 128;
        double green = Math.Sin(time + 2) * 127 + 128;
        double blue = Math.Sin(time + 4) * 127 + 128;
        Color color = new Color((int)red, (int)green, (int)blue);
        spriteBatch.DrawCircle(_position - Camera.getInstance().getPosition(), _valeur/2, _valeur, color, _valeur/2);
    }
    public void UpdatePos(float deltaT)
    {
        Joueur joueur = _ecranJeu._joueur;
        float distanceDattraction = joueur._rayonCollection;

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