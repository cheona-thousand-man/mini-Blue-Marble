using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player currentPlayer;
    public List<Player> players;
    public List<Tile> tiles; 
    public int turnNumber;

    // GameManager가 실행되기 위한 이벤트
    public static event Action OnGameSetEvent;

    void OnEnable() 
    {
        // GameManager 스크립트의 이벤트 구독
        // GameManager.TurnEndEvent += void();    
    }

    void OnDisable() 
    {
        // GameManager 스크립트의 이벤트 구독 해제
        // GameManager.TurnEndEvent -= void();    
    }

    void Start()
    {
        StartCoroutine(WaitForAllPlayerActivated()); // 모든 Player 오브젝트가 활성화 될 때까지 대기 후 실행
    }

    void Update()
    {
        
    }

    IEnumerator WaitForAllPlayerActivated()
    {
        // 모든 Player 오브젝트가 활성화 될 때까지 대기
        yield return new WaitUntil(() => AllPlayerAreActivated());

        // 활성화된 모든 Tile 스크립트 가져오기
        Tile[] activeTiles = FindObjectsOfType<Tile>();
        // 가져온 Tile들을 tiles 리스트에 순서대로 추가
        getTiles(activeTiles);

        int index1 = 0;
        foreach(Tile tile in tiles) Debug.Log($"{++index1}번째 타일은 {tile.name}입니다.");
    
        // 활성화된 모든 Player 스크립트 가져오기
        Player[] activePlayers = FindObjectsOfType<Player>();
        // 가져온 Player들을 players 리스트에 무작위로 추가
        getPlayers(activePlayers);

        int index2 = 0;
        foreach(Player player in players) Debug.Log($"{++index2}번째 플레이어는 {player.playerName}입니다.");

        // 이제 게임 시작을 위한 GameManager 실행 이벤트 트리거
        OnGameSetEvent?.Invoke();
    }

    bool AllPlayerAreActivated()
    {
        // 모든 Player 오브젝트를 가져와서 활성화 여부 확인
        Player[] allPlayers = FindObjectsOfType<Player>();
        foreach (Player player in allPlayers)
        {
            if (!player.gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    public void StartGame()
    {
        // 게임 초기화 로직
        foreach (Player player in players) // 플레이어 게임 시작 머니 세팅
        {
            player.money = 10000;
            Debug.Log($"플레이어 {player.playerName}의 돈이 {player.money}로 초기화되었습니다.");
        }
        currentPlayer = players[0]; // 첫 번째 플레이어로 시작
        turnNumber = 0; // 첫 번째 턴으로 시작
        Debug.Log("Game started!");
    }

    public void EndGame()
    {
        // 게임 종료 로직
        Debug.Log("Game Ended!");
    }

    public void NextTurn()
    {
        // 다음 턴으로 진행하는 로직
        turnNumber++;
        int nextPlayerIndex = (players.IndexOf(currentPlayer) + 1) % players.Count;
        currentPlayer = players[nextPlayerIndex];
        Debug.Log($"Turn {turnNumber} : {currentPlayer.playerName}'s turn.");
    }

    public void CheckVictoryCondition()
    {
        // 승리 조건 확인 및 처리 로직
        foreach (Player player in players)
        {
            // 자산 기준으로 승리 조건을 확인하는 로직
            if (player.money >= 10000)
            {
                Debug.Log($"{player.playerName} wins the game!");
                EndGame();
                break;
            }
        }
    }

    private void getTiles(Tile[] activeTiles)
    {
        // 1번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "StartLand")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 2번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Tiwan")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 3번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "GoldenKeyLine1")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 4번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Turkey")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 5번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Greece")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 6번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Denmark")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 7번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "GoldenKeyLine2")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 8번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Canada")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 9번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Brazil")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 10번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Argentina")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 11번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "GoldenKeyLine3")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 12번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Spain")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 13번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Japan")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 14번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "France")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 15번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "USA")
            {
                tiles.Add(tile);
                break;
            }
        }
        // 16번 타일 추가
        foreach (Tile tile in activeTiles)
        {
            if (tile.name == "Korea")
            {
                tiles.Add(tile);
                break;
            }
        }
    }

    void getPlayers(Player[] activePlayers)
    {
        // 추가된 플레이어를 무작위 순서로 추가
        foreach (Player player in activePlayers)
        {
            players.Add(player);
        }
    }
}
