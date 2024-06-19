using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            DiceNumberText.redDiceNumber = 0;
            float dirX = Random.Range(0, 150);
            float dirY = Random.Range(0, 150);
            float dirZ = Random.Range(0, 150);
            // transform.position = new Vector3(-9, 2, 9); // 던지는 위치 조정
            transform.rotation = Quaternion.identity; // 던지는 면 조정 : 주사위에 고정된 던지는 힘이 틀어지는 걸 방지
            rbRed.AddForce(transform.up * 2000);
            rbRed.AddTorque (dirX, dirY, dirZ);
        }
    }
}
