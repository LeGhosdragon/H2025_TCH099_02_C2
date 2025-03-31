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
    public EcranInscription(Game game) : base(game) { }


    public override void Initialize()
    {

        _fond = new Fond();
        //Panneau du centre
        _centre = new Panel(new Vector2(600, 800));
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
            LocalAPI.Inscription(textID.Value, textMDP.Value);
        };

        _centre.AddChild(btnValider);



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
}