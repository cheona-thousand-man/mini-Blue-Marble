using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberCheck : MonoBehaviour
{
    Vector3 redDiceVelocity, blueDiceVelocity;
    public static int redDiceNumber = 0;
    public static int blueDiceNumber = 0;

    void FixedUpdate() 
    {
        redDiceVelocity = redDiceRoll.redDiceVelocity;
        blueDiceVelocity = blueDiceRoll.blueDiceVelocity;
    }

    void OnTriggerStay(Collider other) 
    {
        //redDice number check
        if (other.gameObject.tag == "RedDice" && redDiceVelocity.x == 0f && redDiceVelocity.y == 0f && redDiceVelocity.z == 0f)
        {
            switch (other.gameObject.name)
            {
                case "1" :
                    DiceNumberText.redDiceNumber = 1;
                    redDiceNumber = 1;
                    break;
                case "2" :
                    DiceNumberText.redDiceNumber = 2;
                    redDiceNumber = 2;
                    break;
                case "3" :
                    DiceNumberText.redDiceNumber = 3;
                    redDiceNumber = 3;
                    break;
                case "4" :
                    DiceNumberText.redDiceNumber = 4;
                    redDiceNumber = 4;
                    break;
                case "5" :
                    DiceNumberText.redDiceNumber = 5;
                    redDiceNumber = 5;
                    break;
                case "6" :
                    DiceNumberText.redDiceNumber = 6;
                    redDiceNumber = 6;
                    break;
            }
        }
        //blueDice number check
        if (other.gameObject.tag == "BlueDice" && blueDiceVelocity.x == 0f && blueDiceVelocity.y == 0f && blueDiceVelocity.z == 0f)
        {
            switch (other.gameObject.name)
            {
                case "1" :
                    DiceNumberText.blueDiceNumber = 1;
                    blueDiceNumber = 1;
                    break;
                case "2" :
                    DiceNumberText.blueDiceNumber = 2;
                    blueDiceNumber = 2;
                    break;
                case "3" :
                    DiceNumberText.blueDiceNumber = 3;
                    blueDiceNumber = 3;
                    break;
                case "4" :
                    DiceNumberText.blueDiceNumber = 4;
                    blueDiceNumber = 4;
                    break;
                case "5" :
                    DiceNumberText.blueDiceNumber = 5;
                    blueDiceNumber = 5;
                    break;
                case "6" :
                    DiceNumberText.blueDiceNumber = 6;
                    blueDiceNumber = 6;
                    break;
            }
        }
    }
}
