using System.Collections.Generic;
using System.Linq;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class PageJeu : GameScreen
{

    protected Joueur _joueur;
    protected Chrono _chronoMonstres;
    protected List<IGameObject> _objets;

    private new Geometrik Game => (Geometrik) base.Game;

    public PageJeu(Game game) : base(game)
    {
        _objets = new List<IGameObject>();
        _joueur = new Joueur(PolyGen.GetPoly(100, 100), new Vector3(0, 0, 0));
        _objets.Add(_joueur);

        _chronoMonstres = new Chrono(100);
    }
    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;

    }


    public List<Monstre> GetMonstres()
    {
        return _objets.OfType<Monstre>().ToList<Monstre>();
    }
    public Joueur getJoueur()
    {
        return _joueur;
    }

}