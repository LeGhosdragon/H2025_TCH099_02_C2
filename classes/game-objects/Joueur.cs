using System;
using System.Diagnostics;
using desktop.armes;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace desktop.gameobjects;

public class Joueur : IGameObject
{
    protected readonly float DELAI_INVICIBILITE = 2; //Delai durant lequel le joueur est invincible en secondes
    protected Chrono _chronoInvincibilite;

    //Attributs selon ameliorations
    public float _vitesse { get; set; } = 100f; //UpgVitMouement 100, +20
    public float _rayonCollection { get; set; } = 100; //UpgRayonAttraction 100,* 1.3
    public float _hpBase { get; set; } = 200; //Pas affecte
    public float _hp { get; set; } //UpgVieJoueur _hpBase, + (0.2 * _hpBase)

    public IArme _arme { get; set; }
    float _rayon = 40;
    int _experience = 0;
    protected int _niveau { get; set; } = 1;
    protected EcranJeu _ecranJeu;
    protected Vector2 _position;

    public Joueur(Vector2 position, EcranJeu ecranJeu)
    {
        this._position = position;
        this._ecranJeu = ecranJeu;
        this._hp = _hpBase;
        _chronoInvincibilite = new Chrono(DELAI_INVICIBILITE, true);
    }
    /// <summary>
    /// Ajoute de l'experience au joueur
    /// </summary>
    /// <param name="quantitee">Quantitee d'experience ajoutee</param>
    public void ajouterExperience(int quantitee)
    {
        _experience += quantitee;
        _ecranJeu._score._experience += quantitee;
    }
    /// <summary>
    /// 
    /// </summary>
    public void augmenterNiveau()
    {
        _experience -= getExpReq();
        _niveau++;
        _ecranJeu.augmenterNiveau(this);
    }
    /// <summary>
    /// Permet d'obtenir le nombre d'experience requis pour atteindre le prochain niveau
    /// </summary>
    /// <returns>nombre d'experience requis pour le prochain niveau</returns>
    public int getExpReq(int niveau)
    {
        return 7 + (int)Math.Round(Math.Pow(niveau, 1.9));
    }

    /// <summary>
    /// Permet d'obtenir le nombre d'experience requis pour atteindre le prochain niveau
    /// </summary>
    /// <returns>nombre d'experience requis pour le prochain niveau</returns>
    public int getExpReq()
    {
        return getExpReq(_niveau);
    }
    /// <summary>
    /// Rayon du joueur
    /// </summary>
    /// <returns>rayon entre le centre et l'extremitee du joueur</returns>
    public float getRayon()
    {
        return _rayon;
    }
    public Vector2 getPosition()
    {
        return _position;
    }
    public void Update(float deltaT)
    {
        //Deplace le joueur
        int xMov = 0;
        int yMov = 0;
        if (Controle.enfonceClavier(ControlesEnum.BOUGER_HAUT))
        {
            yMov -= 1;
        }
        if (Controle.enfonceClavier(ControlesEnum.BOUGER_BAS))
        {
            yMov += 1;
        }
        if (Controle.enfonceClavier(ControlesEnum.BOUGER_GAUCHE))
        {
            xMov -= 1;
        }
        if (Controle.enfonceClavier(ControlesEnum.BOUGER_DROIT))
        {
            xMov += 1;
        }
       Vector2 mov = new Vector2(xMov, yMov);

        if(xMov != 0 || yMov != 0){
            mov.Normalize();

            _position += mov * deltaT * _vitesse;
        }

        if (_experience > getExpReq())
        {
            augmenterNiveau();
        }
        _chronoInvincibilite.Update(deltaT);
    }
    public void collision(float degat)
    {
        if (_chronoInvincibilite.Update(0))
        {
            _hp -= degat;
            _chronoInvincibilite.reinitialiser();

            if (_hp <= 0)
            {
                Console.Write(_hp);
                Mourrir();
            }
        }

    }
    public void Mourrir()
    {
        _ecranJeu.FinPartie();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(_position - Camera.getInstance().getPosition(), _rayon, 40, Color.White);
        Vector2 posTxt = _position - Camera.getInstance().getPosition() - _ecranJeu._font.MeasureString(_hp + "") / 2;
        spriteBatch.DrawString(_ecranJeu._font, _hp + "", posTxt, Color.White);

    }
}
