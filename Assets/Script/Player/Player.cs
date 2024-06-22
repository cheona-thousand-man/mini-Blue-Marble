using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public string playerName; 
    private Vector3 playerPosition;
    public Vector3 movePosition;
    public int money;
    public List<CountryTile> ownedTiles;

    // Move 함수 구현을 위한 변수
    public float moveSpeed = 5.0f; // 이동속도 (단위 : 게임 유닛/초)
    public float smoothTime = 0.3f; // 부드러움 조절 값

    // Start is called before the first frame update
    void Start()
    {
        playerName = gameObject.name;
        playerPosition = transform.position;
        movePosition = transform.position;
    }

    void Update()
    {
        // 목표 위치가 설정되어 있는경우 = 현 위치-목표 위치 거리가 있을 경우(>0.1f) Move() 작동
        if (movePosition != null && (Vector3.Distance(playerPosition, movePosition) > 0.1f)) 
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
        if (distance < 0.01f)
        {
            playerPosition = transform.position;
            return;
        }

        // 이동 방향 벡터 계산
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize(); // 단위벡터로 방향만 추출

        // smoothDamp 함수를 사용하여 현재 위치와 목표 위치 사이를 부드럽게 보간
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref direction, smoothTime, moveSpeed);
    }

    public void PurchaseTile(CountryTile tile)
    {
        if (money >= tile.purchaseCost && tile.owner == null)
        {
            money -= tile.purchaseCost;
            ownedTiles.Add(tile);
            tile.owner = this;
            Debug.Log($"{playerName} purchaed {tile.name}.");
        }
        else
        {
            Debug.Log($"{playerName} cannot purchase {tile.name}");
        }
    }

    public void PayRent(int rentAmount)
    {
        money -= rentAmount;
        Debug.Log($"{playerName} paid ${rentAmount} in rent.");
    }

    public void DrawGoldenKey()
    {
        Debug.Log($"{playerName} drew a golden key!");
    }
}
