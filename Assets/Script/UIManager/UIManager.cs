using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TMP UI를 사용하기 위해 추가

public class UIManager : MonoBehaviour
{
    // PlayerInfoBG를 참조하는 오브젝트
    public GameObject player1InfoBG, player2InfoBG;
    public TextMeshProUGUI p1NameText, p2NameText, p1MoneyText, p2MoneyText;
    // 주사위 결과
    public int diceResult;

    public void InitializeUI()
    {
        // 플레이어 UI 정보 가져오기
        player1InfoBG = GameObject.Find("Player1InfoBG");
        p1NameText = player1InfoBG.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        p1MoneyText = player1InfoBG.transform.Find("Money").GetComponent<TextMeshProUGUI>();
        player2InfoBG = GameObject.Find("Player2InfoBG");
        p2NameText = player2InfoBG.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        p2MoneyText = player2InfoBG.transform.Find("Money").GetComponent<TextMeshProUGUI>();
        
        // 플레이어 정보 초기화 : UI text 설정, 플레이어 보유 머니 초기화
        p1NameText.text = $"플레이어 : {GameManager.Instance.game.players[0].playerName}";
        GameManager.Instance.game.players[0].money = 10000;
        p1MoneyText.text = $"보유현금 : {GameManager.Instance.game.players[0].money}";
        p2NameText.text = $"플레이어 : {GameManager.Instance.game.players[1].playerName}";
        GameManager.Instance.game.players[1].money = 10000;
        p2MoneyText.text = $"보유현금 : {GameManager.Instance.game.players[1].money}";
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

        // currentPlayerInfo = $"Current Player : {GameManager.Instance.game.currentPlayer.playerName}";
        diceResult = DiceNumberCheck.redDiceNumber + DiceNumberCheck.blueDiceNumber;
        // Debug.Log($"{currentPlayerInfo}의 주사위 결과는 {diceResult}.");
        // 이하 UI 업데이트 로직 추가
    }

    public void ShowMessage(string message)
    {
        // 메세지를 화면에 표시하는 로직
        Debug.Log(message);
    }
}
