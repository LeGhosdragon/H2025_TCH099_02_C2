using System.Collections.Generic;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace desktop.armes;

public class Epee : AbstractArme
{
    private List<AttaqueEpee> attaques;
    private float _longueur;

    public float GetLongueur()
    {
        return _longueur;
    }

    public Epee(Joueur joueur)
        : base(PolyGen.GetPoly(4, 5), joueur.getPosition(), joueur, 1f, 5)
    {
        this.attaques = new List<AttaqueEpee>();
        _longueur = 100;
    }

    public override void utiliser()
    {
        attaques.Add(new AttaqueEpee(Camera.getInstance().getPosSourisCamera(), this));
    }

    public override void Update(float deltaT)
    {
        base.Update(deltaT);
        foreach (AttaqueEpee attaqueEpee in attaques)
        {
            attaqueEpee.Update(deltaT);
        }
    }

    public void EnleverAttaque(AttaqueEpee attaque)
    {
        attaques.Remove(attaque);
    }

    /*
    Section Graphique
    */
    public override void Draw(GraphicsDevice device, SpriteBatch spriteBatch)
    {
        foreach (AttaqueEpee attaque in attaques)
        {
            attaque.Draw(device, spriteBatch);
        }
        base.Draw(device, spriteBatch);
    }

    public Joueur GetJoueur()
    {
        return _joueur;
    }
}

public class AttaqueEpee
{
    private Vector2 _debut;
    private Vector2 _act;
    private Epee _epee;

    public AttaqueEpee(Vector2 dir, Epee epee)
    {
        this._debut = dir;
        this._act = dir;
        this._epee = epee;
    }

    public void Update(float deltaT)
    {
        if (!_epee.GetAttaqueAutomatique() && Mouse.GetState().LeftButton != ButtonState.Pressed)
        {
            _epee.EnleverAttaque(this);
            return;
        }
    }

    public void Draw(GraphicsDevice device, SpriteBatch spriteBatch)
    {
    }
}
