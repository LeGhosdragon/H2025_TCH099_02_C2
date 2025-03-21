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
        eviterCollisions();
    }

    private void eviterCollisions()
    {
        List<Monstre> monstres = _pageJeu.GetMonstres();
        monstres.Remove(this);
        monstres.ForEach(eviterCollisions);
    }

    private void eviterCollisions(Monstre monstre)
    {
        float minDistance = 1.5f * _forme[0].Length();
        float facteurEviter = 0.5f;

        Vector3 dif = _position - monstre._position;
        float distance = dif.Length();

        if (distance < minDistance)
        {
            Vector3 eviter = dif * facteurEviter;
            _position += eviter;
        }
    }

    /*    avoidMonsterCollision() {
    
    
            Monstre.monstres.forEach(otherMonstre => {
                if (this === otherMonstre) return;
                let dx = this.getX() - otherMonstre.getX();
                let dy = this.getY() - otherMonstre.getY();
                let distance = Math.sqrt(dx * dx + dy * dy);
    
                if (distance < minDistance) {
                    let angle = Math.atan2(dy, dx);
                    let avoidX = Math.cos(angle) * avoidFactor;
                    let avoidY = Math.sin(angle) * avoidFactor;
                    this.setX(this.getX() + avoidX);
                    this.setY(this.getY() + avoidY);
                }
            });
        }
    */
    public void bouger(Vector3 posJoueur, float deltaT)
    {
        Vector3 mouvement = Vector3.Normalize(posJoueur - _position);
        _position += mouvement * deltaT * _vitesse;

        this._forme = PolyGen.tournerMatrice(_forme, _vitesseRot * deltaT);
    }
}
