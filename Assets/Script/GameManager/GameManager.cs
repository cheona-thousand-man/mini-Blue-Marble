using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Game game;
    public UIManager uiManager;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        StartCoroutine(WaitForGameSettingEnded());
    }

    IEnumerator WaitForGameSettingEnded()
    {
        // Game 오브젝트의 이벤트가 발생할 때까지 대기
    }
    void OnGameSetEnded()
    {
        game = FindObjectOfType<Game>();
        game.StartGame();
        // uiManager.UpdateUI();
    }

    public void HandleTurn()
    {
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
