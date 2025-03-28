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

    Fond fond = new Fond();
    public override void LoadContent()
    {
        base.LoadContent();
    }
    public override void Initialize()
    {
        UserInterface.Initialize(Content,BuiltinThemes.editor);


        //Panneau du centre
        Panel centre = new Panel(new Vector2(600,800));
        UserInterface.Active.AddEntity(centre);

        //Image du Logo


        base.Initialize();
    }

    public override void Draw(GameTime gameTime)
    {   
        Game.GraphicsDevice.Clear(Color.Black);
        Game.GetSpriteBatch().Begin();
        fond.Draw(Game.GetSpriteBatch());
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

        UserInterface.Active.Update(gameTime);
        fond.Update(deltaT);
    }
}