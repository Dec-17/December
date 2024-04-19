using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform; //�÷��̾��� Transform�� ������ ����

    public Vector3 offset = new Vector3(0f, 0f, -1f); //ī�޶�� �÷��̾� ������ �Ÿ��� ������ ������

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset; //�÷��̾��� ��ġ�� �������� ���Ͽ� ī�޶��� ��ġ�� ����
        }
    }
}
