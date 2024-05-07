using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //[Header("")]
    public List<Transform> mapLocations = new List<Transform>(); // �� �̵� �������� ������ ����Ʈ

    private int currentMapIndex = 0; // ���� �� �ε���

    private void Start()
    {
        // �ʱ� �� ����
        MoveToNextMap();
    }

    private void MoveToNextMap()
    {
        if (currentMapIndex < mapLocations.Count)
        {
            // ���� �� ��ġ�� ���� �� ��ġ�� ����
            Transform nextMapLocation = mapLocations[currentMapIndex];
            GameObject.FindGameObjectWithTag("Player").transform.position = nextMapLocation.position;

            // ���� �� �ε��� ����
            currentMapIndex++;
        }
        else
        {
            Debug.Log("��� �� �̵��� �Ϸ�Ǿ����ϴ�.");
            // Ȥ�� ���ϴ� ������ ������ �� �ֽ��ϴ�.
        }
    }
}