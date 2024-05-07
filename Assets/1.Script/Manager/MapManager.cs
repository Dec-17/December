using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //[Header("")]
    public List<Transform> mapLocations = new List<Transform>(); // 맵 이동 지점들을 저장할 리스트

    private int currentMapIndex = 0; // 현재 맵 인덱스

    private void Start()
    {
        // 초기 맵 설정
        MoveToNextMap();
    }

    private void MoveToNextMap()
    {
        if (currentMapIndex < mapLocations.Count)
        {
            // 현재 맵 위치를 다음 맵 위치로 변경
            Transform nextMapLocation = mapLocations[currentMapIndex];
            GameObject.FindGameObjectWithTag("Player").transform.position = nextMapLocation.position;

            // 다음 맵 인덱스 증가
            currentMapIndex++;
        }
        else
        {
            Debug.Log("모든 맵 이동이 완료되었습니다.");
            // 혹은 원하는 동작을 수행할 수 있습니다.
        }
    }
}