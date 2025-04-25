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


            ReponseObtenirPalmares reponse = await LocalAPI.ObtenirPalmares();
            if(reponse.Erreur != null){
                titreChargement.Text = reponse.Erreur;
                return;
            }
            if(!reponse.Reussite){
                titreChargement.Text = "Erreur lors de l'obtention du palmares";
                return;
            }

            
            GenererScores(reponse.Palmares.ToList(), titreChargement);

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
        _palmares.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
        _palmares.Size = new Vector2(0.9f, 0.8f);

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
    private void GenererScores(List<Palmares> scores,Entity chargement)
    {
        _palmares.RemoveChild(chargement);
        
        Comparer<Palmares> comparer = Comparer<Palmares>.Create((p1, p2) =>
        {
            return p1.Score.CompareTo(p2.Score);
        });
        

        for (int i = 0; i < scores.Count ;i++)
        {
            Panel panelTout = new Panel();
            panelTout.Skin = PanelSkin.ListBackground;
            panelTout.Size = new Vector2(0.9f,0.3f);
            panelTout.Anchor = Anchor.AutoCenter;
            _palmares.AddChild(panelTout);
            Header header = new Header(scores[i].Nom_Utilisateur + " #" + (i+1));

            string lHautDebut = "Score: " + scores[i].Score+"         ";
            string lhautFin = "Temps: " + scores[i].Temps_Partie;

            string lBasDebut = "Experience " + scores[i].Experience;
            string lBasFin = "Ennemis Tuées " + scores[i].Ennemis_Enleve;

            for(int j = lBasDebut.Length;j < lHautDebut.Length;j++){
                lBasDebut += " ";
            }


            Paragraph ligne = new Paragraph(lHautDebut +lhautFin + "\n" + lBasDebut + lBasFin);
            panelTout.AddChild(header);
            panelTout.AddChild(ligne);

       /*     Panel bPlace = new Panel();
            bPlace.Skin = PanelSkin.None;
            bPlace.Anchor = Anchor.Auto;
            panelTout.AddChild(bPlace);

            Paragraph pPlace = new Paragraph(i+"");
            pPlace.Anchor  = Anchor.Auto;
            bPlace.Size = new Vector2(0.99f,0.5f);
            bPlace.AddChild(pPlace);         

            Panel bScore = new Panel();
            bScore.Skin = PanelSkin.None;
            bScore.Size = new Vector2(0.99f,0.5f);

            bScore.Anchor = Anchor.AutoInline;
            panelTout.AddChild(bScore);

            Paragraph pScore = new Paragraph(scores[i].Score+"");
            pScore.Anchor = Anchor.Auto;
            bScore.AddChild(pScore);   
 */
        }
        
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
