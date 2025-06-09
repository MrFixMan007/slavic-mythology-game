using UnityEngine;

public class Sword : Item
{
    [SerializeField] Animator new_player_animator;
    [SerializeField] Sprite new_player_sprite;

    override public void Use(GameObject whoUsed)
    {
        SpriteRenderer sr = whoUsed.GetComponent<SpriteRenderer>();
        PlayerController pc = whoUsed.GetComponent<PlayerController>();
        Animator an = whoUsed.GetComponent<Animator>();
        if (sr != null) 
        {
            sr.sprite = new_player_sprite;
        }
        if (pc != null) 
        {
            //pc.attackDamage = 0;
            //pc.attackRadius = 0;
        }
        if (an != null) 
        {
            an = new_player_animator;
        }
    }
}
