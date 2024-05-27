using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUI 사용을 위해 추가

public class Quiz : MonoBehaviour
{
    [Header("범위")]
    public TextMeshProUGUI talkTrue; //대화 가능한 범위에 들어왔는지 표시할 텍스트
    public float talkLength = 1f; //대화 가능 범위
    private bool isInRange = false; //대화 가능 범위인지 확인

    void Start()
    {
        // 초기 상태 설정
        SetAlpha(talkTrue, 0f); // 처음에는 투명하게 설정
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player 태그를 가진 오브젝트와 충돌했을 때
        {
            Debug.Log("대화가능");
            // talkTrue 텍스트의 알파값을 1로 설정
            SetAlpha(talkTrue, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player 태그를 가진 오브젝트가 콜라이더를 벗어났을 때
        {
            Debug.Log("대화불가능");
            // talkTrue 텍스트의 알파값을 0으로 설정
            SetAlpha(talkTrue, 0f);
        }
    }

    // TextMeshProUGUI 컴포넌트의 알파값을 설정하는 메서드
    void SetAlpha(TextMeshProUGUI textMeshPro, float alpha)
    {
        Color color = textMeshPro.color;
        color.a = alpha;
        textMeshPro.color = color;
    }
}
