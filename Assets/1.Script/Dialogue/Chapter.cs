using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    Dialog dialog;


    void Start()
    {
        dialog = FindObjectOfType<Dialog>();
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그와 충돌 했을때
        {
            switch (gameObject.name) // 이 스크립트를 가진 오브젝트의 이름이
            {
                case "chapter00": // chapter00 이라면
                    Debug.Log("챕터0 시작");
                    dialog.SetChapterNum(0); // 다이얼로그 챕터를 0번으로 셋팅
                    dialog.DialogueStart(); // 대화 시작
                    Destroy(gameObject); // 대화가 한번만 실행되도록 오브젝트 파괴
                    break;
                case "chapter01":
                    Debug.Log("챕터1 시작");
                    dialog.SetChapterNum(1);
                    dialog.DialogueStart();
                    Destroy(gameObject);
                    break;
                case "chapter02":
                    Debug.Log("챕터2 시작");
                    dialog.SetChapterNum(2);
                    dialog.DialogueStart();
                    Destroy(gameObject);
                    break;
                case "chapter03":
                    Debug.Log("챕터3 시작");
                    dialog.SetChapterNum(3);
                    dialog.DialogueStart();
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("알 수 없는 chapter");
                    break;
            }
        }
    }
}