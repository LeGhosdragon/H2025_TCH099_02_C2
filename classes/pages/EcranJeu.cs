
using System.Collections.Generic;
using System.Linq;
using desktop.armes;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranJeu : GameScreen
{
    private new Geometrik Game => (Geometrik)base.Game;
    protected Joueur _joueur;
    protected List<IGameObject> _objets;
    protected Chrono _chronoMonstre;

    public EcranJeu(Game game) : base(game)
    {

        _joueur = new Joueur(PolyGen.GetPoly(100, 100), new Vector2(0, 0));
        _joueur.setArme(new Epee(_joueur, this));

    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(Color.Black);
    }

    public override void Update(GameTime gameTime)
    {

    }

    public List<Monstre> GetMonstres()
    {
        return _objets.OfType<Monstre>().ToList<Monstre>();
    }
    public Joueur GetJoueur(){
        return _joueur;
    }
}