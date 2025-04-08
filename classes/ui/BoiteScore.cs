using System;
using System.Net.NetworkInformation;
using System.Numerics;
using desktop.utils;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace desktop.ui;

public class BoiteScore : Panel{
    private Score _score;
    public BoiteScore(Score score){
        _score = score;



        BoiteStat boiteNom = new BoiteStat("Joueur:",score._nomUtilisateur);
        AddChild(boiteNom);
        
        BoiteStat boiteEnnemis = new BoiteStat("Ennemis tués:",score._ennemisEnleve +"");
        AddChild(boiteEnnemis);

        BoiteStat boiteExp = new BoiteStat("Experience:",score._experience +"");
        AddChild(boiteExp);

        BoiteStat boiteTemps = new BoiteStat("Temps:",score._duree +"S");
        AddChild(boiteTemps);

        BoiteStat boiteTotal = new BoiteStat("Score:",score.getScore() +"");
        AddChild(boiteTotal);

        IterateChildren(new EventCallback((entity)=>{
            Console.Write(Children.Count);
            entity.Size = new Vector2(0.9f, 0.9f/(float)Children.Count);
            entity.Anchor = Anchor.AutoCenter;
        }));

    }
    
}
class BoiteStat : Panel{


    public BoiteStat(String stat,String valeur){
        Size = new Vector2(0.9f,0.3f);
        Paragraph txtStat = new Paragraph(stat,Anchor.CenterLeft);
        AddChild(txtStat);

        Paragraph txtValeur = new Paragraph(valeur,Anchor.CenterRight);
        AddChild(txtValeur);
    }
}