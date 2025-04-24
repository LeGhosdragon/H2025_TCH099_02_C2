using System.Diagnostics;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

/// <summary>
/// Objet qui peut etre utilisé dans les pages
/// </summary>
public abstract class AbstractGameObject : IGameObject
{
    protected Vector2[] _forme;
    protected Vector2 _position;
    protected int _zIndex;

    /// <summary>
    /// Fonction par défaut pour générer un objet
    /// </summary>
    /// <param name="forme">tableau ou chaque point représente un sommet de l'objet</param>
    /// <param name="position">position sur l'ecran</param>
    /// <param name="zIndex">determine l'ordre avec lequel ils sera affiche</param>
    public AbstractGameObject(Vector2[] forme, Vector2 position, int zIndex)
    {
        this._forme = forme;
        this._position = position;
        this._zIndex = zIndex;
    }

    /// <summary>
    /// Affiche l'objet
    /// </summary>
    /// <param name="spritebatch">appareil graphique utilisé</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Peintre.dessinerForme(spriteBatch, _forme, _position,3);
    }

    /// <summary>
    /// Rafraichis l'objet
    /// </summary>
    /// <param name="deltaT">Temps entre le dernier rafraichissement</param>
    public virtual void Update(float deltaT)
    {
        _forme = PolyGen.tournerMatrice(_forme, deltaT * 0.2f);
    }

    /// <summary>
    /// Donne la position de l'objet
    /// </summary>
    /// <returns>Retourne la position de l'objet</returns>
    public Vector2 getPosition()
    {
        return _position;
    }

    /// <summary>
    /// Modifie la position de l'objet
    /// </summary>
    /// <param name="position">nouvelle position de l'objet</param>
    public void setPosition(Vector2 position)
    {
        _position = position;
    }
}
