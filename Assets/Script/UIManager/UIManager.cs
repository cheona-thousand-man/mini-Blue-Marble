using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text currentPlayerInfo;
    public Text diceResult;
    public GameObject board;

    public void UpdateUI()
    {
        currentPlayerInfo.text = $"Current Player : GameManager.Instance.game.currentPlayer.playerName";
        diceResult.text = $"Dice Result : {GameManager.Instance.uiManager.diceResult}";
        // 이하 UI 업데이트 로직 추가
    }

    public void ShowMessage(string message)
    {
        // 메세지를 화면에 표시하는 로직
        Debug.Log(message);
    }
}
