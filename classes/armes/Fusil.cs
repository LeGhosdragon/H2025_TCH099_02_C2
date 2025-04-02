using System.Collections.Generic;
using desktop.gameobjects;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.armes;

public class Fusil : AbstractArme
{
    private List<ProjectileFusil> _projectiles;
    
    public Fusil(Joueur j, EcranJeu ecran) : base(new Vector2[]{new Vector2(0,0),new Vector2(0,20),new Vector2(50,20),new Vector2(50,0)}, j, 0.2f, 25, ecran)
    {
        _projectiles = new List<ProjectileFusil>();

    }
    public override void Update(float deltaT)
    {
        foreach(ProjectileFusil projectile in _projectiles){
            projectile.Update(deltaT);
        }
        base.Update(deltaT);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        foreach(ProjectileFusil projectile in _projectiles){
            projectile.Draw(spriteBatch);
        }
    }

    public override void utiliser()
    {
        Vector2 dir = Camera.getInstance().getPosSourisCamera();
        dir.Normalize();
        _projectiles.Add(new ProjectileFusil(this._position, dir * 1000));
    }
}
public class ProjectileFusil : AbstractGameObject
{
    private Vector2 _vitesse;
    public ProjectileFusil(Vector2 position,Vector2 vitesse) : base(PolyGen.GetPoly(100,10), position, 1)
    {
        this._vitesse = vitesse;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(this._position - Camera.getInstance().getPosition(),10,10,Color.Red);
    }

    public void Update(float deltaT)
    {
        this._position += _vitesse * deltaT;
    }
}