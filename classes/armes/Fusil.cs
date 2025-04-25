using System.Collections.Generic;
using System.Linq;
using desktop.gameobjects;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.armes;

public class Fusil : AbstractArme
{
    //Attributs selon ameliorations
    public int _rayonBalles {get;set;} = 6; //UpgGrandeurFusil -> 6, + 3 
    public float _degat {get;set;} = 15 * 1.7f; //UpgDegatFusil ->  15 * 1.7f,  * 1.2 
    public int _pierce {get;set;} = 1; //UpgPierceFusil -> 1, + 1
    public float _vitesse {get;set;} = 500;
    public float _recul {get;set;} = 50; //UpgReculEpee


    private List<ProjectileFusil> _projectiles;
    public Fusil(Joueur j, EcranJeu ecran) : base(new Vector2[] { new Vector2(0, 0), new Vector2(0, 15), new Vector2(15, 15), new Vector2(15, 0) }, j, 1f, new Vector2(15,15), ecran)
    {
        _projectiles = new List<ProjectileFusil>();
    }
    public override void Update(float deltaT)
    {
        foreach (ProjectileFusil projectile in _projectiles.Reverse<ProjectileFusil>())
        {
            projectile.Update(deltaT, _ecran.GetMonstres());
        }
        base.Update(deltaT);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        Peintre.dessinerForme(spriteBatch,_forme,_position,1);
        foreach (ProjectileFusil projectile in _projectiles)
        {
            projectile.Draw(spriteBatch);
        }
    }

    public override void utiliser()
    {
        Vector2 dir = Camera.getInstance().getPosSourisCamera();
        dir.Normalize();
        _projectiles.Add(new ProjectileFusil(_position- new Vector2(dir.Y,-dir.X) * _dimensions.X/2,this, dir * _vitesse));
    }
    public void EnleverProjectile(ProjectileFusil projectile){
        this._projectiles.Remove(projectile);
    }
    public Joueur getJoueur()
    {
        return _joueur;
    }
    
}
public class ProjectileFusil
{
    private List<Monstre> _frappes;
    private Vector2 _vitesse;
    private Vector2 _position;
    private Fusil _fusil;
    private int _pierce;

    /// <summary>
    /// Verifie les collision et inflige du degat au monstres
    /// </summary>
    /// <param name="monstres">monstres avec lesquelles le collisions doivent etre verifie</param>
    public void VerifierCollisions(List<Monstre> monstres)
    {

        foreach (Monstre monstre in monstres)
        {
            if (!_frappes.Contains(monstre) && VerifierCollision(monstre) && _pierce >0)
            {
                _frappes.Add(monstre);
                _fusil.AppliquerDegat(monstre,_fusil._degat);
                monstre.AjouterRecul(_fusil.getJoueur().getPosition(),_fusil._recul);
                _pierce--;
            }
        }
    }
    /// <summary>
    /// Verifie si il y a une collision avec le monstre
    /// </summary>
    /// <param name="monstre">monstre avec lequel la collision doit etre verifie</param>
    /// <returns>true si il y a une collision</returns>
    public bool VerifierCollision(Monstre monstre)
    {
        Vector2 posM = monstre.getPosition();
        float rayM = monstre.getRayon();
        float dist = (posM - _position).Length();
        return rayM +_fusil._rayonBalles > dist;
    }
    public ProjectileFusil(Vector2 position, Fusil fusil,Vector2 vitesse)
    {   
        this._vitesse = vitesse;
        this._position = position;
        this._fusil = fusil;
        this._pierce = fusil._pierce;
        _frappes = new List<Monstre>();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(this._position - Camera.getInstance().getPosition(),_fusil._rayonBalles,_fusil._rayonBalles , Color.Red, _fusil._rayonBalles);
    }

    public void Update(float deltaT, List<Monstre> monstres)
    {
        this._position += _vitesse * deltaT;
        VerifierCollisions(monstres);
        if(_pierce <= 0){
            _fusil.EnleverProjectile(this);
        }
    }
}