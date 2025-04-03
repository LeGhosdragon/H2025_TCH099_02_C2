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
            projectile.Update(deltaT,_ecran.GetMonstres());
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
        _projectiles.Add(new ProjectileFusil(this._position, dir * 1000,10));
    }
}
public class ProjectileFusil
{
    private List<Monstre> _frappes;
    private Vector2 _vitesse;
    private Vector2 _position;
    private int _rayon;
    private int _pierce;

    public void VerifierCollisions(List<Monstre> monstres){
        
        foreach(Monstre monstre in monstres){
            if(!_frappes.Contains(monstre) && VerifierCollision(monstre)){
                _frappes.Add(monstre);
                monstre.RecevoirDegat(10);
            }
        }
    }
    public bool VerifierCollision(Monstre monstre){
        Vector2 posM = monstre.getPosition();
        float rayM = monstre.getRayon();
        float dist = (posM - _position).Length();
        return rayM + _rayon > dist;
    }
    public ProjectileFusil(Vector2 position,Vector2 vitesse,int rayon)
    {
        this._vitesse = vitesse;
        this._position = position;
        this._rayon = rayon;
        _frappes = new List<Monstre>();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(this._position - Camera.getInstance().getPosition(),_rayon,_rayon,Color.Red);
    }

    public void Update(float deltaT,List<Monstre> monstres)
    {
        this._position += _vitesse * deltaT;
        VerifierCollisions(monstres);
    }
}