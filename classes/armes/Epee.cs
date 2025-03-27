using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace desktop.armes;

public class Epee : AbstractArme
{
    /// <summary>
    /// Liste d'attaques qui sont actives
    /// </summary>
    private List<AttaqueEpee> attaques;
    private float _angleZone = (float)Math.PI / 2;
    private float _vitRot = (float)Math.PI / 2;

    public Epee(Joueur joueur)
        : base(
            new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(0, 100),
                new Vector2(10, 100),
                new Vector2(10, 0),
            },
            joueur.getPosition(),
            joueur,
            1f,
            5
        )
    {
        this.attaques = new List<AttaqueEpee>();
    }

    /// <summary>
    /// Cree une nouvelle attaque
    /// </summary>
    public override void utiliser()
    {
        attaques.Add(new AttaqueEpee(getDir(), this));
    }

    public override void Update(float deltaT)
    {
        base.Update(deltaT);
        foreach (AttaqueEpee attaqueEpee in attaques)
        {
            attaqueEpee.Update(deltaT);
        }
    }

    /// <summary>
    /// Permet d'enlever une attaque lorsqu'elle est terminee
    /// </summary>
    /// <param name="attaque">Attaque a enlever</param>
    public void EnleverAttaque(AttaqueEpee attaque)
    {
        attaques.Remove(attaque);
    }

    /// <summary>
    /// Permet d'obtenir la vitesse de rotation dde l'epee
    /// </summary>
    /// <returns>La vitesse a laquelle </returns>
    public float getVitRot()
    {
        return _vitRot;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach (AttaqueEpee attaque in attaques)
        {
            attaque.Draw(spriteBatch);
        }
        base.Draw(spriteBatch);
    }

    /// <summary>
    /// Permet de rafraichir la position actuelle de l'arme
    /// </summary>
    /// <param name="deltaT"></param>
    protected override void UpdatePos(float deltaT)
    {
        //Place l'arme dans la direction ou la souris pointe
        _position = _joueur.getPosition() + getDir() * (_joueur.getRayon() + _longueur / 2);
        _forme = PolyGen.tournerMatrice(_formeBase, PolyGen.angleVecteur(getDir()) - _angleZone);
    }

    /// <summary>
    /// Obtiens la direction ou l'arme doit etre affichee
    /// </summary>
    /// <returns>vecteur de direction normalize</returns>
    protected Vector2 getDir()
    {
        Vector2 dir = Camera.getInstance().getPosSourisCamera();
        dir = PolyGen.tournerMatrice(dir, _angleZone / 2);

        dir.Normalize();
        return dir;
    }
}

/// <summary>
/// Classe unitilisee pour chaque attaque d'epee
/// </summary>
public class AttaqueEpee : AbstractGameObject
{
    private Vector2 _debut;
    private Vector2 _act;
    private Epee _epee;

    public AttaqueEpee(Vector2 angle, Epee epee)
        : base(epee.getFormeBase(), epee.getPosition(), 2)
    {
        this._debut = angle;
        this._act = angle;
        this._epee = epee;
    }

    public override void Update(float deltaT)
    {
        //Annule l'attaque si le joueur n'attaque plus
        if (!_epee.GetAttaqueAutomatique() && Mouse.GetState().LeftButton != ButtonState.Pressed)
        {
            _epee.EnleverAttaque(this);
            return;
        }
        //Tourne l'épée
        this._act = PolyGen.tournerMatrice(_act, deltaT * _epee.getVitRot());
    }

    public override void Draw(SpriteBatch spriteBatch) { }
}
