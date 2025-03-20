using System.Collections.Generic;
using desktop.gameobjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace desktop.pages;

/// <summary>
/// Classe d'une page par défaut
/// </summary>
public abstract class AbstractPageObjet : IPage
{
    protected List<IGameObject> _objets;
    protected GraphicsDeviceManager _graphics;

    public AbstractPageObjet(GraphicsDeviceManager graphics)
    {
        this._graphics = graphics;
    }

    /// <summary>
    /// Dessine chaque objet
    /// </summary>
    /// <param name="gameTime">temps avant le dernier rafraichissement</param>
    public virtual void Draw(GameTime gameTime)
    {
        _graphics.GraphicsDevice.Clear(Color.Black);

        foreach (IGameObject obj in _objets)
        {
            obj.Draw(_graphics.GraphicsDevice);
        }
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
