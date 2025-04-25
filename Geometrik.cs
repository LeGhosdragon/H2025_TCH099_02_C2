using System;
using System.Collections.Generic;
using desktop.armes;
using desktop.gameobjects;
using desktop.pages;
using desktop.utils;
using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        _graphics.SynchronizeWithVerticalRetrace = false;
        //Ajoute les evenements lorsque la page change de dimension
        this.Window.AllowUserResizing = true;
        this.IsMouseVisible = false;
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
        MusiqueAPI.LoadContent(Content);
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
        MusiqueAPI.Jouer(MusiqueAPI.Musique.MENU);
    }
    public void LoadEcranJeu(TypesArmes typesArme){
        _screenManager.LoadScreen(new EcranJeu(this,typesArme));
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
