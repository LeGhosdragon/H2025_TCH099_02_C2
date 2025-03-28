
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class PageAcceuil : GameScreen
{
    private new Geometrik Game => (Geometrik) base.Game;

    public PageAcceuil(Game game) : base(game) { }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(Color.Beige);
    }

    public override void Update(GameTime gameTime)
    {

    }
}