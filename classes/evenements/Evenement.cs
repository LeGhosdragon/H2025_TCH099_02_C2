using System;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;
public class Evenement
{

    protected Chrono _duree { get; set; }
    protected float _difficulte { get; set; }
    protected float _degreeDiff;

    protected bool _isFinissable { get; set; }
    protected DirecteurEvenement _directeur { get; }
    protected EcranJeu _ecran { get; }
    public Evenement(DirecteurEvenement directeur, EcranJeu ecranJeu, float duree, bool isFinissable = true)
    {
        _duree = new Chrono(duree, true);
        _directeur = directeur;
        _ecran = ecranJeu;
        _isFinissable = isFinissable;
    }
    public virtual void DebuterEvenement(float difficulte, float degreeDiff)
    {
        _difficulte = difficulte;
        _degreeDiff = degreeDiff;
        _duree.reinitialiser();
    }
    public virtual void Update(float deltaT)
    {
        if (_duree.Update(deltaT) && _isFinissable)
        {
            FinEvenement();
        }
    }

    public virtual void FinEvenement()
    {
        Console.WriteLine("Fin de l'evenement");
        _directeur.FinEvenement(this);
    }
}