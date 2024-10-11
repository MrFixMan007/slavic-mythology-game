using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Room : MonoBehaviour
{
    int enemy_cnt, died;
    [SerializeField] private List<Door> _doors;
    private RoomProgress _roomProgress;
    private IRoomConsumer _roomConsumer;

    [Inject]
    public void Initialize(RoomProgress RP)
    {
        _roomProgress = RP;
        _roomConsumer.OnDefeated += OnMobDie;
        enemy_cnt = GetComponentsInChildren<Enemy>().Length;
    }

    private void OnMobDie() 
    {
        died++;
        if (died >= enemy_cnt)
        {
            Debug.Log("room cleared");
            foreach (var door in _doors)
            {
                door.open();
            }
        }
        Debug.Log("mob dead");
    }
}
