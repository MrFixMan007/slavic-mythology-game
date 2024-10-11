using System;

public class RoomProgress : IRoomConsumer, IRoomProducer
{
    public event Action OnDefeated;
    
    public void KillMob() 
    {
        OnDefeated?.Invoke();
    }
}
