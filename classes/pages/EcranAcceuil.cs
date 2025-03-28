using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranAcceuil : GameScreen
{
    private new Geometrik Game => (Geometrik) base.Game;

    public EcranAcceuil(Game game) : base(game) { }

    public override void LoadContent()
    {
        base.LoadContent();
    }
    public override void Initialize()
    {

        base.Initialize();
    }

    public override void Draw(GameTime gameTime)
    {

        Game.GraphicsDevice.Clear(Color.Beige);
    }

    public override void Update(GameTime gameTime)
    {

    }
}