using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // GameManage 위한 Game&UIManager 객체 관리
    public Game game;
    public UIManager uiManager;

    // GameManage 위한 GameObject 관리 
    public GameObject rollButtonBG, rollButton; // 주사위 관련된 오브젝트, Button의 경우 Inspector에서 직접 할당
    public GameObject redDice, blueDice, diceNumberCheck; // 주사위 관련된 오브젝트2
    public bool redDiceRollChecked = false;
    public bool blueDiceRollChecked = false;
    public bool diceOkay = false; // 주사위 숫자를 확인하여 이상이 있을 경우 재시도

    // Game&UIMnager와 순차 실행을 위한 이벤트
    public static event Action PlayerMoveEvent; // 플레이어가 이동하는 이벤트
    public static event Action TurnEndEvent; // 턴이 종료되면 다음 턴 진행

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 싱글턴 객체가 씬 전환 시 파괴되지 않도록 함
        }
        else
        {
            Destroy(gameObject); // 이미 싱글턴 인스턴스가 존재하면 파괴
        }
    }
    
    void OnEnable() 
    {
        // Game 스크립트의 이벤트 구독
        Game.OnGameSetEvent += InitializeGame;    
        // UIManager 스크립트의 이벤트 구독
        UIManager.InitializeUIEvent += HandleTurn;
        // UIManager.UpdateUIEvent += ; 
        // DiceRoll 스트립트의 이벤트 구독
        redDiceRoll.RedDiceRollEvent += RedDiceRollCheck;
        blueDiceRoll.BlueDiceRollEvent += BlueDiceRollCheck;
        // DiceNumberCheck 스트립트의 이벤트 구독
        DiceNumberCheck.DiceNumberCheckEvent += DiceNumberOkay;
    }

    void OnDisable() 
    {
        // Game 스크립트의 이벤트 구독 해제
        Game.OnGameSetEvent -= InitializeGame;    
        // UIManager 스크립트의 이벤트 구독 해제
        UIManager.InitializeUIEvent -= HandleTurn;
        // UIManager.UpdateUIEvent -= ; 
        // DiceRoll 스트립트의 이벤트 구독 해제
        redDiceRoll.RedDiceRollEvent -= RedDiceRollCheck;
        blueDiceRoll.BlueDiceRollEvent -= BlueDiceRollCheck;
        // DiceNumberCheck 스트립트의 이벤트 구독
        DiceNumberCheck.DiceNumberCheckEvent -= DiceNumberOkay;
    }

    void InitializeGame()
    {
        // GameManage에 쓰일 Game&UIManager 오브젝트 가져오기
        game = FindObjectOfType<Game>();
        uiManager = FindObjectOfType<UIManager>();
        if (game == null || uiManager == null)
        {
            Debug.LogError("Game or UIManager not found!");
            return;
        }
        // 주사위 오브젝트 가져오기
        redDice = GameObject.Find("RedDice");
        blueDice = GameObject.Find("BlueDice");
        // 주사위 숫자 점검 오브젝트 가져오기
        diceNumberCheck = GameObject.Find("BottomWall");
        // 게임 시작 처리
        game.StartGame();
        uiManager.InitializeUI();
    }

    public void HandleTurn()
    {
        if (!diceOkay) // 4.주사위 숫자 결과(3번)가 이상없을 때까지 던지기 반복
        {
            // 1. 주사위 굴리기
            if (diceNumberCheck.GetComponent<Collider>().enabled) // 주사위 숫자 체크를 비활성화 = 트리거 비활성화 
            {
                diceNumberCheck.GetComponent<Collider>().enabled = false;
            }
            if (!rollButtonBG.activeSelf) // rollButtonBG가 비활성화면 실행
            {
                rollButtonBG.SetActive(true); // 주사위 굴리기 UI 활성화
            } 
            redDice.GetComponent<redDiceRoll>().enabled = true; // 주사위 굴리기 작용 활성화
            blueDice.GetComponent<blueDiceRoll>().enabled = true; 
            rollButton.GetComponent<Text>().text = "주사위를 굴리세요\n(Click)"; // 주사위 굴리기 안내
            // 2. 주사위 굴린 후 굴리기 비활성화 : Red/BlueDiceRoll.Red/BlueDiceRollEvent를 받아서 굴리지 못하게 비활성화
            // 3. 주사위 숫자를 받아와서 이상 없는지 검사 : DiceNumberCheck.DiceNumberCheckEvent를 받아서 검사 실시
            return; // 반복 실행되는 것을 고려해서 함수 종료
        }
        
        
        

        // 플레이어 이동

        // 타일 이벤트 처리

        // 턴 종료
        // game.NextTurn();
        // uiManager.UpdateUI();
    }

    public void ProcessTileEvent(int tileIndex, Player player)
    {
        Tile tile = game.tiles[tileIndex];
        tile.OnLand(player);
        uiManager.UpdateUI();
    }

    public void RedDiceRollCheck()
    {
        redDiceRollChecked = true;
        TryExecuteDiceRollLock();
    }

    public void BlueDiceRollCheck()
    {
        blueDiceRollChecked = true;
        TryExecuteDiceRollLock();
    }

    public void TryExecuteDiceRollLock()
    {
        if (redDiceRollChecked && blueDiceRollChecked)
        {
            DiceRollLock();
            redDiceRollChecked = false;
            blueDiceRollChecked = false;
        }
    }

    public void DiceRollLock()
    {
        if (rollButtonBG.activeSelf) // rollButtonBG가 활성화면 실행
        {
            rollButtonBG.SetActive(false); // 주사위 굴리기 UI 비활성화
        }     
        redDice.GetComponent<redDiceRoll>().enabled = false; // 주사위 굴리기 작용 비활성화
        blueDice.GetComponent<blueDiceRoll>().enabled = false;

        // 주사위가 굴려 졌으므로, 주사위 숫자 체크를 활성화
        if (!diceNumberCheck.GetComponent<Collider>().enabled) // 주사위 숫자 체크를 활성화
        {
            diceNumberCheck.GetComponent<Collider>().enabled = true;
        }
    }

    public void DiceNumberOkay()
    {
        // 주사위 숫자가 모두 확인 되었으므로, 주사위 숫자 체크를 비활성화 = 트리거 비활성화 
        if (diceNumberCheck.GetComponent<Collider>().enabled)
        {
            diceNumberCheck.GetComponent<Collider>().enabled = false;
        }
        // 확인된 숫자 점검
        if (DiceNumberCheck.redDiceNumber != 0 && DiceNumberCheck.blueDiceNumber != 0)
        {
            Debug.Log("주사위 숫자가 정상적으로 감지 되었습니다.");
            diceOkay = true;
            return; // 주사위가 정상이므로 종료
        }
        // 주사위 값이 비정상일 경우 : 다시 주사위 굴려야 함을 알림UI
        if (!rollButtonBG.activeSelf) // rollButtonBG가 비활성화면 실행
        {
            rollButtonBG.SetActive(true); // 주사위 굴리기 UI 활성화
        } 
        rollButton.GetComponent<Text>().text = "주사위가 잘못 던져 졌습니다\n다시 던지세요"; // 주사위 다시굴리기 안내
        Invoke("HandleTurn", 2.0f); // 해당 문구를 2초간 보여줌
    }
}
