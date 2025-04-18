using System;
using System.Collections.Generic;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public class Monstre : AbstractGameObject
{
    static List<ProjectileEnnemi> _projectiles = new List<ProjectileEnnemi>();
    static List<Monstre> _bosses = new List<Monstre>();
    protected EcranJeu _ecranJeu;
    protected float _vitesse;
    protected float _vitesseRot;
    protected float _rayon;
    protected float _hp;
    protected string _type;
    protected float _exp;
    protected float _dmg;
    protected bool _text;
    public int _rayonBalles;

    public Monstre(Vector2[] forme, Vector2 position, EcranJeu ecranJeu, float rayon, string type = "normal", float vitesse = 20, int vitesseRot = 1, float hp = 15, float exp = 1, float dmg = 1)
        : base(forme, position, 1)
    {
        _ecranJeu = ecranJeu;
        _vitesse = vitesse;
        _vitesseRot = vitesseRot;
        _rayon = rayon;
        _hp = hp;
        _type = type;
        _exp = exp;
        _dmg = dmg;
        _text = false;
    }
    /// <summary>
    /// Rayon de la collision de la forme
    /// </summary>
    /// <returns>Le rayon de la collision de la forme</returns>
    public float getRayon()
    {
        return _rayon;
    }

    public override void Update(float deltaT)
    {
        AppliquerRecul(deltaT);
        bouger(_ecranJeu._joueur.getPosition(), deltaT);
        eviterCollisions(deltaT);
        if (collisionJoueur())
        {
            _ecranJeu._joueur.collision(_dmg);
        }
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Controle.enfonceClavier(ControlesEnum.TEXT))
        {
            Vector2 posTxt = _position - Camera.getInstance().getPosition() - _ecranJeu._font.MeasureString(_hp + "") / 2;
            posTxt.Y -= _rayon * 2f;
            spriteBatch.DrawString(_ecranJeu._font, _hp + "", posTxt, Color.White);
        }
        base.Draw(spriteBatch);
    }

    /// <summary>
    /// Evite les collisions avec tout les monstres
    /// </summary>
    /// <param name="deltaT">difference entre la derniere update</param>
    protected virtual void eviterCollisions(float deltaT)
    {
        List<Monstre> monstres = _ecranJeu.GetMonstres();
        monstres.Remove(this);
        monstres.ForEach(m => eviterCollisions(m, deltaT));
    }

    /// <summary>
    /// Evite les collisions entre deux monstres
    /// </summary>
    /// <param name="monstre">monstre a evite</param>
    /// <param name="deltaT">difference entre la derniere update</param>
    protected virtual void eviterCollisions(Monstre monstre, float deltaT)
    {
        float minDistance = 1.7f * _forme[0].Length();
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
    /// Enleve le nombre de degat au monstre
    /// Si sa vie est sous zero, le tue
    /// </summary>
    /// <param name="degat">Nombre de degat subit</param>
    /// <returns>true si le monstre est mort</returns>
    public virtual bool RecevoirDegat(float degat)
    {
        _hp -= degat;
        if (_hp <= 0)
        {
            Mourrir();
            return true;
        }
        return false;
    }

    public virtual void Mourrir()
    {
        _ecranJeu.EnleverObjet(this);
        _ecranJeu._score._ennemisEnleve += 1;
        new Experience(this._position, (int)_exp, _ecranJeu);
    }
    protected Vector2 recul = new Vector2(0, 0);
    public void AppliquerRecul(float deltaT)
    {
        Vector2 aplique = recul * deltaT * 0.92f;
        this._position += aplique;
        this.recul -= aplique;
    }
    public void AjouterRecul(Vector2 source, float force)
    {
        Vector2 dir = _position - source;
        dir.Normalize();
        recul += dir * force;
    }

    /// <summary>
    /// Bouge et fait tourner l'ennemi selon sa vitesse et sa vitesse de rotation
    /// </summary>
    /// <param name="posJoueur">posiition du joueur</param>
    /// <param name="deltaT">difference de temps</param>
    public virtual void bouger(Vector2 posJoueur, float deltaT)
    {
        if(_type == "bossGunner" || _type == "gunner" && (posJoueur - _position).Length() > 400)
        {
            return;
        }
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
        Vector2 posJ = _ecranJeu._joueur.getPosition();
        float rayJ = _ecranJeu._joueur.getRayon();
        float dist = (posJ - _position).Length();
        return rayJ + _rayon > dist;
    }

    public float getDmg()
    {
        return _dmg;
    }

    public static List<ProjectileEnnemi> getProjectiles()
    {
        return _projectiles;
    }
    public static void EnleverProjectile(ProjectileEnnemi projectile)
    {
        _projectiles.Remove(projectile);
    }
    public static void AjouterProjectile(ProjectileEnnemi projectile)
    {
        _projectiles.Add(projectile);
    }

    public static void EnleverBoss(Monstre boss)
    {
        _bosses.Remove(boss);
    }
    public static void AjouterBoss(Monstre boss)
    {
        _bosses.Add(boss);
    }
    public static List<Monstre> GetBosses()
    {
        return _bosses;
    }
    public static Monstre GetMonstre(string type)
    {
        foreach (Monstre boss in _bosses)
        {
            if (boss._type == type)
            {
                return boss;
            }
        }
        return null;
    }
    public static bool CheckSiBoss(string type)
    {
        foreach (Monstre boss in _bosses)
        {
            if (boss._type == type)
            {
                return true;
            }
        }
        return false;
    }
}
