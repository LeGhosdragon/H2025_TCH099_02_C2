using System.Collections.Generic;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.armes;

public class Epee : AbstractArme
{
    private List<AttaqueEpee> attaques;

    public Epee(Joueur joueur)
        : base(PolyGen.GetPoly(4, 5), joueur.getPosition(), joueur, 1f, 5)
    {
        this.attaques = new List<AttaqueEpee>();
    }

    public override void utiliser()
    {
        attaques.Add(new AttaqueEpee(Camera.getInstance().getPosSourisCamera()));
    }

    public override void Update(float deltaT)
    {
        base.Update(deltaT);
    }

    /*
    Section Graphique
    */
    public override void Draw(GraphicsDevice device)
    {
        base.Draw(device);
    }
}

class AttaqueEpee
{
    private Vector2 _debut;
    private Vector2 _act;

    public AttaqueEpee(Vector2 dir) { 
        this._debut = dir;
        this._act = dir;
    }
}
