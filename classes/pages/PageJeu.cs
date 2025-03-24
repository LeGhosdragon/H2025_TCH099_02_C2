using System.Collections.Generic;
using System.Linq;
using desktop.armes;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.pages;

public class PageJeu : AbstractPageObjet
{
    protected Joueur _joueur;
    private Chrono _chronoMonstres;

    public PageJeu(GraphicsDeviceManager graphics)
        : base(graphics)
    {
        _objets = new List<IGameObject>();
        _joueur = new Joueur(PolyGen.GetPoly(100, 100), new Vector2(0, 0));
        _objets.Add(_joueur);

        AbstractArme arme = new Epee(_joueur);
        _joueur.setArme(arme);
        _objets.Add(arme);

        Camera.setPosition(_joueur.getPosition());
        Camera.getInstance().resEffet(graphics.GraphicsDevice);

        _chronoMonstres = new Chrono(1f);
    }

    public override void Update(GameTime gameTime)
    {
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_chronoMonstres.Update(deltaT))
        {
            genererMonstres();
        }

        base.Update(gameTime);
    }

    /// <summary>
    /// Genere un monstre
    /// </summary>
    private void genererMonstres()
    {
        Monstre monstre = new Monstre(PolyGen.GetPoly(3, 10), new Vector2(100, 100), this);
        _objets.Add(monstre);
    }

    public List<Monstre> GetMonstres()
    {
        return _objets.OfType<Monstre>().ToList<Monstre>();
    }

    public Joueur getJoueur()
    {
        return _joueur;
    }

    public override void Draw(GameTime gameTime)
    {
        Camera.setPosition(_joueur.getPosition());
        Camera.getInstance().Draw(_graphics.GraphicsDevice);

        base.Draw(gameTime);
    }
}
