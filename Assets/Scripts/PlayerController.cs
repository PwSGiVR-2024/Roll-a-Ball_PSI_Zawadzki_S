using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action gameOverEvent;

    //punkty w grze
    private int playerLife = 3;

    private KillBoxScript killBox;
    private void Start()
    {
        killBox = FindAnyObjectByType<KillBoxScript>();
        EnemyMovement.onPlayerHit += DecLife;
        ToxicCollectibleScript.toxicPickUpEvent += DecLife;
        killBox.OnKill += DecLife;
    }

    //Dekrementacja ¿ycia
    public void DecLife()
    {
        playerLife--;

        if (playerLife == 0)
        {
            gameOverEvent?.Invoke();
        }
    }

    public int GetPlayerLife()
    {
        return playerLife;
    }

    private void OnDisable()
    {
        EnemyMovement.onPlayerHit -= DecLife;
        ToxicCollectibleScript.toxicPickUpEvent -= DecLife;
        if (killBox != null)
        {
            killBox.OnKill -= DecLife;
        }
    }
}
