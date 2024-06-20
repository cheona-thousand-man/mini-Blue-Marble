using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBounce : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        // 주사위의 Rigiedbody 컴포넌트 가져오기
         rb = GetComponent<Rigidbody>();

         // Rigidbody의 bounciness와 관련된 물리 재질 설정 (옵션)
         PhysicMaterial material = new PhysicMaterial();
         material.bounciness = 0.5f; // 반발계수 설정(0.0에서 1.0 사이의 값, 1.0은 완전 반발)
         material.bounceCombine = PhysicMaterialCombine.Maximum;

         Collider collider = GetComponent<Collider>();
         if (collider != null)
         {
            collider.material = material;
         }
    }

    // 충돌 감지
    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("RedDice") || collision.gameObject.CompareTag("BlueDice"))
        {
            // 벽과 충돌 시 반발 벡터 계산
            Vector3 normal = collision.contacts[0].normal;
            Vector3 velocity = rb.velocity;

            // 반발 벡터 계산
            Vector3 reflection = Vector3.Reflect(velocity, normal);

            // 반발 벡터 설정
            rb.velocity = reflection;
        }   
    }
}
