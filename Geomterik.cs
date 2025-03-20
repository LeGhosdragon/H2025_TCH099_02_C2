using System;
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
    GraphicsDeviceManager _graphics;
    Joueur _joueur;

    public Geometrik()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        this.Window.AllowUserResizing = true;
        this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _objets = new List<IGameObject>();
        _joueur = new Joueur(PolyGen.GetPoly(3, 100), _graphics.GraphicsDevice);
        _objets.Add(_joueur);
        float x = _graphics.GraphicsDevice.Viewport.Width / 2;
        float y = _graphics.GraphicsDevice.Viewport.Height / 2;
        _joueur.setPosition(new Vector3(x, y, 0));
    }

    protected override void Update(GameTime gameTime)
    {
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        foreach (IGameObject obj in _objets)
        {
            obj.Update(deltaT);
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _graphics.GraphicsDevice.Clear(Color.Black);
        
        foreach (IGameObject obj in _objets)
        {
            obj.Draw(_graphics.GraphicsDevice);
        }

        base.Draw(gameTime);
    }

    void Window_ClientSizeChanged(object sender, EventArgs e)
    {
        float x = _graphics.GraphicsDevice.Viewport.Width / 2;
        float y = _graphics.GraphicsDevice.Viewport.Height / 2;
        _joueur.setPosition(new Vector3(x, y, 0));
    }
}
