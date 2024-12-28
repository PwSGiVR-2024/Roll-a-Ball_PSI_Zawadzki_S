using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action gameOverEvent;

    //punkty w grze
    private int playerLife = 3;

    private void Start()
    {
        ToxicCollectibleScript.toxicPickUpEvent += DecLife;
        FindAnyObjectByType<KillBoxScript>().OnKill += DecLife;
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
}
