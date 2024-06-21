using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollButton : MonoBehaviour
{
    public redDiceRoll rDiceRoll;
    public blueDiceRoll bDiceRoll;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnButtonClick()
    {
        Debug.Log("주사위 버튼이 클릭되었습니다.");

        rDiceRoll.RollDice();
        bDiceRoll.RollDice();
    }
}
