using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; // TMP UI를 사용하기 위해 추가

public class UIManager : MonoBehaviour
{
    // PlayerInfoBG를 참조하는 오브젝트
    public GameObject player1InfoBG, player2InfoBG;
    public TextMeshProUGUI p1NameText, p2NameText, p1MoneyText, p2MoneyText;
    // Turn 정보를 저장하는 오브젝트
    public GameObject turnPanel, turnNumber; // 해당 오브젝트는 기본 비활성화 상태로, 유니티 inspector에서 직접 할당

    // GameManager와의 순차 실행을 위한 Event
    public static event Action InitializeUIEvent; // UI 초기화 후 GameManager 호출

    void OnEnable() 
    {
        // GameManager 스크립트의 이벤트 구독
        // GameManager.PlayerMoveEvent += ; 
    }

    void OnDisable() 
    {
        // GameManager 스크립트의 이벤트 구독 해제
        // GameManager.PlayerMoveEvent += ;  
    }

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

        // 게임 시작에 따라 턴 정보 표시
        turnPanel.SetActive(true);
        turnNumber.SetActive(true);

        // 초기화 완료되어 턴:1 을 실행하기 위한 이벤트 발생
        InitializeUIEvent?.Invoke();
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
        // 이하 UI 업데이트 로직 추가

        // 플레이어 정보 업데이트 : 플레이어 보유 머니
        p1MoneyText.text = $"보유현금 : {GameManager.Instance.game.players[0].money}";
        p2MoneyText.text = $"보유현금 : {GameManager.Instance.game.players[1].money}";
    }

    public void ShowMessage(string message)
    {
        // 메세지를 화면에 표시하는 로직
        Debug.Log(message);
    }

    public void EndTurnUI()
    {

    }
}
