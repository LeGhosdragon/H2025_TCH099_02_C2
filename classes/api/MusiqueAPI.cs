using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace desktop;


public static class MusiqueAPI{
    public enum Musiques{
        RIEN,
    }
    public static Dictionary<Musiques,Song> fichiersMusique;
    public static void LoadContent(ContentManager content){
        fichiersMusique = new Dictionary<Musiques, Song>();

        Song song = content.Load<Song>("");
        fichiersMusique.Add(Musiques.RIEN,null);
    }

    public static LinkedList<Musiques> file = new LinkedList<Musiques>();

    
}