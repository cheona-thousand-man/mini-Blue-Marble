using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoldenKeyTile : Tile
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("StartTile collieded with " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            OnLand(player);
        }
    }

    public override void OnLand(Player player)
    {
        // base.OnLand(player); // Tile 클래스에서 별도로 구현된 기능 없음
        Debug.Log($"{player.playerName} landed on Golden Key Tile and drew a golden key.");
        player.DrawGoldenKey(); // 황금 열쇠 이벤트 처리 : 지금은 일단 보너스 지급 이벤트
    }

    // 여러가지 이벤트를 함수로 정리하여 배열
    // 1. 델리게이트 타입을 정의하고 함수를 배열에 저장, 2. 무작위로 함수를 선택하여 호출
}
