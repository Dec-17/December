using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform; //플레이어의 Transform을 저장할 변수
    public BoxCollider2D boxcol;
   
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    void Start()
    {
        // 카메라의 반 너비와 반 높이 계산
        //Camera cam = Camera.main;
        //cameraHalfHeight = cam.orthographicSize;
        //cameraHalfWidth = cam.aspect * cameraHalfHeight;

        // 맵의 경계를 BoxCollider2D로부터 가져옴
        //Bounds bounds = boxcol.bounds;
        //Vector3 min = bounds.min;
        //Vector3 max = bounds.max;

        // 맵의 경계를 벗어나지 않도록 최소 및 최대 값을 설정
        //minX = min.x + cameraHalfWidth;
        //maxX = max.x - cameraHalfWidth;
        //minY = min.y + cameraHalfHeight;
        //maxY = max.y - cameraHalfHeight;
    }

    void LateUpdate()
    {
        // 플레이어를 추적하도록 카메라 위치 설정
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position;
            targetPosition.z = transform.position.z; // 카메라의 z 값 유지
            transform.position = targetPosition;
        }

        // 카메라가 맵 경계를 벗어나지 않도록 제한
        //Vector3 clampedPosition = transform.position;
        //clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        //clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        //transform.position = clampedPosition;
    }
}