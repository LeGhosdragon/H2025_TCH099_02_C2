using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.utils;

public class Peintre
{
    /// <summary>
    /// Dessine une forme vide sur l'ecran a la position demande (en blanc)
    /// </summary>
    /// <param name="batch">Spritebatch de l'ecran</param>
    /// <param name="vertex">arretes de la forme</param>
    /// <param name="pos">position de la forme</param>
    public static void dessinerForme(SpriteBatch batch, Vector2[] vertex, Vector2 pos)
    {
        dessinerForme(batch, vertex, pos, Color.White);
    }

    /// <summary>
    /// Dessine une forme vide sur l'ecran a la position demande
    /// </summary>
    /// <param name="batch">Spritebatch de l'ecran</param>
    /// <param name="vertex">arretes de la forme</param>
    /// <param name="pos">position de la forme</param>
    /// <param name="color">Couleur des cotes</param>
    public static void dessinerForme(SpriteBatch batch, Vector2[] vertex, Vector2 pos, Color color)
    {
        Vector2 posRel = pos - Camera.getInstance().getPosition();
        for (int i = 0; i < vertex.Length; i++)
        {
            batch.DrawLine(vertex[i] + posRel, vertex[(i + 1) % vertex.Length] + posRel, color);
        }
    }
}
