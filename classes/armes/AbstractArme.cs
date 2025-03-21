using System;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace desktop.armes;

public abstract class AbstractArme : AbstractGameObject, IArme
{
    protected Joueur _joueur;
    protected Chrono _delai;
    private bool _attaqueAutomatique;

    protected AbstractArme(
        Vector2[] forme,
        Microsoft.Xna.Framework.Vector3 position,
        Joueur joueur,
        float delai
    )
        : base(forme, position)
    {
        this._joueur = joueur;
        this._delai = new Chrono(delai, true);
        this._attaqueAutomatique = true;
    }

    public override void Update(float deltaT)
    {
        if (
            _delai.Update(deltaT)
            && (_attaqueAutomatique || Mouse.GetState().LeftButton == ButtonState.Pressed)
        )
        {
            utiliser();
            _delai.reinitialiser();
        }
        UpdatePos(deltaT);
    }

    protected virtual void UpdatePos(float deltaT)
    {
        Controle.getPosSourisRel(MathPlus.EnVector2(_joueur.getPosition()));
    }

    public abstract void utiliser();
}
