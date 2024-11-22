using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    //punkty w grze
    private int playerLife = 1;

    //Dekrementacja ¿ycia
    public void DecLife()
    {
        playerLife--;
    }

    public int GetPlayerLife()
    {
        return playerLife;
    }
}
