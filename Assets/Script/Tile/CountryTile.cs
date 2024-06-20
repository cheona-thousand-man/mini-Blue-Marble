using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryTile : Tile
{
    public Player owner;
    public int purchaseCost;
    public int baseRentCost;
    public List<BuildingType> buildings;

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
        if (owner == null)
        {
            Debug.Log($"{player.playerName} landed on unowned Country Tile and can purchase it for ${purchaseCost}.");
        }
        else if (owner != player)
        {
            int rentAmount = CalculateRent();
            player.PayRent(rentAmount);
            owner.money += rentAmount;
        }
    }

    public void Purchase(Player player)
    {
        if (player.money >= purchaseCost && owner == null)
        {
            player.money -= purchaseCost;
            owner = player;
            Debug.Log($"{player.playerName} purchased {gameObject.name}.");
        }
        else
        {
            Debug.Log($"{player.playerName} cannot purchase {gameObject.name}.");
        }
    }

    public int CalculateRent()
    {
        int rentAmount = baseRentCost;
        foreach (BuildingType building in buildings)
        {
            switch (building)
            {
                case BuildingType.Villa :
                    rentAmount += 50;
                    break;
                case BuildingType.Hotel :
                    rentAmount += 100;
                    break;
                case BuildingType.Landmark :
                    rentAmount += 200;
                    break;
                default :
                    break;
            }
        }
        return rentAmount;
    }
}
