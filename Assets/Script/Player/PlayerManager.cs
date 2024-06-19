using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private string playerName; 
    private Vector3 playerPosition, movePosition;
    private int money;
    private List<CountryTile> ownedTiles;

    // 이동속도 (단위 : 게임 유닛/초)
    public float moveSpeed = 5.0f

    // Start is called before the first frame update
    void Start()
    {
        playerName = gameObject.name;
        playerPosition = gameObject.transform.position;
        print(playerPosition);
    }

    void Update()
    {
        if (movePosition != null) // 목표 위치가 설정되어 있는경우
        {
            Move(movePosition);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        // 이동 방향 벡터 계산
        Vector3 direction = targetPosition - playerPosition;
        direction.Normalize(); // 단위벡터로 방향만 추출

        // 목표 위치까지 거리 계산
        float distance = Vector3.Distance(playerPosition, targetPosition);

        // 매 프레임 이동 거리 계산 (이동 속도 x 시간)
        float moveDistance = moveSpeed * Time.deltaTime;

        // 목표 위치까지 남은 거리가 이동 거리보다 작을 때
        if (distance <= moveDistance)
        {
            // 목표 위치로 이동
            playerPosition = targetPosition;
            transform.position = targetPosition;
            movePosition = null;
        }        
        else
        {
            // 현재 위치에서 이동 거리만큼 이동
            playerPosition += direction * moveDistance;
            transform.position = playerPosition;
        }

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
