using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class redDiceRoll : MonoBehaviour
{
    static Rigidbody rbRed;
    public static Vector3 redDiceVelocity;

    // 주사위 굴리면 굴리기 기능 비활성화 하는 이벤트
    public static event Action RedDiceRollEvent;

    // Start is called before the first frame update
    void Start()
    {
        rbRed = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        redDiceVelocity = rbRed.velocity;

        // if (Input.GetKeyDown (KeyCode.Space))
        // {
        //     RollDice();
        // }
    }

    public void RollDice()
    {
        float dirX = UnityEngine.Random.Range(0, 150); // Random.Range()는 C# System이 아닌 UnityEngine의 함수
        float dirY = UnityEngine.Random.Range(0, 150);
        float dirZ = UnityEngine.Random.Range(0, 150);
        transform.position = new Vector3(-8, 2, 8); // 던지는 위치 조정
        transform.rotation = Quaternion.identity; // 던지는 면 조정 : 주사위에 고정된 던지는 힘이 틀어지는 걸 방지
        // transform.rotation = Quaternion.Euler(10f, 0f, 10f); // x축에 대해 10도 회전 추가
        rbRed.AddForce(transform.up * UnityEngine.Random.Range(500, 2500));
        rbRed.AddTorque (dirX, dirY, dirZ);
        DiceNumberCheck.redDiceNumber = 0;

        // 주사위 굴렸음을 알려주는 이벤트 발생
        RedDiceRollEvent?.Invoke();
    }
}
