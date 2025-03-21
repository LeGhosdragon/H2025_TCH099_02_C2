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

    public Monstre(Vector2[] forme, Vector3 position, PageJeu pageJeu)
        : base(forme, position)
    {
        this._pageJeu = pageJeu;
        this._vitesse = 20;
        this._vitesseRot = 1;
    }

    public override void Update(float deltaT)
    {
        bouger(_pageJeu.getJoueur().getPosition(), deltaT);
        eviterCollisions(deltaT);
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

        Vector3 dif = _position - monstre._position;
        float distance = dif.Length();

        if (distance < minDistance)
        {
            Vector3 eviter = dif * facteurEviter;
            _position += eviter * deltaT;
        }
    }

    /// <summary>
    /// Bouge et fait tourner l'ennemi selon sa vitesse et sa vitesse de rotation
    /// </summary>
    /// <param name="posJoueur">posiition du joueur</param>
    /// <param name="deltaT">difference de temps</param>
    public void bouger(Vector3 posJoueur, float deltaT)
    {
        Vector3 mouvement = Vector3.Normalize(posJoueur - _position);
        _position += mouvement * deltaT * _vitesse;

        this._forme = PolyGen.tournerMatrice(_forme, _vitesseRot * deltaT);
    }
}
