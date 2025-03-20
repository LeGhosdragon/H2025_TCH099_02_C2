using Microsoft.Xna.Framework;

namespace desktop.pages;

/// <summary>
/// Represente un écran qui est affiché, il devrait y en avoir seulement un a la fois
/// </summary>
public interface IPage
{
    /// <summary>
    /// Dessine la page
    /// </summary>
    /// <param name="gameTime">temps du jeu (selon le dernier appel de la fonction)</param>
    public void Draw(GameTime gameTime);
    /// <summary>
    /// Rafraichis la page
    /// </summary>
    /// <param name="gameTime">temps du jeu (selon le dernier appel de la fonction)</param>
    public void Update(GameTime gameTime);
}
