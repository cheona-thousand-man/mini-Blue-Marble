using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueDiceRoll : MonoBehaviour
{
    static Rigidbody rbBlue;
    public static Vector3 blueDiceVelocity;

    // 주사위 굴리면 굴리기 기능 비활성화 하는 이벤트 : redDiceRoll에 있으므로 비활성화
    // public static event Action DiceRollEvent;

    // Start is called before the first fra me update
    void Start()
    {
        rbBlue = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {  
        blueDiceVelocity = rbBlue.velocity;

        if (Input.GetKeyDown (KeyCode.Space))
        {
            RollDice();
        }
    }

    public void RollDice()
    {
        float dirX = UnityEngine.Random.Range(0, 150);
        float dirY = UnityEngine.Random.Range(0, 150);
        float dirZ = UnityEngine.Random.Range(0, 150);
        transform.position = new Vector3(-6, 2, 6); // 던지는 위치 조정
        transform.rotation = Quaternion.identity; // 던지는 면 조정 : 주사위에 고정된 던지는 힘이 틀어지는 걸 방지
        // transform.rotation = Quaternion.Euler(10f, 0f, 10f); // x축에 대해 10도 회전 추가
        rbBlue.AddForce(transform.up * UnityEngine.Random.Range(500, 2500));
        rbBlue.AddTorque (dirX, dirY, dirZ);
        DiceNumberCheck.blueDiceNumber = 0;
    }
}
