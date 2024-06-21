using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redDiceRoll : MonoBehaviour
{
    static Rigidbody rbRed;
    public static Vector3 redDiceVelocity;

    // Start is called before the first fra me update
    void Start()
    {
        rbRed = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        redDiceVelocity = rbRed.velocity;

        if (Input.GetKeyDown (KeyCode.Space))
        {
            RollDice();
        }
    }

    public void RollDice()
    {
        float dirX = Random.Range(0, 150);
        float dirY = Random.Range(0, 150);
        float dirZ = Random.Range(0, 150);
        transform.position = new Vector3(-8, 2, 8); // 던지는 위치 조정
        transform.rotation = Quaternion.identity; // 던지는 면 조정 : 주사위에 고정된 던지는 힘이 틀어지는 걸 방지
        // transform.rotation = Quaternion.Euler(10f, 0f, 10f); // x축에 대해 10도 회전 추가
        rbRed.AddForce(transform.up * Random.Range(500, 2500));
        rbRed.AddTorque (dirX, dirY, dirZ);
        DiceNumberText.redDiceNumber = 0;
        DiceNumberCheck.redDiceNumber = 0;
    }
}
