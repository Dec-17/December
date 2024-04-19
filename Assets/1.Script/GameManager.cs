using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int goldInt = 0;
    public Text goldText;

    void Start()
    {

    }

    void Update()
    {
        UpdateGoldText();
    }

    void UpdateGoldText()
    {
        goldText.text = "Gold: " + goldInt.ToString();
    }
}
