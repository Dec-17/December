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
    [Header("위치값")]
    public Transform map01;
    public Transform map02;
    public Transform map03;
    public Transform map04;
    public Transform quiaMap01;
    public Transform quiaMap02;
    public Transform quiaMap03;
    public Transform quiaMap04;
    public Transform quiaMap05;

    [Header("카메라")]
    public GameObject cam;
    public CinemachineVirtualCamera virtualCamera; // CinemachineVirtualCamera 컴포넌트를 참조

    [Header("음악")]
    public AudioSource backgroundMusic; //기본
    public AudioSource dungeonMusic; //던전
    public AudioSource bossMusic; //보스전

    [Header("라이트 효과")]
    public GameObject lightBug;
    public Volume volume;
    public Vignette vignette;
    public Bloom bloom;

    public Collider2D[] col2d;
    CinemachineConfiner confiner;

    Dialog dialog;
    

    private void Start()
    {
        dialog = FindObjectOfType<Dialog>();

        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out bloom);

        confiner = cam.GetComponent<CinemachineConfiner>();
        if (confiner != null && col2d.Length > 0)
        {
            confiner.m_BoundingShape2D = col2d[0];
        }

        if (virtualCamera != null)
        {
            SetOrthographicSize(10f); //초기 카메라 사이즈
        }

        backgroundMusic.Play();
        dungeonMusic.Stop();
        bossMusic.Stop();
    }


    private void Update()
    {

    }

    public void SetBloomTint(float r, float g, float b)
    {
        if (bloom != null)
        {
            // RGB 값으로 Color 생성
            UnityEngine.Color color = new UnityEngine.Color(r, g, b);

            // Bloom Tint 속성 설정
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
        if (other.gameObject.name == "Map01") //동굴 입장
        {
            Vector3 newPosition = new Vector3(map02.position.x, map02.position.y + 2, map02.position.z);
            transform.position = newPosition;
            if (col2d.Length > 1)
            {
                confiner.m_BoundingShape2D = col2d[1];
                lightBug.SetActive(false);
                vignette.rounded.value = true;
                SetBloomTint(0.0f, 0.0f, 0.0f);
                SwitchDungeonMusic();
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
        else if (other.gameObject.name == "Map02") //동굴 나감
        {
            Vector3 newPosition = new Vector3(map01.position.x, map01.position.y - 1.6f, map01.position.z);
            transform.position = newPosition;
            if (col2d.Length > 0)
            {
                confiner.m_BoundingShape2D = col2d[0];
                lightBug.SetActive(true);
                vignette.rounded.value = false;
                SetBloomTint(17.6f, 17.6f, 66.7f);
                SwitchToBackgroundMusic();
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
        else if (other.gameObject.name == "Map03") //보스맵 입장
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
                SwitchToBossMusic();
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
        else if (other.gameObject.name == "Map04") //보스맵>동굴 나감
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
                SwitchDungeonMusic();
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
        else if (other.gameObject.name == "QuizMap01") //퀴즈 실패
        {
            Vector3 newPosition = new Vector3(quiaMap05.position.x, quiaMap05.position.y - 1.6f, quiaMap05.position.z);
            transform.position = newPosition;
            if (col2d.Length > 0)
            {
                confiner.m_BoundingShape2D = col2d[0];
                lightBug.SetActive(true);

                vignette.rounded.value = false;
                SetBloomTint(17.6f, 17.6f, 66.7f);

                StartCoroutine(WrongAnser());
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
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

                StartCoroutine(WrongAnser());
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
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

                StartCoroutine(WrongAnser());
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
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

                StartCoroutine(WrongAnser());
            }
            else
            {
                Debug.LogWarning("col2d 배열에 충분한 요소가 없습니다.");
            }
        }
    }

    IEnumerator WrongAnser()
    {
        //던전에서 쫒겨나는 애니메이션이나 이팩트 추가

        yield return new WaitForSeconds(0.6f);
        dialog.SetChapterNum(4);
        dialog.DialogueStart(); //대화 시작
    }

    private void SwitchToBackgroundMusic()
    {
        backgroundMusic.Play();
        dungeonMusic.Stop();
        bossMusic.Stop();
    }

    private void SwitchDungeonMusic()
    {
        backgroundMusic.Stop();
        dungeonMusic.Play();
        bossMusic.Stop();
    }

    private void SwitchToBossMusic()
    {
        backgroundMusic.Stop();
        dungeonMusic.Stop();
        bossMusic.Play();
    }

    public void respawnLight() //리스폰시 라이트 효과 초기화
    {
        confiner.m_BoundingShape2D = col2d[0];
        lightBug.SetActive(true);
        vignette.rounded.value = false;
        SetBloomTint(17.6f, 17.6f, 66.7f);
        SetOrthographicSize(virtualCamera.m_Lens.OrthographicSize = 10f);
    }
}