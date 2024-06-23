using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceNumberCheck : MonoBehaviour
{
    // Vector3 redDiceVelocity, blueDiceVelocity;
    public static int redDiceNumber = 0;
    public static int blueDiceNumber = 0;

    // 숫자 확인이 끝났음을 알리는 이벤트 처리
    public static event Action DiceNumberCheckEvent;

    // 주사위의 방향 벡터를 통해 면의 숫자를 지정
    public static Vector3[] diceDirectionList = new Vector3[]
    {
        new Vector3(0, 0, 1), // 1면
        new Vector3(0, 1, 0), // 2면
        new Vector3(-1, 0, 0), // 3면
        new Vector3(1, 0, 0), // 4면
        new Vector3(0, -1, 0), // 5면
        new Vector3(0, 0, -1) // 6면
    };

    // 주사위 면 중에서 가장 그럴싸한 숫자를 파악(Vector 각이 가장 작은 면)
    public static int GetDiceNumberByDirection(Vector3 diceDirection)
    {
        float minAngle = Mathf.Infinity; // 양의 무한대로 초기값 설정
        int resultNumber = 0;

        for (int i = 0; i < diceDirectionList.Length; i++)
        {
            Vector3 direction = diceDirectionList[i];
            float angle = Vector3.Angle(diceDirection, direction);

            if (angle < minAngle)
            {
                minAngle = angle;
                resultNumber = i + 1; // 배열은 0부터 시작하므로, 1면은 배열 인덱스가 0
            }
        }
        return resultNumber;
    }

    // 주사위 정지 여부를 판단
    public static bool IsDiceStopped(Rigidbody rb)
    {
        Vector3 angularVelocity = rb.angularVelocity;
        float angularVelocityThreshold = 0f; // 각도 변화량 임계값

        for (int i = 0; i < 3; i++) // x, y, z축에 대해 3개의 변위각을 가지므로 3번 비교
        {
            if (Mathf.Abs(angularVelocity[i]) > angularVelocityThreshold)
            {
                return false;
            }
        }
        return true;
    }

    void OnTriggerStay(Collider other) 
    {
        //redDice number check
        if (other.gameObject.tag == "RedDice")
        {
            if (IsDiceStopped(redDiceRoll.rbRed))
            {
                redDiceNumber = GetDiceNumberByDirection(redDiceRoll.rbRed.transform.up); // 윗면을 보는 Vector와 가까운 주사위 면 비교
                Debug.Log($"RedDice 숫자 : {redDiceNumber}");
            }
        }

        //blueDice number check
        if (other.gameObject.tag == "BlueDice")
        {
            if (IsDiceStopped(blueDiceRoll.rbBlue))
            {
                blueDiceNumber = GetDiceNumberByDirection(blueDiceRoll.rbBlue.transform.up); // 윗면을 보는 Vector와 가까운 주사위 면 비교
                Debug.Log($"BlueDice 숫자 : {blueDiceNumber}");
            }
        }

        Debug.Log("주사위 숫자 점검중입니다.");
        // 숫자 점검이 끝난 경우 = Red/Blue 주사위 모두 운동량이 0일 때!!
        if (IsDiceStopped(redDiceRoll.rbRed) && IsDiceStopped(blueDiceRoll.rbBlue))
        {
            Debug.Log("주사위가 모두 멈췄습니다.");
            Debug.Log($"주사위 숫자 : {redDiceNumber}, {blueDiceNumber}");
            DiceNumberCheckEvent?.Invoke(); // 숫자 확인이 끝났음을 넘겨줌
            return; // 숫자 확인하는 트리거 종료
        }
    }
}