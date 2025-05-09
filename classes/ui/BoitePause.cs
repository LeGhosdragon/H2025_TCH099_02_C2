using desktop.gameobjects;
using desktop.pages;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace desktop.ui;

public class BoitePause:Panel{

    protected EcranJeu _ecran;
    public BoitePause(EcranJeu ecranJeu){
        _ecran = ecranJeu;

        Size = new Vector2(0.6f,0.8f);

        Label labelMusique = new Label("Volume Musique");
        this.AddChild(labelMusique);
        Slider sliderMusique = new Slider(0,100);
        sliderMusique.Value = (int)MusiqueAPI._volume * 100;

        sliderMusique.OnValueChange = (Entity entity) =>{
                MusiqueAPI.SetVolume( (float) (((Slider) entity).Value )/100);
        };

        this.AddChild(sliderMusique);

        Button btnQuitter = new Button("Abandonner");
        btnQuitter.OnClick = (e) =>{
            _ecran.FinPartie();
        };
        this.AddChild(btnQuitter);

        Button btnContinuer = new Button("Continuer");
        btnContinuer.OnClick = (e) => {
            _ecran.ChangerPause();
        };
        this.AddChild(btnContinuer);





    }


}