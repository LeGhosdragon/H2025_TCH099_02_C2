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
    public static void dessinerForme(SpriteBatch batch, Vector2[] vertex, Vector2 pos,float largeur)
    {
        dessinerForme(batch, vertex, pos, Color.White,largeur);
    }

    /// <summary>
    /// Dessine une forme vide sur l'ecran a la position demande
    /// </summary>
    /// <param name="batch">Spritebatch de l'ecran</param>
    /// <param name="vertex">arretes de la forme</param>
    /// <param name="pos">position de la forme</param>
    /// <param name="color">Couleur des cotes</param>
    public static void dessinerForme(SpriteBatch batch, Vector2[] vertex, Vector2 pos, Color color,float largeur)
    {
        Vector2 posRel = pos - Camera.getInstance().getPosition();
        for (int i = 0; i < vertex.Length; i++)
        {
            Vector2 debut = vertex[i] + posRel;
            Vector2 fin = vertex[(i + 1) % vertex.Length] + posRel;
            Vector2 diff = debut - fin;
            diff.Normalize();
            debut -= diff * largeur/2;
            fin += diff * largeur/2;
            batch.DrawLine(debut,fin , color,largeur);
        }
    }
}
