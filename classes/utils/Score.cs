using System;
using System.Security.Cryptography.X509Certificates;

namespace desktop.utils;

public class Score{
    public string _nomUtilisateur {get;set;} = "Invité";
    public int _ennemisEnleve {get;set;} = 0;
    public int _experience {get;set;} = 0;
    public DateTime _dateSoumission {get;private set;} = DateTime.Now;
    //Temps en ms
    public int _duree {get;set;} = 0;

    public int getScore(){
        return 100 * _ennemisEnleve + 10000 * _experience + 250 * GetTempsEnSec();
    }
    public int GetTempsEnSec(){
        return (int) Math.Round( (double) _duree/1000); 
    }
    public Score(string nomUtilisateur = "Invité")
    {
        if(_nomUtilisateur != null){
            _nomUtilisateur = nomUtilisateur;
        }
    }
    public void Update(int deltaT){
        _duree += deltaT;
    }
    public DateTime setDate(){
       return _dateSoumission = DateTime.Now;
    }
    
}