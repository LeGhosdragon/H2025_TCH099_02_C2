using System.Collections.Generic;
using System.Linq;
using desktop.armes;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;

namespace desktop.pages;

public class PageJeu : AbstractPageObjet
{
    protected Joueur _joueur;
    private Chrono _chronoMonstres;

    public PageJeu(GraphicsDeviceManager graphics)
        : base(graphics)
    {
        //Genere le joueur
        _objets = new List<IGameObject>();
        _joueur = new Joueur(PolyGen.GetPoly(100, 100), new Vector2(0, 0));
        _objets.Add(_joueur);

        //Assigne une arme au joueur
        AbstractArme arme = new Epee(_joueur);
        _joueur.setArme(arme);
        _objets.Add(arme);

        //Initialise la camera et le timer pour les monstres
        new Camera(_graphics.GraphicsDevice, _joueur.getPosition());
        _chronoMonstres = new Chrono(1f);
        genererMonstres();
    }

    public override void Update(GameTime gameTime)
    {
        //Genere un monstre a periodiquement
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_chronoMonstres.Update(deltaT))
        {
            //genererMonstres();
        }

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        //Centre la camera avec le joueur
        Camera.setPosition(_joueur.getPosition());
        base.Draw(gameTime);
    }

    /// <summary>
    /// Genere un monstre
    /// </summary>
    private void genererMonstres()
    {
        Monstre monstre = new Monstre(PolyGen.GetPoly(3, 10), new Vector2(100, 100), this, 10);
        _objets.Add(monstre);
    }

    /// <summary>
    /// Retourne la liste de monstres
    /// </summary>
    /// <returns>Liste contenant tout les monstres de la page</returns>
    public List<Monstre> GetMonstres()
    {
        return _objets.OfType<Monstre>().ToList<Monstre>();
    }

    /// <summary>
    /// Permet d'obtenir le joueur
    /// </summary>
    /// <returns>Le joueur </returns>
    public Joueur getJoueur()
    {
        return _joueur;
    }
}
