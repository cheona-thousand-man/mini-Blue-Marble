using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public string currentPlayerInfo;
    public int diceResult;
    public GameObject board;

    public void InitializeUI()
    {
        // add Initialize UI
    }

    public void UpdateUI()
    {
        if (GameManager.Instance == null || GameManager.Instance.game == null || GameManager.Instance.uiManager == null)
        {
            Debug.LogError("GameManager, Game, or UIManager instance is null.");
            return;
        }

        if (GameManager.Instance.game.currentPlayer == null)
        {
            Debug.LogError("Current player is null.");
            return;
        }

        currentPlayerInfo = $"Current Player : {GameManager.Instance.game.currentPlayer.playerName}";
        diceResult = DiceNumberCheck.redDiceNumber + DiceNumberCheck.blueDiceNumber;
        Debug.Log($"{currentPlayerInfo}의 주사위 결과는 {diceResult}.");
        // 이하 UI 업데이트 로직 추가
    }

    public void ShowMessage(string message)
    {
        // 메세지를 화면에 표시하는 로직
        Debug.Log(message);
    }
}
