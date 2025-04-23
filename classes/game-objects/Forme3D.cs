using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace desktop.gameobjects;

public class Forme3D : IGameObject
{
    protected Vector3 _angle {get;set;}
    protected Vector3 _vitesse {get;set;}
    protected Vector3 _position {get;set;}
    protected List<Vector3> _cotes {get;set;}
    protected List<Vector3> _coins {get;set;}
    public Forme3D(Vector3 position,List<Vector3> cotes, List<Vector3> coins){
        _angle = new Vector3(
           (float)( Random.Shared.NextDouble() * Math.PI * 2),
           (float)( Random.Shared.NextDouble() * Math.PI * 2),
           (float)( Random.Shared.NextDouble() * Math.PI * 2)
        );
        _vitesse = new Vector3(
            (float)((Random.Shared.NextDouble() - 0.2) * 0.002),
            (float)((Random.Shared.NextDouble() - 0.2) * 0.002),
            (float)((Random.Shared.NextDouble() - 0.2) * 0.002)
        );
        _position = position;
        _coins = coins;
        _cotes = cotes;
        


    }
    public void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    public void Update(float deltaT)
    {
        throw new System.NotImplementedException();
    }
}