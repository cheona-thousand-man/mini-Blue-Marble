using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollButton : MonoBehaviour
{
    public redDiceRoll rDiceRoll;
    public blueDiceRoll bDiceRoll;

    // Start is called before the first frame update
    void Start()
    {
        rDiceRoll = GetComponent<redDiceRoll>();
        bDiceRoll = GetComponent<blueDiceRoll>();
    }

    void OnButtonClick()
    {
        rDiceRoll.RollDice();
        bDiceRoll.RollDice();
    }
}
