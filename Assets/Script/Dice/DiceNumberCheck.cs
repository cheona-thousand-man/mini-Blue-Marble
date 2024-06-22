using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberCheck : MonoBehaviour
{
    Vector3 redDiceVelocity, blueDiceVelocity;
    public static int redDiceNumber = 0;
    public static int blueDiceNumber = 0;

    // 숫자 확인이 끝났음을 알리는 이벤트 처리
    public static event Action DiceNumberCheckEvent;

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
                    redDiceNumber = 1;
                    break;
                case "2" :
                    redDiceNumber = 2;
                    break;
                case "3" :
                    redDiceNumber = 3;
                    break;
                case "4" :
                    redDiceNumber = 4;
                    break;
                case "5" :
                    redDiceNumber = 5;
                    break;
                case "6" :
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
                    blueDiceNumber = 1;
                    break;
                case "2" :
                    blueDiceNumber = 2;
                    break;
                case "3" :
                    blueDiceNumber = 3;
                    break;
                case "4" :
                    blueDiceNumber = 4;
                    break;
                case "5" :
                    blueDiceNumber = 5;
                    break;
                case "6" :
                    blueDiceNumber = 6;
                    break;
            }
            DiceNumberCheckEvent?.Invoke(); // 숫자 확인이 끝났음을 넘겨줌
        }
    }
}
