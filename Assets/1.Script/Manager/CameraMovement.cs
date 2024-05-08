using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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

    public bool earthQuake = false;


    void Start()
    {
        // 카메라의 반 너비와 반 높이 계산
        Camera cam = Camera.main;
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cam.aspect * cameraHalfHeight;

        // 맵의 경계를 BoxCollider2D로부터 가져옴
        Bounds bounds = boxcol.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        // 맵의 경계를 벗어나지 않도록 최소 및 최대 값을 설정
        minX = min.x + cameraHalfWidth;
        maxX = max.x - cameraHalfWidth;
        minY = min.y + cameraHalfHeight;
        maxY = max.y - cameraHalfHeight;

    }

    void LateUpdate()
    {
        // 플레이어를 추적하도록 카메라 위치 설정
        if (playerTransform != null && !earthQuake)
        {
            Vector3 targetPosition = playerTransform.position;
            targetPosition.z = transform.position.z; // 카메라의 z 값 유지
            transform.position = targetPosition;
        }

        // 카메라가 맵 경계를 벗어나지 않도록 제한 항상작동
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;

    }

    public void earthQuakeStart()
    {
        StartCoroutine(earthQuakeEffect());
    }

    public void earthQuakeStart2()
    {
        StartCoroutine(earthQuakeEffect2());
    }

    IEnumerator earthQuakeEffect()
    {
        earthQuake = true;  // 화면 진동 시작

        float shakeDuration = 10f; // 진동 지속 시간
        float shakeMagnitude = 0.3f; // 진동 크기

        // 진동 지속 시간 동안 반복
        float endTime = Time.time + shakeDuration;
        while (Time.time < endTime)
        {
            // 현재 카메라 위치
            Vector3 originalCameraPosition = playerTransform.position;

            // 랜덤한 진동 크기 및 방향 계산
            float offsetX = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);

            // 새로운 카메라 위치 설정
            transform.position = new Vector3(originalCameraPosition.x + offsetX, originalCameraPosition.y + offsetY, originalCameraPosition.z - 10f);

            // 진동 간격만큼 대기
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("화면 진동 끝");

        // 화면 진동이 끝난 후 다시 플레이어 추적을 시작
        earthQuake = false;
    }

    IEnumerator earthQuakeEffect2()
    {
        earthQuake = true;  // 화면 진동 시작

        float shakeDuration = 2f; // 진동 지속 시간
        float shakeMagnitude = 0.3f; // 진동 크기

        // 진동 지속 시간 동안 반복
        float endTime = Time.time + shakeDuration;
        while (Time.time < endTime)
        {
            // 현재 카메라 위치
            Vector3 originalCameraPosition = playerTransform.position;

            // 랜덤한 진동 크기 및 방향 계산
            float offsetX = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);

            // 새로운 카메라 위치 설정
            transform.position = new Vector3(originalCameraPosition.x + offsetX, originalCameraPosition.y + offsetY, originalCameraPosition.z - 10f);

            // 진동 간격만큼 대기
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("화면 진동 끝");

        // 화면 진동이 끝난 후 다시 플레이어 추적을 시작
        earthQuake = false;
    }
}