using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberCheck : MonoBehaviour
{
    Vector3 redDiceVelocity, blueDiceVelocity;

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
                    break;
                case "2" :
                    DiceNumberText.redDiceNumber = 2;
                    break;
                case "3" :
                    DiceNumberText.redDiceNumber = 3;
                    break;
                case "4" :
                    DiceNumberText.redDiceNumber = 4;
                    break;
                case "5" :
                    DiceNumberText.redDiceNumber = 5;
                    break;
                case "6" :
                    DiceNumberText.redDiceNumber = 6;
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
                    break;
                case "2" :
                    DiceNumberText.blueDiceNumber = 2;
                    break;
                case "3" :
                    DiceNumberText.blueDiceNumber = 3;
                    break;
                case "4" :
                    DiceNumberText.blueDiceNumber = 4;
                    break;
                case "5" :
                    DiceNumberText.blueDiceNumber = 5;
                    break;
                case "6" :
                    DiceNumberText.blueDiceNumber = 6;
                    break;
            }
        }
    }
}
