using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CountryTile : Tile
{
    public Player owner;
    public int purchaseCost = 100;
    public int baseRentCost = 150;
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
        // base.OnLand(player); // Tile 클래스에서 별도로 구현된 기능 없음
        if (owner == null)
        {
            Debug.Log($"{player.playerName} landed on unowned Country Tile and can purchase it for ${purchaseCost}.");
            GameManager.Instance.DoYouBuyCountryUIon(); // 구매할지 안내 UI
        }
        else if (owner != player)
        {
            int rentAmount = CalculateRent();
            player.PayRent(rentAmount);
            owner.money += rentAmount;
        }
        // else if (owner == player) // 빌딩 건설 기능은 나중에 구현
        // {

        // }
    }

    public void OnBuyButtonClick()
    {
        Debug.Log("구매 버튼이 클릭되었습니다.");

        
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
