using desktop.gameobjects;
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
    protected Chrono _delai;

    /// <summary>
    /// Determine si une nouvelle attaque doit etre fait des que le delai est ecoule
    /// </summary>
    protected bool _attaqueAutomatique;

    /// <summary>
    /// Forme de base de l'arme (pas necessairement celle qui est affiche)
    /// </summary>
    protected Vector2[] _formeBase;

    /// <summary>
    /// Permet de bien placcer l'arme autour du joueur
    /// </summary>
    protected float _longueur;

    protected AbstractArme(
        Vector2[] formeBase,
        Vector2 position,
        Joueur joueur,
        float delai,
        float longueur
    )
        : base(formeBase, position, 1)
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
        //Place l'arme dans la direction ou la souris pointe
        Vector2 dir = Camera.getInstance().getPosSourisCamera();
        dir.Normalize();
        _position = _joueur.getPosition() + dir * (_joueur.getRayon() + _longueur / 2);

        //Doit gerer la rotaion de larme
        //   _forme = PolyGen.tournerMatrice(_formeBase, (float)(MathPlus.AngleEntre(dir, new Vector2(1, 0)) + Math.PI));
    }

    public float GetLongueur()
    {
        return _longueur;
    }

    public abstract void utiliser();

    /// <summary>
    /// Permet de savoir si l'arme attaque automatiquement
    /// </summary>
    /// <returns>True si _attaqueAutomatique est active</returns>
    public bool GetAttaqueAutomatique()
    {
        return _attaqueAutomatique;
    }

    /// <summary>
    /// Permet d'obtenir la forme de l'arme avant des modification
    /// </summary>
    /// <returns>formeBase de l'arme</returns>
    public Vector2[] getFormeBase()
    {
        return _formeBase;
    }
}
