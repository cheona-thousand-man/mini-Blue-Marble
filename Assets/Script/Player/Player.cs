using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public string playerName; 
    private Vector3 playerPosition;
    public Tile playerNowTile;
    public Vector3 movePosition;
    public int money;
    public List<CountryTile> ownedTiles;

    // Move 함수 구현을 위한 변수
    public float moveSpeed = 100f; // 이동속도 (단위 : 게임 유닛/초)
    public float smoothTime = 0.1f; // 부드러움 조절 값
    // Animator 적용을 위한 변수
    public Animator animator;
    public bool isMoving = false;

    // 1칸식 이동할 때 마다 Event를 GameManager에 전달
    public static event Action PlayerMoveEndEvent;

    // Start is called before the first frame update
    void Start()
    {
        // 현재 위치를 초기화 해 두어야, 게임 시작하자마다 겹치도록 이동하지 않음
        playerName = gameObject.name;
        playerPosition = transform.position;
        movePosition = transform.position;

        // 애니메이션 초기화
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
    }

    void Update()
    {
        // 목표 위치가 지금 위치와 다른 경우 Move() 작동
        if (movePosition != playerPosition) 
        {
            Move(movePosition);
            Debug.Log("Move activated!");
        }
    }

    public void Move(Vector3 targetPosition)
    {
        // 목표 위치까지 거리 계산
        float distance = Vector3.Distance(playerPosition, targetPosition);

        // 이동 완료 시 스크립트 종료 & 플레이어 위치 갱신
        if (distance <= 0.1f)
        {
            playerPosition = movePosition;
            Debug.Log("Tile 1칸 이동을 마쳤습니다.");
            PlayerMoveEndEvent?.Invoke();
            return;
        }

        // 이동 방향 벡터 계산
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize(); // 단위벡터로 방향만 추출

        // smoothDamp 함수를 사용하여 현재 위치와 목표 위치 사이를 부드럽게 보간
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref direction, smoothTime, moveSpeed);

        // 이동 완료 후 위치 업데이트
        playerPosition = transform.position;
    }

    public void PurchaseTile(CountryTile tile, Player player)
    {
        if (tile.owner == null) // 빚져서도 구매 가능 | money >= tile.purchaseCost && tile.owner == null
        {
            money -= tile.purchaseCost; // 임시 방편으로 첫 객체의 금액 가져오기
            ownedTiles.Add(tile);
            tile.owner = player;
            Debug.Log($"{playerName} purchaed {tile.name}. Cost is {tile.purchaseCost}");
            GameManager.Instance.uiManager.UpdateUI(); // 줄어든 돈 UI 반영
            ownedTiles.Add(tile); // 구매한 Tile 소유목록에 추가
            foreach (Tile myTile in ownedTiles)
            {
                Debug.Log($"my{myTile.name} ");
            }
        }
        else
        {
            Debug.Log($"{playerName} cannot purchase {tile.name}");
        }
    }

    public void PayRent(int rentAmount)
    {
        GameManager.Instance.game.currentPlayer.money -= rentAmount;
        GameManager.Instance.uiManager.UpdateUI(); // 줄어든 돈 UI 반영
        Debug.Log($"{playerName} paid ${rentAmount} in rent.");
        
        // 요급 지불 UI
        GameManager.Instance.PayRentUIon();
        Invoke("PayRentUIoff", 2.0f);
        Invoke("ProcessTurnEnd", 0f); // 요금 지불 시 Country Tile 이벤트 종료하여 ProcessTurnEnd() 호출
    }

    public void DrawGoldenKey()
    {
        Debug.Log($"{playerName} drew a golden key!");
        // money += 1000; // 돈 지급 처리는 GameManager에서 처리
    }

    public void PayRentUIoff()
    {
        GameManager.Instance.PayRentUIoff();
    }

    public void ProcessTurnEnd()
    {
        GameManager.Instance.ProcessTurnEnd();
    }
}
