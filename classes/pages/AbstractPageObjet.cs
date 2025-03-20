using System.Collections.Generic;
using desktop.gameobjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace desktop.pages;

public abstract class AbstractPageObjet : IPage
{
    protected List<IGameObject> _objets;
    protected GraphicsDeviceManager _graphics;

    public AbstractPageObjet(GraphicsDeviceManager graphics)
    {
        this._graphics = graphics;
    }

    public void Draw(GameTime gameTime)
    {
        _graphics.GraphicsDevice.Clear(Color.Black);

        foreach (IGameObject obj in _objets)
        {
            obj.Draw(_graphics.GraphicsDevice);
        }
    }

    public void Update(GameTime gameTime)
    {
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (IGameObject obj in _objets)
        {
            obj.Update(deltaT);
        }
    }
}
