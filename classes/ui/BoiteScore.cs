using System;
using System.Net.NetworkInformation;
using System.Numerics;
using desktop.pages;
using desktop.utils;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace desktop.ui;

public class BoiteScore : Panel{
    public BoiteScore(Score score,EcranJeu ecranJeu){
        Size = new Vector2(0.5f,0.7f);
       
        BoiteStat boiteNom = new BoiteStat("Joueur:",score._nomUtilisateur);
        AddChild(boiteNom);
        
        BoiteStat boiteEnnemis = new BoiteStat("Ennemis tués:",score._ennemisEnleve +"");
        AddChild(boiteEnnemis);

        BoiteStat boiteExp = new BoiteStat("Experience:",score._experience +"");
        AddChild(boiteExp);

        BoiteStat boiteTemps = new BoiteStat("Temps:",score.GetTempsEnSec() +" S");
        AddChild(boiteTemps);

        BoiteStat boiteTotal = new BoiteStat("Score:",score.getScore() +"");
        AddChild(boiteTotal);

        Button btnSuite = new Button("Suivant");
        AddChild(btnSuite);

        btnSuite.OnClick = (Entity)=>{
        ecranJeu.ChargerEcranScore(score);
        };

        IterateChildren(new EventCallback((entity)=>{
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