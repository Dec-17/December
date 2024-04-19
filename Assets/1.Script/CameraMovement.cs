using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform; //플레이어의 Transform을 저장할 변수

    public Vector3 offset = new Vector3(0f, 0f, -1f); //카메라와 플레이어 사이의 거리를 조절할 오프셋

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset; //플레이어의 위치에 오프셋을 더하여 카메라의 위치를 결정
        }
    }
}
