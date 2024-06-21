using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Game game;
    public UIManager uiManager;

    // 턴을 종료하기 위한 이벤트
    public static event Action TurnEndEvent;

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
    }

    void OnDisable() 
    {
        // Game 스크립트의 이벤트 구독 해제
        Game.OnGameSetEvent -= InitializeGame;    
    }

    void InitializeGame()
    {
        game = FindObjectOfType<Game>();
        uiManager = FindObjectOfType<UIManager>();
        if (game == null || uiManager == null)
        {
            Debug.LogError("Game or UIManager not found!");
            return;
        }
        game.StartGame();
        uiManager.InitializeUI();
    }

    public void HandleTurn()
    {
        // 주사위 굴리기
        
        // 플레이어 이동

        // 타일 이벤트 처리

        // 턴 종료
        game.NextTurn();
        uiManager.UpdateUI();
    }

    public void ProcessTileEvent(int tileIndex, Player player)
    {
        Tile tile = game.tiles[tileIndex];
        tile.OnLand(player);
        uiManager.UpdateUI();
    }
}
