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
    public GameObject redDice, blueDice; // 주사위 관련된 오브젝트2
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
        redDiceRoll.DiceRollEvent += DiceRollLock;
        // DiceNumberCheck 스트립트의 이벤트 구독
        // DiceNumberCheck.DiceNumberCheckEvent += DiceNumberOkay;
    }

    void OnDisable() 
    {
        // Game 스크립트의 이벤트 구독 해제
        Game.OnGameSetEvent -= InitializeGame;    
        // UIManager 스크립트의 이벤트 구독 해제
        UIManager.InitializeUIEvent -= HandleTurn;
        // UIManager.UpdateUIEvent -= ; 
        // DiceRoll 스트립트의 이벤트 구독 해제
        redDiceRoll.DiceRollEvent -= DiceRollLock;
        // DiceNumberCheck 스트립트의 이벤트 구독
        // DiceNumberCheck.DiceNumberCheckEvent -= DiceNumberOkay;
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
        // 게임 시작 처리
        game.StartGame();
        uiManager.InitializeUI();
    }

    public void HandleTurn()
    {

        // 1. 주사위 굴리기
        if (!rollButtonBG.activeSelf) // rollButtonBG가 비활성화면 실행
        {
            rollButtonBG.SetActive(true); // 주사위 굴리기 UI 활성화
        } 
        redDice.GetComponent<redDiceRoll>().enabled = true; // 주사위 굴리기 작용 활성화
        blueDice.GetComponent<blueDiceRoll>().enabled = true; 
        rollButton.GetComponent<Text>().text = "주사위를 굴리세요\n(Click or Space)"; // 주사위 굴리기 안내
        // 2. 주사위 굴린 후 굴리기 비활성화 : DiceRollEvent를 받아서 굴리지 못하게 비활성화
        
        

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

    public void DiceRollLock()
    {
        rollButtonBG.SetActive(false); // 주사위 굴리기 UI 비활성화
        redDice.GetComponent<redDiceRoll>().enabled = false; // 주사위 굴리기 작용 비활성화
        blueDice.GetComponent<blueDiceRoll>().enabled = false;
    }
}
