using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform; //�÷��̾��� Transform�� ������ ����
    public BoxCollider2D boxcol;
   
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    void Start()
    {
        // ī�޶��� �� �ʺ�� �� ���� ���
        //Camera cam = Camera.main;
        //cameraHalfHeight = cam.orthographicSize;
        //cameraHalfWidth = cam.aspect * cameraHalfHeight;

        // ���� ��踦 BoxCollider2D�κ��� ������
        //Bounds bounds = boxcol.bounds;
        //Vector3 min = bounds.min;
        //Vector3 max = bounds.max;

        // ���� ��踦 ����� �ʵ��� �ּ� �� �ִ� ���� ����
        //minX = min.x + cameraHalfWidth;
        //maxX = max.x - cameraHalfWidth;
        //minY = min.y + cameraHalfHeight;
        //maxY = max.y - cameraHalfHeight;
    }

    void LateUpdate()
    {
        // �÷��̾ �����ϵ��� ī�޶� ��ġ ����
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position;
            targetPosition.z = transform.position.z; // ī�޶��� z �� ����
            transform.position = targetPosition;
        }

        // ī�޶� �� ��踦 ����� �ʵ��� ����
        //Vector3 clampedPosition = transform.position;
        //clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        //clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        //transform.position = clampedPosition;
    }
}