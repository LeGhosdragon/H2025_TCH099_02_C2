using System;
using desktop.gameobjects;
using desktop.utils;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranSelection : GameScreen
{
    private ChoixSelection _selectionEpee;
    private ChoixSelection _selectionFusil;
    private Chrono _delaiChargement;
    private new Geometrik Game => (Geometrik)base.Game;

    public EcranSelection(Game game) : base(game) { 

    }
    public override void LoadContent()
    {
        Texture2D _textDemoEpee = Content.Load<Texture2D>("custom/demo-epee");
        _selectionEpee = new ChoixSelection(2,0,GraphicsDevice,_textDemoEpee,800,800,38);
        
        Texture2D _textDemoFusil = Content.Load<Texture2D>("custom/demo-fusil");
        _selectionFusil = new ChoixSelection(2,1,GraphicsDevice,_textDemoFusil,423,442,47);

        _delaiChargement = new Chrono(1f,true);


        base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    {
        float deltaT = (float) gameTime.ElapsedGameTime.TotalSeconds;
        
        if(Controle.getPosSouris().X < GraphicsDevice.Viewport.Width/2){
            _selectionEpee._hovered = true;
            _selectionFusil._hovered = false;
            if(Mouse.GetState().LeftButton == ButtonState.Pressed){
                UnloadContent();
                Game.LoadEcranJeu(armes.TypesArmes.EPEE);
            }
        }else{
            _selectionFusil._hovered = true;
            _selectionEpee._hovered = false;
                        if(Mouse.GetState().LeftButton == ButtonState.Pressed){
                UnloadContent();
                Game.LoadEcranJeu(armes.TypesArmes.FUSIL);
            }
        }
        _selectionEpee.Update(gameTime);
        _selectionFusil.Update(gameTime);

    }

    public override void Draw(GameTime gameTime)
    {
                Game.GraphicsDevice.Clear(Color.Black);

        _selectionEpee.Draw(gameTime,Game.GetSpriteBatch());
        _selectionFusil.Draw(gameTime,Game.GetSpriteBatch());
    }
    public override void UnloadContent()
    {

        base.UnloadContent();
    }
}