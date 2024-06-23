using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberCheck : MonoBehaviour
{
    public static int redDiceNumber = 0;
    public static int blueDiceNumber = 0;

    public static event Action DiceNumberCheckEvent;

    private bool isChecking = false;

    // 주사위 정지 여부를 판단
    public static bool IsDiceStopped(Rigidbody rb)
    {
        Vector3 angularVelocity = rb.angularVelocity;
        Vector3 velocity = rb.velocity;
        float angularVelocityThreshold = 0.1f; // 각도 변화량 임계값
        float velocityThreshold = 0.1f; // 속도 변화량 임계값

        if (angularVelocity.magnitude < angularVelocityThreshold && velocity.magnitude < velocityThreshold)
        {
            return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        // 주사위가 트리거에 들어왔을 때 검사 시작
        if (!isChecking)
        {
            isChecking = true;
            StartCoroutine(CheckDiceNumbersCoroutine());
        }
    }

    void OnTriggerStay(Collider other)
    {
        // 주사위 숫자 확인
        if (IsDiceStopped(other.attachedRigidbody))
        {
            RaycastHit hit;
            if (Physics.Raycast(other.transform.position, Vector3.down, out hit, 1f))
            {
                if (hit.collider.CompareTag("DiceFace1"))
                {
                    SetDiceNumber(other, 1);
                }
                else if (hit.collider.CompareTag("DiceFace2"))
                {
                    SetDiceNumber(other, 2);
                }
                else if (hit.collider.CompareTag("DiceFace3"))
                {
                    SetDiceNumber(other, 3);
                }
                else if (hit.collider.CompareTag("DiceFace4"))
                {
                    SetDiceNumber(other, 4);
                }
                else if (hit.collider.CompareTag("DiceFace5"))
                {
                    SetDiceNumber(other, 5);
                }
                else if (hit.collider.CompareTag("DiceFace6"))
                {
                    SetDiceNumber(other, 6);
                }
            }
        }
    }

    IEnumerator CheckDiceNumbersCoroutine()
    {
        // 5초 동안 주사위 숫자 확인
        yield return new WaitForSeconds(5f);

        // 5초 후에 주사위 숫자 판독 완료
        Debug.Log("주사위가 모두 멈췄습니다.");
        Debug.Log($"주사위 숫자 : {redDiceNumber}, {blueDiceNumber}");
        DiceNumberCheckEvent?.Invoke();
        isChecking = false;
    }

    void SetDiceNumber(Collider other, int number)
    {
        if (other.CompareTag("RedDice"))
        {
            redDiceNumber = number;
            Debug.Log($"RedDice 숫자 : {redDiceNumber}");
        }
        else if (other.CompareTag("BlueDice"))
        {
            blueDiceNumber = number;
            Debug.Log($"BlueDice 숫자 : {blueDiceNumber}");
        }
    }
}