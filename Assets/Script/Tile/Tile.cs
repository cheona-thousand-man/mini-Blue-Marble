using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 tilePosition;
    public static Action EndTileEvent;

    void Start()
    {
        tilePosition = transform.position;
    }

    public virtual void OnLand(Player player)
    {
        Debug.Log($"{player.playerName} landed on Tile at position {tilePosition}.");
        // Implement tile-specific actions
    }
}
