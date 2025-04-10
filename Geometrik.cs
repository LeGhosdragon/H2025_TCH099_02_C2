using System;
using System.Collections.Generic;
using desktop.gameobjects;
using desktop.pages;
using desktop.utils;
using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace desktop;

public class Geometrik : Game
{
    private GraphicsDeviceManager _graphics;
    private readonly ScreenManager _screenManager;
    private SpriteBatch _spriteBatch;

    public Geometrik()
    {
        _graphics = new GraphicsDeviceManager(this);

        _screenManager = new ScreenManager();
        Components.Add(_screenManager);

        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        //Ajoute les evenements lorsque la page change de dimension
        this.Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        UserInterface.Initialize(Content,BuiltinThemes.editor);

        _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

        _graphics.PreferredBackBufferWidth = 1000;
        _graphics.PreferredBackBufferHeight = 800;
        _graphics.ApplyChanges();


        LoadEcranAcceuil();
        base.Initialize();
    }

    protected override void LoadContent()
    {

    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        base.Draw(gameTime);
    }
    public SpriteBatch GetSpriteBatch(){
        return _spriteBatch;
    }

    /*
    CHARGER LES ECRANS
    */
    public void LoadEcranAcceuil(){
        _screenManager.LoadScreen(new EcranAcceuil(this));
    }
    public void LoadEcranJeu(){
        _screenManager.LoadScreen(new EcranJeu(this));
    }
    public void LoadEcranInscription(){
        _screenManager.LoadScreen(new EcranInscription(this));
    }
    public void LoadEcranConnexion(){
        _screenManager.LoadScreen(new EcranConnexion(this));
    }
    public void LoadEcranSelection(){
        _screenManager.LoadScreen(new EcranSelection(this));
    }
    public void LoadEcranScore(){
        _screenManager.LoadScreen(new EcranScore(this));
    }

    

}
