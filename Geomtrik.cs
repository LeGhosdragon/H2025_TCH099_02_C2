using System;
using System.Collections.Generic;
using desktop.gameobjects;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace desktop;

public class Geometrik : Game
{
    public PageJeu _pageJeu;
    private GraphicsDeviceManager _graphics;

    public Geometrik()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        //Ajoute les evenements lorsque la page change de dimension
        this.Window.AllowUserResizing = true;
        this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
    }

    protected override void Initialize()
    {

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _pageJeu = new PageJeu(_graphics);
    }

    protected override void Update(GameTime gameTime)
    {
        _pageJeu.Update(gameTime);

        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _pageJeu.Draw(gameTime);

        base.Draw(gameTime);
    }


    void Window_ClientSizeChanged(object sender, EventArgs e)
    {

    }
}
