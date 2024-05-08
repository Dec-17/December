using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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

    public bool earthQuake = false;


    void Start()
    {
        // ī�޶��� �� �ʺ�� �� ���� ���
        Camera cam = Camera.main;
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cam.aspect * cameraHalfHeight;

        // ���� ��踦 BoxCollider2D�κ��� ������
        Bounds bounds = boxcol.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        // ���� ��踦 ����� �ʵ��� �ּ� �� �ִ� ���� ����
        minX = min.x + cameraHalfWidth;
        maxX = max.x - cameraHalfWidth;
        minY = min.y + cameraHalfHeight;
        maxY = max.y - cameraHalfHeight;

    }

    void LateUpdate()
    {
        // �÷��̾ �����ϵ��� ī�޶� ��ġ ����
        if (playerTransform != null && !earthQuake)
        {
            Vector3 targetPosition = playerTransform.position;
            targetPosition.z = transform.position.z; // ī�޶��� z �� ����
            transform.position = targetPosition;
        }

        // ī�޶� �� ��踦 ����� �ʵ��� ���� �׻��۵�
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
        earthQuake = true;  // ȭ�� ���� ����

        float shakeDuration = 10f; // ���� ���� �ð�
        float shakeMagnitude = 0.3f; // ���� ũ��

        // ���� ���� �ð� ���� �ݺ�
        float endTime = Time.time + shakeDuration;
        while (Time.time < endTime)
        {
            // ���� ī�޶� ��ġ
            Vector3 originalCameraPosition = playerTransform.position;

            // ������ ���� ũ�� �� ���� ���
            float offsetX = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);

            // ���ο� ī�޶� ��ġ ����
            transform.position = new Vector3(originalCameraPosition.x + offsetX, originalCameraPosition.y + offsetY, originalCameraPosition.z - 10f);

            // ���� ���ݸ�ŭ ���
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("ȭ�� ���� ��");

        // ȭ�� ������ ���� �� �ٽ� �÷��̾� ������ ����
        earthQuake = false;
    }

    IEnumerator earthQuakeEffect2()
    {
        earthQuake = true;  // ȭ�� ���� ����

        float shakeDuration = 2f; // ���� ���� �ð�
        float shakeMagnitude = 0.3f; // ���� ũ��

        // ���� ���� �ð� ���� �ݺ�
        float endTime = Time.time + shakeDuration;
        while (Time.time < endTime)
        {
            // ���� ī�޶� ��ġ
            Vector3 originalCameraPosition = playerTransform.position;

            // ������ ���� ũ�� �� ���� ���
            float offsetX = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);

            // ���ο� ī�޶� ��ġ ����
            transform.position = new Vector3(originalCameraPosition.x + offsetX, originalCameraPosition.y + offsetY, originalCameraPosition.z - 10f);

            // ���� ���ݸ�ŭ ���
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("ȭ�� ���� ��");

        // ȭ�� ������ ���� �� �ٽ� �÷��̾� ������ ����
        earthQuake = false;
    }
}