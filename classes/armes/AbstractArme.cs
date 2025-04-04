using System.Collections.Generic;
using desktop.gameobjects;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace desktop.armes;

public abstract class AbstractArme : AbstractGameObject, IArme
{
    protected Joueur _joueur;

    /// <summary>
    /// Delai entre chaque attaque
    /// </summary>
    public Chrono _delai {get;}

    /// <summary>
    /// Determine si une nouvelle attaque doit etre fait des que le delai est ecoule
    /// </summary>
    public bool _attaqueAutomatique {get;set;}

    /// <summary>
    /// Forme de base de l'arme (pas necessairement celle qui est affiche)
    /// </summary>
    public Vector2[] _formeBase {get;}

    /// <summary>
    /// Permet de bien placcer l'arme autour du joueur
    /// </summary>
    public float _longueur {get;}

    protected EcranJeu _ecran;
    protected List<Touche> _touches = new List<Touche>();

    protected AbstractArme(
        Vector2[] formeBase,
        Joueur joueur,
        float delai,
        float longueur,
        EcranJeu ecran
    )
        : base(formeBase, joueur.getPosition(), 1)
    {
        this._formeBase = formeBase;
        this._joueur = joueur;
        this._delai = new Chrono(delai, true);
        this._attaqueAutomatique = true;
        this._longueur = longueur;
        this._ecran = ecran;
    }

    public override void Update(float deltaT)
    {
        Touche.enleverTouches(_touches);
        
        if (Touche.ValiderTouche(_touches,ControlesEnum.ATTAQUE_AUTOMATIQUE) && Controle.enfonceClavier(ControlesEnum.ATTAQUE_AUTOMATIQUE))
        {
            _touches.Add(new Touche(ControlesEnum.ATTAQUE_AUTOMATIQUE));
            _attaqueAutomatique = !_attaqueAutomatique;
        }
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
        //Place l'arme dans la direction ou la souris pointe
        Vector2 dir = Camera.getInstance().getPosSourisCamera();
        dir.Normalize();
        _position = _joueur.getPosition() + dir * (_joueur.getRayon() + _longueur / 2);
        _forme = PolyGen.tournerMatrice(_formeBase, PolyGen.angleVecteur(dir));
    }



    public abstract void utiliser();

}
