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
    protected Vector3 _position;
    protected BasicEffect _effet;
    protected EffectTechnique _tecEffets;
    protected EffectPassCollection _passes;

    /// <summary>
    /// Fonction par défaut pour générer un objet
    /// </summary>
    /// <param name="forme">tableau ou chaque point représente un sommet de l'objet</param>
    /// <param name="graphics">appareil graphique utilisé</param>
    public AbstractGameObject(Vector2[] forme, GraphicsDevice graphics)
    {
        this._forme = forme;
        resEffet(graphics);
    }

    /// <summary>
    /// Affiche l'objet
    /// </summary>
    /// <param name="device">appareil graphique utilisé</param>
    public void Draw(GraphicsDevice device)
    {
        VertexPositionColor[] formeVis = PolyGen.GenererFormeVide(_forme, _position, Color.White);
        foreach (EffectPass pass in _passes)
        {
            pass.Apply();
            device.DrawUserPrimitives(PrimitiveType.LineStrip, formeVis, 0, formeVis.Length - 1);
        }
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
    public Vector3 getPosition()
    {
        return _position;
    }

    /// <summary>
    /// Modifie la position de l'objet
    /// </summary>
    /// <param name="position">nouvelle position de l'objet</param>
    public void setPosition(Vector3 position)
    {
        _position = position;
    }

    /// <summary>
    /// Rafraichis les effets de laffichage
    /// </summary>
    /// <param name="device">appareil graphique utilisé</param>
    public void resEffet(GraphicsDevice device)
    {
        _effet = new BasicEffect(device);
        _effet.World = Matrix.CreateOrthographicOffCenter(
            0,
            device.Viewport.Width,
            device.Viewport.Height,
            0,
            0,
            1
        );

        _tecEffets = _effet.Techniques[0];
        _passes = _tecEffets.Passes;
    }
}
