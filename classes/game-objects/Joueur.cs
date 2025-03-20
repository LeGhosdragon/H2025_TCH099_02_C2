using System.Diagnostics;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace desktop.gameobjects;

public class Joueur : AbstractGameObject
{
    float vitesse = 100f;

    public Joueur(Vector2[] forme, GraphicsDevice device)
        : base(forme, device) { }

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

        _position.Y += yMov * deltaT * vitesse;
        _position.X += xMov * deltaT * vitesse;
    }
}
