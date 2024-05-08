using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMonster : Mob
{
    void Start()
    {
        mobHP = 5f;
        NomalItemProbability = 0.85f;
        EpicItemProbability = 0.15f;
        mobSpeed = 3f;
        detectionRange = 50f;
        damageColor = new Color(1f, 0.5f, 0.5f);
        originalColor = new Color(1f, 1f, 1f);
    }

    void Update()
    {
        Testvirtual();
        FindPlayer();
    }
}
