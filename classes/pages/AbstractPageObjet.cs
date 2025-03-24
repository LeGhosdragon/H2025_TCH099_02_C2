using System.Collections.Generic;
using desktop.gameobjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace desktop.pages;

/// <summary>
/// Classe d'une page par défaut
/// </summary>
public abstract class AbstractPageObjet : IPage
{
    protected List<IGameObject> _objets;
    protected GraphicsDeviceManager _graphics;
    SpriteBatch _spriteBatch;

    public AbstractPageObjet(GraphicsDeviceManager graphics)
    {
        this._graphics = graphics;
        this._spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
    }

    /// <summary>
    /// Dessine chaque objet
    /// </summary>
    /// <param name="gameTime">temps avant le dernier rafraichissement</param>
    public virtual void Draw(GameTime gameTime)
    {
        _graphics.GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        foreach (IGameObject obj in _objets)
        {
            obj.Draw(_spriteBatch);
        }
        _spriteBatch.End();
    }

    /// <summary>
    /// Rafraichis chaque objet
    /// </summary>
    /// <param name="gameTime">temps entre les deux images</param>
    public virtual void Update(GameTime gameTime)
    {
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (IGameObject obj in _objets)
        {
            obj.Update(deltaT);
        }
    }
}
