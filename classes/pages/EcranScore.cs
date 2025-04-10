using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using desktop.gameobjects;
using desktop.utils;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranScore : GameScreen
{
    Fond fond = new Fond();
    Panel _palmares;
    private new Geometrik Game => (Geometrik)base.Game;


    public EcranScore(Game game) : base(game)
    { }
    public override void Initialize()
    {
        UserInterface.Active.UseRenderTarget = true;
        GenererUI();

        Thread t1 = new Thread(async () =>
        {
            Header titreChargement = new Header("Chargement en cours");
            _palmares.AddChild(titreChargement);


            List<Palmares> scores = await obtenirPalmares();
            GenererScores(scores, titreChargement);

        });
        t1.Start();
        base.Initialize();
    }
    private void GenererUI()
    {
        Panel centre = new Panel();
        centre.Size = new Vector2(0.9f, 0.9f);
        UserInterface.Active.AddEntity(centre);


        Header titre = new Header("Palmarès");
        titre.Size = new Vector2(0.4f, 0.1f);
        centre.AddChild(titre);
        titre.Anchor = Anchor.AutoCenter;

        _palmares = new Panel();
        _palmares.Size = new Vector2(0.9f, 0.8f);
        _palmares.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
        centre.AddChild(_palmares);
        _palmares.Anchor = Anchor.AutoCenter;


        Button btnHome = new Button("Retour");
        btnHome.OnClick = (Entity e) =>
        {
            UnloadContent();
            Game.LoadEcranAcceuil();
        };
        btnHome.Size = new Vector2(0.5f, 0.1f);
        btnHome.Anchor = Anchor.AutoCenter;
        centre.AddChild(btnHome);

    }
    private void GenererScores(List<Palmares> scores, Entity chargement)
    {
        _palmares.RemoveChild(chargement);
        
        Comparer<Palmares> comparer = Comparer<Palmares>.Create((p1, p2) =>
        {
            return p1.Score.CompareTo(p2.Score);
        });
        

        foreach (Palmares score in scores)
        {
            Panel panel = new Panel();
            panel.Size = new Vector2(0.9f,0.2f);
            panel.Anchor = Anchor.AutoCenter;
            _palmares.AddChild(panel);
        }
        foreach (Palmares score in scores)
        {
            Panel panel = new Panel();
            panel.Size = new Vector2(0.9f,0.2f);
            panel.Anchor = Anchor.AutoCenter;
            _palmares.AddChild(panel);
        }
                foreach (Palmares score in scores)
        {
            Panel panel = new Panel();
            panel.Size = new Vector2(0.9f,0.2f);
            panel.Anchor = Anchor.AutoCenter;
            _palmares.AddChild(panel);
        }
                foreach (Palmares score in scores)
        {
            Panel panel = new Panel();
            panel.Size = new Vector2(0.9f,0.2f);
            panel.Anchor = Anchor.AutoCenter;
            _palmares.AddChild(panel);
        }
    }
    public async Task<List<Palmares>> obtenirPalmares()
    {
        Palmares[] liste = await LocalAPI.ObtenirPalmares();
        return liste.ToList<Palmares>();
    }


    public override void Draw(GameTime gameTime)
    {
        UserInterface.Active.Draw(Game.GetSpriteBatch());

        Game.GraphicsDevice.Clear(Color.Black);
        Game.GetSpriteBatch().Begin();
        fond.Draw(Game.GetSpriteBatch());
        Game.GetSpriteBatch().End();

        UserInterface.Active.DrawMainRenderTarget(Game.GetSpriteBatch());
    }

    public override void Update(GameTime gameTime)
    {
        fond.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        UserInterface.Active.Update(gameTime);
    }
    public override void UnloadContent()
    {
        UserInterface.Active.Clear();
        UserInterface.Active.UseRenderTarget = false;

        base.UnloadContent();

    }
}
