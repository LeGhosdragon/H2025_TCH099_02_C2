using System;
using System.Collections.Generic;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Content;

namespace desktop.ameliorations;
public class Amelioration{
    static List<Amelioration> ameliorations {get;set;} = new List<Amelioration>() ;

    protected Image image {get;set;}
    protected String description {get;set;}

    public static void LoadContent(ContentManager content){
        
    }

}