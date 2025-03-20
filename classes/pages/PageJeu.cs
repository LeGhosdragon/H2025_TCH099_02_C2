using System.Collections.Generic;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;

namespace desktop.pages;

public class PageJeu : AbstractPageObjet
{
    protected Joueur _joueur;

    public PageJeu(GraphicsDeviceManager graphics)
        : base(graphics)
    {
        _objets = new List<IGameObject>();
        _joueur = new Joueur(PolyGen.GetPoly(100, 100), _graphics.GraphicsDevice);
        _objets.Add(_joueur);
        float x = _graphics.GraphicsDevice.Viewport.Width / 2;
        float y = _graphics.GraphicsDevice.Viewport.Height / 2;
        _joueur.setPosition(new Vector3(x, y, 0));
    }
}
