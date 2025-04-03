using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;

namespace desktop.utils;

public class ChoixSelection
{
    private Texture2D _texture { get; }
    private Texture2DAtlas _atlas { get; }
    private SpriteSheet _sheet { get; }
    private AnimationController _controlleur { get; }
    private Rectangle _rect { get; set; }
    private GraphicsDevice _device { get; set; }
    private int _quantitee { get; }
    private int _numero { get; }
    private int _largeurVid;
    private int _hauteurVid;
    public bool _hovered { get; set; }
    private float _zoom = 1;
    private float _couleurVal = 1;
    public ChoixSelection(int quantitee, int numero, GraphicsDevice device, Texture2D texture, int largeurVid, int hauteurVid, int nbImages)
    {
        this._device = device;
        this._quantitee = quantitee;
        this._numero = numero;
        this._texture = texture;
        this._largeurVid = largeurVid;
        this._hauteurVid = hauteurVid;

        Resize();
        _atlas = Texture2DAtlas.Create("Atlas/video" + numero, texture, largeurVid, hauteurVid, nbImages);
        _sheet = new SpriteSheet("SpriteSheet/sheet" + numero, _atlas);



        _sheet.DefineAnimation("video", builder =>
        {
            builder.IsLooping(true);
            for (int i = 0; i < nbImages; i++)
            {
                builder.AddFrame(i, TimeSpan.FromSeconds(0.1));
            }

        });
        SpriteSheetAnimation spriteSheetAnimation = _sheet.GetAnimation("video");
        _controlleur = new AnimationController(spriteSheetAnimation);


    }
    public void Draw(GameTime gameTime, SpriteBatch batch)
    {
        Texture2DRegion currentFrameTexture = _sheet.TextureAtlas[_controlleur.CurrentFrame];
        Vector2 pos = new Vector2(_numero * _device.Viewport.Width / _quantitee, 0);
        Vector2 scale = new Vector2((float)GetLargeur() / _largeurVid, (float)GetHauteur() / _hauteurVid) * _zoom;
        Color color = new Color(_couleurVal, _couleurVal, _couleurVal);

        batch.Begin(samplerState: SamplerState.PointClamp);
        batch.Draw(currentFrameTexture, pos, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f, _rect);
        batch.End();

    }
    public void Resize()
    {

        _rect = new Rectangle(_numero * GetLargeur(), 0, GetLargeur(), GetHauteur());
    }
    private int GetLargeur()
    {
        return _device.Viewport.Width / _quantitee;
    }
    private int GetHauteur()
    {
        return _device.Viewport.Height;
    }
    public void Update(GameTime gameTime)
    {
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Resize();
        if (_hovered)
        {
            _zoom = Math.Max(1f, _zoom - deltaT);
            _couleurVal = Math.Max(0.5f, _couleurVal - deltaT);
        }
        else
        {
            _zoom = Math.Min(1.2f, _zoom + deltaT);
            _couleurVal = Math.Min(1, _couleurVal + deltaT);
        }
        _controlleur.Update(gameTime);


    }

}