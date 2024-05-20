using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject statue01;
    public GameObject statue02;
    public GameObject statue03;
    public GameObject statue04;

    private int currentStatueIndex = 0; // 현재 충돌이 예상되는 석상의 인덱스
    private GameObject[] statues; // 석상 오브젝트 배열

    void Start()
    {
        // 석상 오브젝트 배열 초기화
        statues = new GameObject[] { statue01, statue02, statue03, statue04 };
    }

    void Update()
    {
        // 필요 시 업데이트 로직 추가
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            if (currentStatueIndex < statues.Length && statues[currentStatueIndex] == other.gameObject)
            {
                currentStatueIndex++;
                if (currentStatueIndex == statues.Length)
                {
                    Debug.Log("암호:1234");
                    // 암호가 올바르게 입력되었을 때 추가 작업 수행
                }
            }
            else
            {
                // 순서가 잘못되었을 때 초기화
                currentStatueIndex = 0;
                Debug.Log("순서가 잘못되었습니다. 다시 시도하십시오.");
            }
        }
    }
}
