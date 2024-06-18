using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    Vector3 diceVelocity;

    void FixedUpdate() 
    {
        diceVelocity = DiceScript.diceVelocity;
    }

    void OnTriggerStay(Collider other) 
    {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch (other.gameObject.name)
            {
                case "1" :
                    DiceNumberTextScript.diceNumber = 1;
                    break;
                case "2" :
                    DiceNumberTextScript.diceNumber = 2;
                    break;
                case "3" :
                    DiceNumberTextScript.diceNumber = 3;
                    break;
                case "4" :
                    DiceNumberTextScript.diceNumber = 4;
                    break;
                case "5" :
                    DiceNumberTextScript.diceNumber = 5;
                    break;
                case "6" :
                    DiceNumberTextScript.diceNumber = 6;
                    break;
            }
        }
    }
}
