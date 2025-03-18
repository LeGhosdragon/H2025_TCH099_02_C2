using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace desktop;

public class Game1 : Game
{
    Texture2D texture;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    VertexPositionColor[] _vertexPositionColors;
    BasicEffect _basicEffect;
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    _vertexPositionColors = new[]
    {
        new VertexPositionColor(new Vector3(0, 0, 0), Color.White),
        new VertexPositionColor(new Vector3(100, 0, 0), Color.White),
        new VertexPositionColor(new Vector3(100, 100, 0), Color.White),
        new VertexPositionColor(new Vector3(0, 100, 0), Color.White)
    };
    _basicEffect = new BasicEffect(GraphicsDevice);
    _basicEffect.World = Matrix.CreateOrthographicOffCenter(
        0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, 1);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        // TODO: Add your drawing code here
    EffectTechnique effectTechnique = _basicEffect.Techniques[0];
    EffectPassCollection effectPassCollection = effectTechnique.Passes;
    foreach (EffectPass pass in effectPassCollection)
    {
        pass.Apply();
        GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, _vertexPositionColors, 0, 3);
    }

        base.Draw(gameTime);
    }
}
