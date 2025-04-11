using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.Arm;
using desktop.gameobjects;
using desktop.pages;
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
    private List<AttaqueEpee> _aEnlever;
    private float _angleZone = (float)Math.PI / 2;
    private float _vitRot = (float)Math.PI;

    public Epee(Joueur joueur, EcranJeu ecranJeu)
        : base(
            [
                new Vector2(0, 0),
                new Vector2(0, 100),
                new Vector2(10, 100),
                new Vector2(10, 0),
            ],
            joueur,
            1f,
            new Vector2(10,100),
            ecranJeu
        )
    {
        this.attaques = new List<AttaqueEpee>();
        this._aEnlever = new List<AttaqueEpee>();
    }

    /// <summary>
    /// Cree une nouvelle attaque
    /// </summary>
    public override void utiliser()
    {
        attaques.Add(new AttaqueEpee(PolyGen.angleVecteur(getDir()), this));
    }

    public override void Update(float deltaT)
    {
        base.Update(deltaT);
        foreach (AttaqueEpee attaqueEpee in attaques)
        {
            attaqueEpee.Update(deltaT, _ecran.GetMonstres());
        }
        attaques.RemoveAll(e => _aEnlever.Contains(e));
        _aEnlever = new List<AttaqueEpee>();
    }

    /// <summary>
    /// Permet d'enlever une attaque lorsqu'elle est terminee
    /// </summary>
    /// <param name="attaque">Attaque a enlever</param>
    public void EnleverAttaque(AttaqueEpee attaque)
    {
        _aEnlever.Add(attaque);
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
    }

    /// <summary>
    /// Permet de rafraichir la position actuelle de l'arme
    /// </summary>
    /// <param name="deltaT">temps depuis le dernier appel</param>
    protected override void UpdatePos(float deltaT)
    {
        //Place l'arme dans la direction ou la souris pointe
        _position = _joueur.getPosition() + getDir() * (_joueur.getRayon() + _dimensions.Y / 2);
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

    public float getAngleZone()
    {
        return _angleZone;
    }

    public Joueur getJoueur()
    {
        return _joueur;
    }
}

/// <summary>
/// Classe unitilisee pour chaque attaque d'epee
/// </summary>
public class AttaqueEpee
{
    private float _debut;
    private float _act;
    private Epee _epee;
    private Vector2 _position;
    private Vector2[] _forme;
    private List<Monstre> frappes;

    public AttaqueEpee(float angle, Epee epee)
    {
        this._debut = angle;
        this._act = angle;
        this._epee = epee;
        frappes = new List<Monstre>();
    }

    public void Update(float deltaT, List<Monstre> monstres)
    {
        //Annule l'attaque si le joueur n'attaque plus
        if (
            !_epee._attaqueAutomatique && Mouse.GetState().LeftButton != ButtonState.Pressed
            || _act < _debut - _epee.getAngleZone()
        )
        {
            _epee.EnleverAttaque(this);
            return;
        }
        DetecterCollisions(monstres, deltaT);
        //Tourne l'épée

        _act -= _epee.getVitRot() * deltaT;

        this._forme = PolyGen.tournerMatrice(_epee._formeBase, _act - _epee.getAngleZone());
        Vector2 v =
            _epee.getJoueur().getPosition()
            + new Vector2((float)Math.Cos(_act), (float)Math.Sin(_act))
                * _epee.getJoueur().getRayon();
        this._position = v;
    }

    public void DetecterCollisions(List<Monstre> monstres, float deltaT)
    {
        foreach (Monstre monstre in monstres)
        {
            if (!frappes.Contains(monstre) & DetecterCollision(monstre, deltaT))
            {
                frappes.Add(monstre);
                monstre.RecevoirDegat(10);
            }
        }
    }

    public bool DetecterCollision(Monstre monstre, float deltaT)
    {
        //Collision avec un des points
        Vector2[] forme = getZoneDegat(deltaT);


        if (collisionLigneCercle(monstre.getPosition(), monstre.getRayon(), forme[0], forme[1]))
        {
            return true;
        }
        if (collisionLigneCercle(monstre.getPosition(), monstre.getRayon(), forme[1], forme[2]))
        {
            return true;
        }
        if (collisionLigneCercle(monstre.getPosition(), monstre.getRayon(), forme[2], forme[0]))
        {
            return true;
        }




        return false;
    }
    private bool collisionLigneCercle(Vector2 pos, float rayon, Vector2 p1, Vector2 p2)
    {
        Vector2 dif = p2 - p1;


        float A = dif.LengthSquared();
        float B = 2 * (dif.X * (p1.X - pos.X) + dif.Y * (p1.Y - pos.Y));
        float C = (p1.X - pos.X) * (p1.X - pos.X) + (p1.Y - pos.Y) * (p1.Y - pos.Y) - rayon * rayon;

        float det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            return false;
        }
        return true;
    }

    public Vector2[] getZoneDegat(float deltaT)
    {
        Vector2[] zone = new Vector2[3];

        zone[0] = _epee.getJoueur().getPosition();
        zone[1] =
            zone[0]
            + new Vector2((float)Math.Cos(_act), (float)Math.Sin(_act))
                * (_epee.getJoueur().getRayon() + _epee._dimensions.Y);
        float apres = _act - _epee.getVitRot() * deltaT;
        zone[2] =
            zone[0]
            + new Vector2((float)Math.Cos(apres), (float)Math.Sin(apres))
                * (_epee.getJoueur().getRayon() + _epee._dimensions.Y);

        return zone;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Peintre.dessinerForme(spriteBatch, _forme, _position);
    }
}
