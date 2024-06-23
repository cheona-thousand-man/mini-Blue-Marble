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
    public GameObject rollButtonBG, rollButton, reRollUI; // 주사위 관련된 오브젝트, Button의 경우 Inspector에서 직접 할당
    public GameObject redDice, blueDice, diceNumberCheck; // 주사위 관련된 오브젝트2
    public bool redDiceRollChecked = false;
    public bool blueDiceRollChecked = false;
    public bool diceOkay = false; // 주사위 숫자를 확인하여 이상이 있을 경우 재시도
    public int targetTileIndex, remainIndex, movedIndex; // 플레이어 목적지 타일, 아직 남은 수, 이동한 수
    public bool moveYet = false; // 플레이어 이동처리 시작하였는지 확인
    public GameObject getSalaryUI; // Tile 관련된 오브젝트, Inspector에서 직접 할당

    // Game&UIMnager와 순차 실행을 위한 이벤트
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
        // Player 스크립트의 이벤트 구독
        Player.PlayerMoveEndEvent += PlayerMoveOneTile;
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
        // DiceNumberCheck 스트립트의 이벤트 구독 해제
        DiceNumberCheck.DiceNumberCheckEvent -= DiceNumberOkay;
        // Player 스크립트의 이벤트 구독 해제
        Player.PlayerMoveEndEvent -= PlayerMoveOneTile;
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
        // 주사위 굴리기
        if (!diceOkay) // 4.주사위 숫자 결과(3번)가 이상없을 때까지 던지기 반복
        {
            if (reRollUI.activeSelf) // reRollUI 비활성화 하기
            {
                reRollUI.SetActive(false);
            }

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
        if (!moveYet) // 이동처리를 시작했는지 확인, 시작했으면 통과
        {
            moveYet = true;
             // 1. 플레이어 이동 처리
            targetTileIndex = (game.tiles.IndexOf(game.currentPlayer.playerNowTile) + DiceNumberCheck.redDiceNumber + DiceNumberCheck.blueDiceNumber) % game.tiles.Count;
            Debug.Log($"플레이어가 이동할 Tile : {game.tiles[targetTileIndex].name}");
            // 2-1. 이동해야 할 타일 수 계산 
            if ((targetTileIndex - game.tiles.IndexOf(game.currentPlayer.playerNowTile)) > 0) // 이동해야 하는 타일 갯수 확인
            {
                remainIndex = targetTileIndex - game.tiles.IndexOf(game.currentPlayer.playerNowTile);
            }
            else
            {
                remainIndex = game.tiles.Count - game.tiles.IndexOf(game.currentPlayer.playerNowTile) + targetTileIndex;
            }
            movedIndex = 1; // 2-2. 이동을 확인하기 위한 초기화
            // 2-3. 플레이어 애니메이션 전환(idle->run)
            if (!game.currentPlayer.isMoving)
            {
                game.currentPlayer.isMoving = true;
                game.currentPlayer.animator.SetBool("isRunning", true);
            }
            // 3. 최초로 1개 타일 이동하고, 플레이어가 이동할 때마다 PlayerMoveEndEvent를 통해 PlayerMoveOneTile()로 한칸씩 이동
            game.currentPlayer.movePosition = game.tiles[(game.tiles.IndexOf(game.currentPlayer.playerNowTile) + 1) % game.tiles.Count].transform.position;
        }

        // 타일 이벤트 처리
        ProcessTileEvent(targetTileIndex, game.currentPlayer);

        // 턴 종료
        // game.NextTurn();
        // uiManager.UpdateUI();

        diceOkay = false; // 다른 플레이어를 위해 초기화
        moveYet = false;
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
            Invoke("HandleTurn", 0f); // 주사위가 정상이므로 바로 HandleTurn 호출
            return; // 주사위가 정상이므로 종료
        }

        // 주사위 값이 비정상일 경우 : 다시 주사위 굴려야 함을 알림UI
        if (!reRollUI.activeSelf) // reRollUI가 비활성화면 실행
        {
            reRollUI.SetActive(true);
        }
        Invoke("HandleTurn", 2.0f); // 해당 문구를 2초간 보여주고 HandleTurn 호출
    }

    public void PlayerMoveOneTile()
    {
        --remainIndex;
        Debug.Log($"이동해야 할 Tile 수 : {remainIndex}");

        // 플레이어가 1칸 이동한 이후부터 Colldier 활성화
        if (!game.currentPlayer.GetComponent<CapsuleCollider>().enabled)
        {
            game.currentPlayer.GetComponent<CapsuleCollider>().enabled = true;
        }

        // 모서리 Tile일 경우 플레이어 방향 90도 회전
            switch (game.tiles[(game.tiles.IndexOf(game.currentPlayer.playerNowTile) + movedIndex) % game.tiles.Count].name)
            {
                case "Greece" :
                    game.currentPlayer.transform.rotation *= Quaternion.Euler(0f, 90f, 0f);
                    break;
                case "Brazil" :
                    game.currentPlayer.transform.rotation *= Quaternion.Euler(0f, 90f, 0f);
                    break;
                case "Japan" :
                    game.currentPlayer.transform.rotation *= Quaternion.Euler(0f, 90f, 0f);
                    break;
                case "StartLand" :
                    game.currentPlayer.transform.rotation *= Quaternion.Euler(0f, 90f, 0f);
                    break;
                default :
                    break;
            }

        // 플레이어가 시작타일을 지날 경우 월급 500 지급
        if (game.tiles[(game.tiles.IndexOf(game.currentPlayer.playerNowTile) + movedIndex) % game.tiles.Count].name == "StartLand")
        {
            game.currentPlayer.money += 500;
            uiManager.UpdateUI(); // UI에 변경된 Money 반영
            if (!getSalaryUI.activeSelf) // getSalaryUI가 비활성화면 실행
            {
                getSalaryUI.SetActive(true); // 월급 보상 UI 활성화
            } 
            Invoke("getSalaryUIoff", 2.0f);
        }

        // 플레이어 이동 처리
        if (remainIndex > 0)
        {
            game.currentPlayer.movePosition = game.tiles[(game.tiles.IndexOf(game.currentPlayer.playerNowTile) + ++movedIndex) % game.tiles.Count].transform.position;
        }
        else
        {
            // 목표지점에 도달하여 플레이어 위치 타일 갱신
            game.currentPlayer.playerNowTile = game.tiles[targetTileIndex];
            Debug.Log($"이동을 마치고 목표지점 {game.tiles[targetTileIndex].name}에 도착했습니다.");

            // 목표 지점에 도착하여 플레이어 Collider 비활성화
            if (game.currentPlayer.GetComponent<CapsuleCollider>().enabled)
            {
                game.currentPlayer.GetComponent<CapsuleCollider>().enabled = false;
            }

            // 이동이 종료되어 애니메이션 복구(run->idle)
            game.currentPlayer.isMoving = false;
            game.currentPlayer.animator.SetBool("isRunning", false);

            Invoke("HandleTurn", 0f); // 이동 종료하여 바로 HandleTurn 호출
            return; 
        }
    }

    public void getSalaryUIoff()
    {
        if (getSalaryUI.activeSelf) // getSalaryUI가 활성화면 실행
        {
            getSalaryUI.SetActive(false); // 월급 보상 UI 비활성화
        } 
    }
}
