using System;
using System.Security.Cryptography.X509Certificates;

namespace desktop.utils;

public class Score{
    public String _nomUtilisateur {get;set;}
    public int _ennemisEnleve {get;set;} = 0;
    public int _experience {get;set;} = 0;
    public DateTime _dateSoumission {get;set;} = DateTime.Now;
    //Temps en ms
    public int _duree {get;set;} = 0;

    public int getScore(){
        return _ennemisEnleve * _experience * _duree;
    }
    public Score(String nomUtilisateur = "Invité")
    {
        _nomUtilisateur = nomUtilisateur;
    }
    public void Update(int deltaT){
        _duree += deltaT;
    }
    
}