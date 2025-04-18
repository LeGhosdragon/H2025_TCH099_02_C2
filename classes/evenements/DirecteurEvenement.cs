using System;
using System.Collections.Generic;
using System.Linq;
using desktop.pages;

namespace desktop.evenements;
public class DirecteurEvenement
{
    public List<Evenement> _evenementsActifs { get; set; } = new List<Evenement>();
    public List<Evenement> _evenementsDisponnibles { get; set; } = new List<Evenement>();
    public float temps = 0f;
    public EcranJeu _ecranJeu { get; }
    public float _difficulte { get; private set; } = 1;
    public float _degreeDiff { get; private set; } = 1;

    public DirecteurEvenement(EcranJeu ecranJeu)
    {
        _ecranJeu = ecranJeu;
        _evenementsDisponnibles.Add(new EvenementNormal(this, _ecranJeu));
        _evenementsDisponnibles.Add(new EvenementHorde(this, _ecranJeu));
        _evenementsDisponnibles.Add(new EvenementAmbush(this, _ecranJeu));
        _evenementsDisponnibles.Add(new EvenementGuns(this, _ecranJeu));
        _evenementsDisponnibles.Add(new EvenementFort(this, _ecranJeu));
 
        EvenementNormal firstEvent = new EvenementNormal(this, _ecranJeu);
        EvenementBoss eventBoss = new EvenementBoss(this, _ecranJeu);
        EvenementExp eventExp = new EvenementExp(this, _ecranJeu);
        _evenementsActifs.Add(firstEvent);
        _evenementsActifs.Add(eventBoss);
        _evenementsActifs.Add(eventExp);
        firstEvent.DebuterEvenement(_difficulte, _degreeDiff);
        eventBoss.DebuterEvenement(_difficulte, _degreeDiff);
        eventExp.DebuterEvenement(_difficulte, _degreeDiff);
    }

    public void Update(float deltaT)
    {
        temps += deltaT;
        Console.WriteLine(temps);
        foreach (Evenement evenement in _evenementsActifs.Reverse<Evenement>())
        {
            evenement.Update(deltaT);
        }
    }
    public void FinEvenement(Evenement evenement)
    {
        _difficulte = _difficulte * 1.1f;
        _evenementsActifs.Remove(evenement);
        DebuterEvenement();
    }
    public void DebuterEvenement()
    {
        Evenement evenementChoisis = _evenementsDisponnibles[Random.Shared.Next(_evenementsDisponnibles.Count)];
        _evenementsActifs.Add(evenementChoisis);
        evenementChoisis.DebuterEvenement(_difficulte, _degreeDiff);
    }
}