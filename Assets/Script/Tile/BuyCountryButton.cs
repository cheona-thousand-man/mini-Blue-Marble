using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCountryButton : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {  
       gameManager = GameManager.Instance; 
    }

    public void OnBuyButtonClick()
    {
        gameManager.DoYouBuyCountryUIoff(); // 구매 선택했으므로 UI 종료
        if (gameManager.game.tiles[gameManager.targetTileIndex] is CountryTile countryTile) // Tile 오브젝트가 CountyTile일 경우 type 캐스팅
        {
            gameManager.game.currentPlayer.PurchaseTile(countryTile, gameManager.game.currentPlayer); // 타일 구매는 플레이어에서 처리
        }
        gameManager.BuyCountryUIon(); // 구매 완료 UI
        Invoke("BuyCountryUIoff", 2.0f); // 구매 완료 UI 종료
    }

    public void OnNoButtonClick()
    {
        gameManager.DoYouBuyCountryUIoff(); // 거부 선택했으므로 UI 종료
        Invoke("ProcessTurnEnd", 0f); // 구매 거부 시 Country Tile 이벤트 종료하여 ProcessTurnEnd() 호출
    }

    public void BuyCountryUIoff()
    {
        gameManager.BuyCountryUIoff();
    }

    public void ProcessTurnEnd()
    {
        gameManager.ProcessTurnEnd();
    }
}
