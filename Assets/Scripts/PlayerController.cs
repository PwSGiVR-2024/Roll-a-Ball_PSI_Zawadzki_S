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
        EnemyScript.onPlayerHit += DecLife;
        ToxicCollectibleScript.toxicPickUpEvent += DecLife;
        LifeCollectibleScript.lifePickUpEvent += IncLife;
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

    public void IncLife()
    {
        Debug.Log("Inkrementacja ¿ycia");
        playerLife++;
    }

    public int GetPlayerLife()
    {
        return playerLife;
    }

    private void OnDisable()
    {
        EnemyScript.onPlayerHit -= DecLife;
        ToxicCollectibleScript.toxicPickUpEvent -= DecLife;
        LifeCollectibleScript.lifePickUpEvent -= IncLife;
        if (killBox != null)
        {
            killBox.OnKill -= DecLife;
        }
    }
}
