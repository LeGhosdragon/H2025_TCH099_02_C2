using System.Collections.Generic;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace desktop;

public class Geometrik : Game
{
    List<IGameObject> _objets;
    private SpriteBatch _spriteBatch;
    GraphicsDeviceManager _graphics;

    public Geometrik()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        this.Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _objets = new List<IGameObject>();
        _objets.Add(
            new Joueur(PolyGen.GetPolyVide(5, 100, 0, Color.White), _graphics.GraphicsDevice)
        );
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        foreach (IGameObject obj in _objets)
        {
            obj.Draw(_graphics.GraphicsDevice);
        }

        base.Draw(gameTime);
    }
}
