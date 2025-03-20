namespace desktop.utils;

/// <summary>
/// Utilise pour avoir un effet a chaque n secondes
/// </summary>
public class Chrono
{
    //Delai en secondes
    private float _delai;
    private float _tempsAct;

    /// <summary>
    /// Genere un chronometre
    /// </summary>
    /// <param name="delai">Delai entre les retours (en secondes)</param>
    public Chrono(float delai)
    {
        this._delai = delai;
    }

    /// <summary>
    /// Utilise pour valider si le delai a ete atteint
    /// </summary>
    /// <param name="deltaT">duree entre le dernier appel</param>
    /// <returns>true si le delai a ete atteint</returns>
    public bool Update(float deltaT)
    {
        _tempsAct += deltaT;
        if (_tempsAct >= _delai)
        {
            _tempsAct -= _delai;
            return true;
        }
        return false;
    }
}
