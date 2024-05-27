using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    public Transform map01;
    public Transform map02;
    public Transform map03;
    public Transform map04;

    public GameObject cam;
    public Collider2D[] col2d;
    CinemachineConfiner confiner;

    public GameObject lightBug;

    private void Start()
    {
        confiner = cam.GetComponent<CinemachineConfiner>();
        if (confiner != null && col2d.Length > 0)
        {
            confiner.m_BoundingShape2D = col2d[0];
        }
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Map01")
        {
            Vector3 newPosition = new Vector3(map02.position.x, map02.position.y + 2, map02.position.z);
            transform.position = newPosition;
            if (col2d.Length > 1)
            {
                confiner.m_BoundingShape2D = col2d[1];
                lightBug.SetActive(false);
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
        else if (other.gameObject.name == "Map02")
        {
            Vector3 newPosition = new Vector3(map01.position.x, map01.position.y - 1.6f, map01.position.z);
            transform.position = newPosition;
            if (col2d.Length > 0)
            {
                confiner.m_BoundingShape2D = col2d[0];
                lightBug.SetActive(true);
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
        else if (other.gameObject.name == "Map03")
        {
            Vector3 newPosition = new Vector3(map04.position.x, map04.position.y + 2, map04.position.z);
            transform.position = newPosition;
            if (col2d.Length > 2)
            {
                confiner.m_BoundingShape2D = col2d[2];
                lightBug.SetActive(true);
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
        else if (other.gameObject.name == "Map04")
        {
            Vector3 newPosition = new Vector3(map03.position.x, map03.position.y - 2, map03.position.z);
            transform.position = newPosition;
            if (col2d.Length > 1)
            {
                confiner.m_BoundingShape2D = col2d[1];
                lightBug.SetActive(false);
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
    }
}