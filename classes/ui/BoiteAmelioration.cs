using System;
using desktop.ameliorations;
using desktop.ameliorations.joueur;
using desktop.pages;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ui;

public class BoiteAmelioration: Panel{
    private int _numero;
    private int _quantiee;    
    private EcranJeu _ecranJeu;
    private Amelioration _amelioration;
    private Image _image;
    public BoiteAmelioration(int quantiee, int numero,EcranJeu ecranJeu,Amelioration amelioration){
        _numero = numero; 
        _quantiee = quantiee;
        _ecranJeu = ecranJeu;
        this._amelioration = amelioration;

        Size = new Vector2(0.8f/quantiee,1f); 

        //Pour plus que trois, la taille minimale devrait etre modifie
        MinSize = new Vector2(300,600);
        Anchor = Anchor.CenterLeft;
        _image = new Image(_amelioration._image,new Vector2(200,200));
        _image.Anchor = Anchor.TopCenter;
        AddChild(_image);

        HorizontalLine line = new HorizontalLine();
        AddChild(line);

        Paragraph description = new Paragraph(amelioration._description);
        AddChild(description);


        Update(0,_ecranJeu.GraphicsDevice);
        OnMouseEnter = (Entity panel) => {
            this.FillColor = new Color(0.8f,0.8f,0.8f);
        };
        OnMouseLeave = (Entity panel) => {
            this.FillColor = new Color(1f,1f,1f);
        };
        OnMouseDown = (Entity panel) => {
            this.FillColor = new Color(0.7f,0.7f,0.7f);
        };
        OnMouseReleased = (Entity panel) => {
            this.FillColor = new Color(0.8f,0.8f,0.8f);
            amelioration.Appliquer(_ecranJeu);
        };

    }
    public static BoiteAmelioration[] genererAmelioration(int quantiee,EcranJeu ecranJeu){
        BoiteAmelioration[] boites = new BoiteAmelioration[quantiee];
        Amelioration[] ameliorations = Amelioration.obtenirAmeliorations(quantiee,ecranJeu._joueur._arme);
        for(int i = 0; i < quantiee; i++){
            boites[i] = new BoiteAmelioration(quantiee,i,ecranJeu,ameliorations[i]);
        }
        return boites;
    }
    public void Update(float deltaT,GraphicsDevice graphicsDevice){
        _image.CalcAutoHeight();

        float marges = 20;
        float largeur = graphicsDevice.Viewport.Width - 2 * marges;
        Offset = new Vector2((float)_numero / (float)_quantiee *  largeur + 0.1f/(float)_quantiee * largeur,0);
    }
 
}