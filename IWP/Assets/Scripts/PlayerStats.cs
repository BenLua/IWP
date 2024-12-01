using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 200;
    public static int Lives;
    public int startingLives = 20;

    void Awake()
    {
        Money = startMoney;
        Lives = startingLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
