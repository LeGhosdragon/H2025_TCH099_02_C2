using System.Diagnostics;
using desktop.armes;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace desktop.gameobjects;

public class Joueur : AbstractGameObject
{
    private IArme _arme;
    float _vitesse = 100f;
    float _rayon;

    public Joueur(Vector2[] forme, Vector3 position)
        : base(forme, position)
    {
        _rayon = forme[0].Length();
    }

    public void setArme(IArme arme)
    {
        this._arme = arme;
    }

    public float getRayon()
    {
        return _rayon;
    }

    public override void Update(float deltaT)
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
    }
}
