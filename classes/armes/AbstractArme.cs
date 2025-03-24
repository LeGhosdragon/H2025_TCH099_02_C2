using System;
using System.Diagnostics;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace desktop.armes;

public abstract class AbstractArme : AbstractGameObject, IArme
{
    protected Joueur _joueur;
    protected Chrono _delai;
    protected bool _attaqueAutomatique;
    protected Vector2[] _formeBase;
    protected float _longueur;

    protected AbstractArme(
        Vector2[] formeBase,
        Vector3 position,
        Joueur joueur,
        float delai,
        float longueur
    )
        : base(formeBase, position)
    {
        this._formeBase = formeBase;
        this._joueur = joueur;
        this._delai = new Chrono(delai, true);
        this._attaqueAutomatique = true;
        this._longueur = longueur;
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
        Vector2 dir = Camera.getInstance().getPosSourisCamera();
        //Console.WriteLine(Camera.getInstance().getPosSourisCamera());

        dir.Normalize();
        _position =
            _joueur.getPosition()
            + MathPlus.EnVector3(dir * (_joueur.getRayon() + _longueur / 2), _position.Z);
        //   _forme = PolyGen.tournerMatrice(_formeBase, (float)(MathPlus.AngleEntre(dir, new Vector2(1, 0)) + Math.PI));
    }

    public abstract void utiliser();
}
