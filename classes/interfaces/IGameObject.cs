using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

/// <summary>
/// Classe représentant un objet du jeu qui va être affiché et rafraichis a chaque image
/// </summary>
public interface IGameObject
{
    /// <summary>
    /// Devrait afficher l'objet sur l'écran
    /// </summary>
    /// <param name="device">Élément graphique sur lequel l'objet doit être affiché</param>
    public void Draw(GraphicsDevice device);
    /// <summary>
    /// Rafraichis l'objet a chaque image
    /// </summary>
    /// <param name="deltaT">temps entre la dernière image</param>
    public void Update(float deltaT);
}
