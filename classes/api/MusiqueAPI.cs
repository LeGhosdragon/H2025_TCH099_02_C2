using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace desktop;


public static class MusiqueAPI{

    private static Dictionary<Musique,Song> fichiersMusique;

    public static float _volume {get;private set;} = 0.1f;
    public static void SetVolume(float volume){
        MediaPlayer.Volume = volume;
        _volume = volume;
    }
    public static void Jouer(Musique musique){
        Song song = null;
        try{
            song = GetSong(musique);
        }catch (NullReferenceException){
            
        }
        if(song == null){
            MediaPlayer.Stop();
            return;
        }
        MediaPlayer.Play(song);
    }
    private static Song GetSong(Musique musique){
        fichiersMusique.TryGetValue(musique, out Song song);
        return song;
    }

    public enum Musique{
        RIEN,
        MENU,
        SPEED,
        KIM,
        SPACE1,
        SPACE2,
        SPACE3,
        NORMAL_INTENSE,
        DIFFICULTY,
        DIFFICULTY2,
        BOSS,
        BOSS404,
        MILK_MAN,
        GAME_OVER
    }
    
    public static void LoadContent(ContentManager content){

        MediaPlayer.IsRepeating = true;

        fichiersMusique = new Dictionary<Musique, Song>();

        fichiersMusique.Add(Musique.RIEN,null);

        Song song = content.Load<Song>("musiques/difficultyllup");
        fichiersMusique.Add(Musique.DIFFICULTY2,song);

        song = content.Load<Song>("musiques/fast-chiptune-instrumental-2-minute-boss-fight-254040");
        fichiersMusique.Add(Musique.SPEED,song);

        song = content.Load<Song>("musiques/fun-with-my-8-bit-game-301278");
        fichiersMusique.Add(Musique.NORMAL_INTENSE,song);

        song = content.Load<Song>("musiques/game-over-38511");
        fichiersMusique.Add(Musique.GAME_OVER,song);

        song = content.Load<Song>("musiques/glitch-in-the-dark-306765");
        fichiersMusique.Add(Musique.BOSS,song);

        song = content.Load<Song>("musiques/jungle-ish-beat-for-video-games-314073");
        fichiersMusique.Add(Musique.SPACE3,song);

        song = content.Load<Song>("musiques/kim-lightyear-legends-109307");
        fichiersMusique.Add(Musique.KIM,song);

        song = content.Load<Song>("musiques/level-iii-medium-294426");
        fichiersMusique.Add(Musique.DIFFICULTY,song);

        song = content.Load<Song>("musiques/mold-250080");
        fichiersMusique.Add(Musique.BOSS404,song);

        song = content.Load<Song>("musiques/neon-gaming-128925");
        fichiersMusique.Add(Musique.SPACE2,song);

        song = content.Load<Song>("musiques/panic-182769");
        fichiersMusique.Add(Musique.MILK_MAN,song);

        song = content.Load<Song>("musiques/space-station-247790");
        fichiersMusique.Add(Musique.SPACE1,song);

        song = content.Load<Song>("musiques/kahoot");
        fichiersMusique.Add(Musique.MENU,song);
    }
}