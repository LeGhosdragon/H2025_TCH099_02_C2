using System.Collections.Generic;
using System.Linq;
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
        _joueur = new Joueur(PolyGen.GetPoly(100, 100), new Vector3(0, 0, 0));
        _objets.Add(_joueur);
        resEffet();
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
        Monstre monstre = new Monstre(PolyGen.GetPoly(3, 10), new Vector3(100, 100, 0), this);
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

    /*
    SECTION GRAPHIQUE
    */
    protected BasicEffect _effet;
    protected EffectTechnique _tecEffets;
    protected EffectPassCollection _passes;

    /// <summary>
    /// Rafraichis les effets de laffichage
    /// </summary>
    /// <param name="device">appareil graphique utilisé</param>
    public void resEffet()
    {
        _effet = new BasicEffect(_graphics.GraphicsDevice);
        _effet.World = Matrix.CreateOrthographicOffCenter(
            _joueur.getPosition().X - _graphics.GraphicsDevice.Viewport.Width / 2,
            _graphics.GraphicsDevice.Viewport.Width / 2 + _joueur.getPosition().X,
            _graphics.GraphicsDevice.Viewport.Height / 2 + _joueur.getPosition().Y,
            _joueur.getPosition().Y - _graphics.GraphicsDevice.Viewport.Height / 2,
            0,
            1
        );

        _tecEffets = _effet.Techniques[0];
        _passes = _tecEffets.Passes;
    }

    public override void Draw(GameTime gameTime)
    {
        resEffet();
        foreach (EffectPass pass in _passes)
        {
            pass.Apply();
        }
        base.Draw(gameTime);
    }
}
