using System;
using desktop.gameobjects;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranAcceuil : GameScreen
{
    private new Geometrik Game => (Geometrik) base.Game;

    public EcranAcceuil(Game game) : base(game) { }

    Fond _fond = new Fond();
    Panel _centre;
    public override void LoadContent()
    {
        base.LoadContent();
    }
    public override void Initialize()
    {


        //Panneau du centre
        _centre = new Panel(new Vector2(600,800));
        UserInterface.Active.AddEntity(_centre);

        //Ajout du bouton Jouer
        Button btnJouer = new Button("Jouer");
        btnJouer.OnClick = (Entity btn) =>{
            Game.LoadEcranJeu();
        };

        _centre.AddChild(btnJouer);

        Button btnInscription = new Button("Inscription");
        _centre.AddChild(btnInscription);
        btnInscription.OnClick = (Entity btn) =>{
            Game.LoadEcranInscription();
        };

        base.Initialize();
    }

    public override void Draw(GameTime gameTime)
    {   
        Game.GraphicsDevice.Clear(Color.Black);
        Game.GetSpriteBatch().Begin();
        _fond.Draw(Game.GetSpriteBatch());
        Game.GetSpriteBatch().End();

        UserInterface.Active.Draw(Game.GetSpriteBatch());
    }
    public float GetLargeurEcran(){
        return Game.GraphicsDevice.Viewport.Width;
    }
    public float GetHauteurEcran(){
        return Game.GraphicsDevice.Viewport.Height;
    }

    public override void Update(GameTime gameTime)
    {
        float deltaT = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _fond.Update(deltaT);
        UserInterface.Active.Update(gameTime);
    }
}