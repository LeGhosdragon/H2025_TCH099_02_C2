using System;
using System.Collections.Generic;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public class Monstre : AbstractGameObject
{
    private EcranJeu _ecranJeu;
    private float _vitesse;
    private float _vitesseRot;
    private float _rayon;
    private float hp;

    public Monstre(Vector2[] forme, Vector2 position, EcranJeu ecranJeu, float rayon)
        : base(forme, position, 1)
    {
        this._ecranJeu = ecranJeu;
        this._vitesse = 20;
        this._vitesseRot = 1;
        this._rayon = rayon;
        this.hp = 20;
    }
    /// <summary>
    /// Rayon de la collision de la forme
    /// </summary>
    /// <returns>Le rayon de la collision de la forme</returns>
    public float getRayon(){
        return _rayon;
    }

    public override void Update(float deltaT)
    {
        bouger(_ecranJeu._joueur.getPosition(), deltaT);
        eviterCollisions(deltaT);
        if (collisionJoueur()) { }
    }

    /// <summary>
    /// Evite les collisions avec tout les monstres
    /// </summary>
    /// <param name="deltaT">difference entre la derniere update</param>
    private void eviterCollisions(float deltaT)
    {
        List<Monstre> monstres = _ecranJeu.GetMonstres();
        monstres.Remove(this);
        monstres.ForEach(m => eviterCollisions(m, deltaT));
    }

    /// <summary>
    /// Evite les collisions entre deux monstres
    /// </summary>
    /// <param name="monstre">monstre a evite</param>
    /// <param name="deltaT">difference entre la derniere update</param>
    private void eviterCollisions(Monstre monstre, float deltaT)
    {
        float minDistance = 1.2f * _forme[0].Length();
        float facteurEviter = 0.5f;

        Vector2 dif = _position - monstre._position;
        float distance = dif.Length();

        if (distance < minDistance)
        {
            Vector2 eviter = dif * facteurEviter;
            _position += eviter * deltaT;
        }
    }
    /// <summary>
    /// Enleve le nombre de degat au monstre
    /// Si sa vie est sous zero, le tue
    /// </summary>
    /// <param name="degat">Nombre de degat subit</param>
    /// <returns>true si le monstre est mort</returns>
    public bool RecevoirDegat(float degat){
        this.hp -= degat;
        if(hp <= 0){
            Mourrir();
            return true;
        }
        return false;
    }

    public void Mourrir(){
            _ecranJeu.EnleverObjet(this);
            new Experience(this._position,10,_ecranJeu);
    }

    /// <summary>
    /// Bouge et fait tourner l'ennemi selon sa vitesse et sa vitesse de rotation
    /// </summary>
    /// <param name="posJoueur">posiition du joueur</param>
    /// <param name="deltaT">difference de temps</param>
    public void bouger(Vector2 posJoueur, float deltaT)
    {
        Vector2 mouvement = Vector2.Normalize(posJoueur - _position);
        _position += mouvement * deltaT * _vitesse;

        this._forme = PolyGen.tournerMatrice(_forme, _vitesseRot * deltaT);
    }

    /// <summary>
    /// Determine si le joueur entre en colision avec ce monstre
    /// </summary>
    /// <returns>True si le joueur est sur le monstre</returns>
    public bool collisionJoueur()
    {
        Vector2 posJ = _ecranJeu._joueur.getPosition();
        float rayJ = _ecranJeu._joueur.getRayon();
        float dist = (posJ - _position).Length();
        return rayJ + _rayon > dist;
    }
}
