using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;


public class MapManager : MonoBehaviour
{
    public Transform map01;
    public Transform map02;
    public Transform map03;
    public Transform map04;

    public Transform quiaMap01;
    public Transform quiaMap02;
    public Transform quiaMap03;
    public Transform quiaMap04;
    public Transform quiaMap05;

    public GameObject cam;
    public CinemachineVirtualCamera virtualCamera; // CinemachineVirtualCamera ������Ʈ�� ����

    public Collider2D[] col2d;
    CinemachineConfiner confiner;

    public GameObject lightBug;

    public Volume volume;
    public Vignette vignette;
    public Bloom bloom;

    private void Start()
    {
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out bloom);
        
        
        confiner = cam.GetComponent<CinemachineConfiner>();
        if (confiner != null && col2d.Length > 0)
        {
            confiner.m_BoundingShape2D = col2d[0];
        }

        if (virtualCamera != null)
        {
            SetOrthographicSize(10f); // �ʱ� Orthographic Size ����
        }
    }


    private void Update()
    {
        
    }

    public void SetBloomTint(float r, float g, float b)
    {
        if (bloom != null)
        {
            // RGB ������ Color ����
            UnityEngine.Color color = new UnityEngine.Color(r, g, b);

            // Bloom Tint �Ӽ� ����
            bloom.tint.value = color;
        }
    }

    void SetOrthographicSize(float newSize)
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.OrthographicSize = newSize;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Map01") //���� ����
        {
            Vector3 newPosition = new Vector3(map02.position.x, map02.position.y + 2, map02.position.z);
            transform.position = newPosition;
            if (col2d.Length > 1)
            {
                confiner.m_BoundingShape2D = col2d[1];
                lightBug.SetActive(false);
                vignette.rounded.value = true;
                SetBloomTint(0.0f, 0.0f, 0.0f);
            }
            else
            {
                Debug.LogWarning("col2d �迭�� ����� ��Ұ� �����ϴ�.");
            }
        }
        else if (other.gameObject.name == "Map02") //���� ����
        {
            Vector3 newPosition = new Vector3(map01.position.x, map01.position.y - 1.6f, map01.position.z);
            transform.position = newPosition;
            if (col2d.Length > 0)
            {
                confiner.m_BoundingShape2D = col2d[0];
                lightBug.SetActive(true);
                vignette.rounded.value = false;
                SetBloomTint(17.6f, 17.6f, 66.7f);
            }
            else
            {
                Debug.LogWarning("col2d �迭�� ����� ��Ұ� �����ϴ�.");
            }
        }
        else if (other.gameObject.name == "Map03") //������ ����
        {
            Vector3 newPosition = new Vector3(map04.position.x, map04.position.y + 2, map04.position.z);
            transform.position = newPosition;
            if (col2d.Length > 2)
            {
                confiner.m_BoundingShape2D = col2d[2];
                lightBug.SetActive(true);
                //15
                SetOrthographicSize(virtualCamera.m_Lens.OrthographicSize + 4.9f);
                vignette.rounded.value = false;
                SetBloomTint(17.6f, 17.6f, 66.7f);
            }
            else
            {
                Debug.LogWarning("col2d �迭�� ����� ��Ұ� �����ϴ�.");
            }
        }
        else if (other.gameObject.name == "Map04") //������ ����
        {
            Vector3 newPosition = new Vector3(map03.position.x, map03.position.y - 2, map03.position.z);
            transform.position = newPosition;
            if (col2d.Length > 1)
            {
                confiner.m_BoundingShape2D = col2d[1];
                lightBug.SetActive(false);
                //10
                SetOrthographicSize(virtualCamera.m_Lens.OrthographicSize - 4.9f);
                vignette.rounded.value = true;
                SetBloomTint(0.0f, 0.0f, 0.0f);
            }
            else
            {
                Debug.LogWarning("col2d �迭�� ����� ��Ұ� �����ϴ�.");
            }
        }
        else if (other.gameObject.name == "QuizMap01")
        {
            Vector3 newPosition = new Vector3(quiaMap05.position.x, quiaMap05.position.y - 1.6f, quiaMap05.position.z);
            transform.position = newPosition;
            if (col2d.Length > 0)
            {
                confiner.m_BoundingShape2D = col2d[0];
                lightBug.SetActive(true);
                
                vignette.rounded.value = false;
                SetBloomTint(17.6f, 17.6f, 66.7f);
            }
            else
            {
                Debug.LogWarning("col2d �迭�� ����� ��Ұ� �����ϴ�.");
            }
        }
        else if (other.gameObject.name == "QuizMap02")
        {
            Vector3 newPosition = new Vector3(quiaMap05.position.x, quiaMap05.position.y - 1.6f, quiaMap05.position.z);
            transform.position = newPosition;
            if (col2d.Length > 0)
            {
                confiner.m_BoundingShape2D = col2d[0];
                lightBug.SetActive(true);
                
                vignette.rounded.value = false;
                SetBloomTint(17.6f, 17.6f, 66.7f);
            }
            else
            {
                Debug.LogWarning("col2d �迭�� ����� ��Ұ� �����ϴ�.");
            }
        }
        else if (other.gameObject.name == "QuizMap03")
        {
            Vector3 newPosition = new Vector3(quiaMap05.position.x, quiaMap05.position.y - 1.6f, quiaMap05.position.z);
            transform.position = newPosition;
            if (col2d.Length > 0)
            {
                confiner.m_BoundingShape2D = col2d[0];
                lightBug.SetActive(true);
                
                vignette.rounded.value = false;
                SetBloomTint(17.6f, 17.6f, 66.7f);
            }
            else
            {
                Debug.LogWarning("col2d �迭�� ����� ��Ұ� �����ϴ�.");
            }
        }
        else if (other.gameObject.name == "QuizMap04")
        {
            Vector3 newPosition = new Vector3(quiaMap05.position.x, quiaMap05.position.y - 1.6f, quiaMap05.position.z);
            transform.position = newPosition;
            if (col2d.Length > 0)
            {
                confiner.m_BoundingShape2D = col2d[0];
                lightBug.SetActive(true);
                
                vignette.rounded.value = false;
                SetBloomTint(17.6f, 17.6f, 66.7f);
            }
            else
            {
                Debug.LogWarning("col2d �迭�� ����� ��Ұ� �����ϴ�.");
            }
        }
    }

    public void respawnLight()
    {
        confiner.m_BoundingShape2D = col2d[0];
        lightBug.SetActive(true);
        vignette.rounded.value = false;
        SetBloomTint(17.6f, 17.6f, 66.7f);
        SetOrthographicSize(virtualCamera.m_Lens.OrthographicSize = 10f);
    }
}