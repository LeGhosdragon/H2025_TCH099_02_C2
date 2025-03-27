using System;
using System.Collections.Generic;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public class Monstre : AbstractGameObject
{
    private PageJeu _pageJeu;
    private float _vitesse;
    private float _vitesseRot;
    private float _rayon;

    public Monstre(Vector2[] forme, Vector2 position, PageJeu pageJeu, float rayon)
        : base(forme, position, 1)
    {
        this._pageJeu = pageJeu;
        this._vitesse = 20;
        this._vitesseRot = 1;
        this._rayon = rayon;
    }
    public float getRayon(){
        return _rayon;
    }

    public override void Update(float deltaT)
    {
        bouger(_pageJeu.getJoueur().getPosition(), deltaT);
        eviterCollisions(deltaT);
        if (collisionJoueur()) { }
    }

    /// <summary>
    /// Evite les collisions avec tout les monstres
    /// </summary>
    /// <param name="deltaT">difference entre la derniere update</param>
    private void eviterCollisions(float deltaT)
    {
        List<Monstre> monstres = _pageJeu.GetMonstres();
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
        Vector2 posJ = _pageJeu.getJoueur().getPosition();
        float rayJ = _pageJeu.getJoueur().getRayon();
        float dist = (posJ - _position).Length();
        return rayJ + _rayon > dist;
    }
}
