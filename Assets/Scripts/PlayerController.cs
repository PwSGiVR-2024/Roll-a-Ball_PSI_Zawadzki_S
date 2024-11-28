using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action gameOverEvent;

    //punkty w grze
    private int playerLife = 2;

    private void Start()
    {
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
