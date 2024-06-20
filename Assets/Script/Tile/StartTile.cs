using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : Tile
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
        player.money += 500; // 출발지점 보상
    }
}
