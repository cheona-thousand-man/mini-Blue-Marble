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

    void Update() // 주사위 이벤트에 따라 DiceRoll 스크립트가 비활성화 되므로, 주사위 벡터는 여기서 계산
    {
        redDiceRoll.redDiceVelocity = redDiceRoll.rbRed.velocity;
        blueDiceRoll.blueDiceVelocity = blueDiceRoll.rbBlue.velocity;
    }

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
        }
        Debug.Log("주사위 숫자 점검중입니다.");
        Debug.Log($"RedDice Veolcity : {redDiceVelocity}, BlueDice Velocity : {blueDiceVelocity}");
        // 숫자 점검이 끝난 경우 = Red/Blue 주사위 모두 운동량이 0일 때!!
        if (redDiceVelocity.x == 0f && redDiceVelocity.y == 0f && redDiceVelocity.z == 0f && blueDiceVelocity.x == 0f && blueDiceVelocity.y == 0f && blueDiceVelocity.z == 0f)
        {
            Debug.Log("주사위가 모두 멈췄습니다.");
            Debug.Log($"주사위 숫자 : {redDiceNumber}, {blueDiceNumber}");
            DiceNumberCheckEvent?.Invoke(); // 숫자 확인이 끝났음을 넘겨줌
            return; // 숫자 확인하는 트리거 종료
        }
    }
}
