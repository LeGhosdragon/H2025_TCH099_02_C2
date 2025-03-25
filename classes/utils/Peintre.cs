using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.utils;

public class Peintre
{
    public static void dessinerForme(SpriteBatch batch, Vector2[] vertex, Vector2 pos)
    {
        dessinerForme(batch, vertex, pos, Color.White);
    }

    public static void dessinerForme(SpriteBatch batch, Vector2[] vertex, Vector2 pos, Color color)
    {
        Vector2 posRel = pos - Camera.getInstance().getPosition();
        for (int i = 0; i < vertex.Length; i++)
        {
            batch.DrawLine(vertex[i] + posRel, vertex[(i + 1) % vertex.Length] + posRel, color);
        }
    }
}
