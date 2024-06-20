using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        base.OnLand(player);
        Debug.Log($"{player.playerName} landed on Golden Key Tile and drew a golden key.");
        player.DrawGoldenKey(); // 황금 열쇠 이벤트 처리 
    }
}
