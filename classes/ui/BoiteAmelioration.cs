using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ui;

public class BoiteAmelioration: Panel{
    private int _numero;
    private int _quantiee;    
    public BoiteAmelioration(int quantiee, int numero,GraphicsDevice graphicsDevice){
        _numero = numero; 
        _quantiee = quantiee;

        Size = new Vector2(0.8f/quantiee,1f); 

        //Pour plus que trois, la taille minimale devrait etre modifie
        MinSize = new Vector2(300,600);
        Anchor = Anchor.CenterLeft;
        
        

        Update(0,graphicsDevice);
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
        };
    }
    public static BoiteAmelioration[] genererAmelioration(int quantiee,GraphicsDevice graphicsDevice){
        BoiteAmelioration[] boites = new BoiteAmelioration[quantiee];
        for(int i = 0; i < quantiee; i++){
            boites[i] = new BoiteAmelioration(quantiee,i,graphicsDevice);
        }
        Console.WriteLine(boites.Length);
        return boites;
    }
    public void Update(float deltaT,GraphicsDevice graphicsDevice){
        float marges = 20;
        float largeur = graphicsDevice.Viewport.Width - 2 * marges;
        Offset = new Vector2((float)_numero / (float)_quantiee *  largeur + 0.1f/(float)_quantiee * largeur,0);
    }
 
}