using System;
using System.Threading;
using desktop.gameobjects;
using desktop.utils;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranInscription : GameScreen
{
    private new Geometrik Game => (Geometrik)base.Game;

    protected Fond _fond;
    protected Panel _centre;
    protected Paragraph erreurs;
    public EcranInscription(Game game) : base(game) { }


    public override void Initialize()
    {

        _fond = new Fond();
        //Panneau du centre
        _centre = new Panel();
        UserInterface.Active.AddEntity(_centre);

        //Entree pour l'identifiant
        TextInput textID = new TextInput(false);
        textID.PlaceholderText = "Nom d'utilisateur";
        _centre.AddChild(textID);


        //Entree pour l'identifiant
        TextInput textMDP = new TextInput(false);
        textMDP.PlaceholderText = "Mot de passe";
        textMDP.HideInputWithChar = '*';
        _centre.AddChild(textMDP);



        //Ajout du bouton Jouer
        Button btnValider = new Button("S'inscrire");
        btnValider.OnClick = (Entity btn) => 
        {

            Thread t1 = new Thread (async ()=>{
                ReponseInscription reponse = await LocalAPI.Inscription(textID.Value, textMDP.Value);
                if(!reponse.Reussite){
                    erreurs.FillColor = new Color(200,6,87);
                    erreurs.Text = LocalAPI.formatterErreurs(reponse.Erreurs);
                }else{
                    ReponseConnexion reponseConnexion = await LocalAPI.Connexion(textID.Value,textMDP.Value);
                    if(!reponseConnexion.Reussite){
                        Console.WriteLine(erreurs);
                    }

        
                    UnloadContent();
                    Game.LoadEcranAcceuil();
                }
            });
            t1.Start();

        };
        _centre.AddChild(btnValider);

        erreurs = new Paragraph();
         _centre.AddChild(erreurs);
        
        Button btnRetour = new Button("Retour");
        btnRetour.Anchor = Anchor.BottomCenter;
        btnRetour.OnClick = (Entity btn) =>{
            UnloadContent();
            Game.LoadEcranAcceuil();
        };
        _centre.AddChild(btnRetour);


        base.Initialize();
    }
    public override void Update(GameTime gameTime)
    {
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _fond.Update(deltaT);
        UserInterface.Active.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(Color.Black);
        Game.GetSpriteBatch().Begin();
        _fond.Draw(Game.GetSpriteBatch());
        Game.GetSpriteBatch().End();

        UserInterface.Active.Draw(Game.GetSpriteBatch());
    }
    public override void UnloadContent()
    {
        UserInterface.Active.Clear();
    }
}