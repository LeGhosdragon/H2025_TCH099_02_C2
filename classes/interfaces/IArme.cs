using desktop.gameobjects;

namespace desktop.armes;

public interface IArme : IGameObject
{
    /// <summary>
    /// Devrait etre appele lorsqu'une attaque est effectue
    /// </summary>
    public void utiliser();
}
