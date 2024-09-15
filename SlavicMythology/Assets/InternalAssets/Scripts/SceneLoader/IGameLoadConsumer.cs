using System;

public interface IGameLoadConsumer
{
    event Action StartGame;
    event Action StartIntroCatScene;
}