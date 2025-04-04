namespace desktop.utils;

/// <summary>
/// Utilise pour avoir un effet a chaque n secondes
/// </summary>
public class Chrono
{
    //Delai en secondes
    public float _delai {get;set;}
    private float _tempsAct;

    //Determine si le delai doit etre reinitialise manuellement ou non
    private bool _manuel;

    /// <summary>
    /// Genere un chronometre
    /// </summary>
    /// <param name="delai">Delai entre les retours (en secondes)</param>
    public Chrono(float delai, bool manuel = false)
    {
        this._delai = delai;
        this._manuel = manuel;
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
            if (!_manuel)
            {
                _tempsAct -= _delai;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Permet de remetre le temps ecoule a 0
    /// </summary>
    public void reinitialiser()
    {
        this._tempsAct = 0;
    }
}
