using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    public PlayerController player;
    public void PlayerAttackEvent()
    {
        Debug.Log("Player Attack Event Triggered");
        // Add attack logic here
        player.DoAttack();
    }
    
    public void PlayerDamage()
    {
        transform.GetComponentInParent<EnemyCotroller>().damagePlayer();
    }

    public void MoveSound()
    {
        LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[0], LevelManager.instance.player.position);
    }
}
