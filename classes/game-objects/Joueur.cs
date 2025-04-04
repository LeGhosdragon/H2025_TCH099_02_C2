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
     
    private IArme _arme;
    public float _vitesse {get;set;}= 100f;
    float _rayon = 40;
    int _experience = 0;
    protected int _niveau {get;set;} = 1;
    protected EcranJeu _ecranJeu;
    protected Vector2 _position;

    public Joueur(Vector2 position,EcranJeu ecranJeu)
    {
        this._ecranJeu = ecranJeu;
    }
    /// <summary>
    /// Ajoute de l'experience au joueur
    /// </summary>
    /// <param name="quantitee">Quantitee d'experience ajoutee</param>
    public void ajouterExperience(int quantitee)
    {
        _experience += quantitee;
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
    /// Choisis l'arme qui est utilisé
    /// </summary>
    /// <param name="arme"></param>
    public void setArme(IArme arme)
    {
        this._arme = arme;
    }
    /// <summary>
    /// Rayon du joueur
    /// </summary>
    /// <returns>rayon entre le centre et l'extremitee du joueur</returns>
    public float getRayon()
    {
        return _rayon;
    }
    public Vector2 getPosition(){
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
        _position.Y += yMov * deltaT * _vitesse;
        _position.X += xMov * deltaT * _vitesse;

        if (_experience > getExpReq())
        {
            augmenterNiveau();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(_position - Camera.getInstance().getPosition(),_rayon,40,Color.White);
    }
}
