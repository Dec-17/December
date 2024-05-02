using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TreeBoss : MonoBehaviour
{
    //체력
    public float bossHP = 50f; //체력
    public float bossLeftHandHP = 30f; //왼손 체력
    public float bossRightHandHP = 30f; //오른손 체력

    //패턴 지속시간
    public float patenTime = 3f;

    //레이저 패턴
    public GameObject razerPrefab; //레이저 프리팹
    public GameObject razerWarningPrefab; //레이저 경고 프리팹

    //몹소환 패턴
    public GameObject spawnMonsterPrefab; //소환몹 프리팹
    public int minSpawnCount = 1; //최소 소환 몬스터 수
    public int maxSpawnCount = 3; //최대 소환 몬스터 수

    //탄막 패턴
    public GameObject energyBombPrefab; //탄막 프리팹
    public float energyBombSpeed = 20f; //탄막 속도

    //낙석 패턴
    public GameObject rockShotPrefab; //낙석 프리팹

    //스위치 변수
    bool isPatternRunning = false;

    Collider bossColliders;
    Animation bossAnimation;
    Sprite leftHandSprite;
    Sprite rightHandSprite;

    void Start()
    {
        bossColliders = GetComponent<Collider>();
        bossAnimation = GetComponent<Animation>();
        leftHandSprite = GetComponent<Sprite>();
        rightHandSprite = GetComponent<Sprite>();

        // 5초마다 패턴 호출을 시작
        StartCoroutine(RandomPatternCaller());
    }

    void Update()
    {

    }

    // 5초마다 패턴을 랜덤으로 호출하는 코루틴
    IEnumerator RandomPatternCaller()
    {
        while (true)
        {
            //bossAnimation.SetTrigger(""); //보스 기본 애니메이션
            yield return new WaitForSeconds(0f); //*초 대기

            if (!isPatternRunning) // 패턴이 실행 중이 아닐 때만 패턴을 호출
            {
                int patternNumber = Random.Range(1, 5); // 1에서 4까지 랜덤한 패턴 번호 생성

                switch (patternNumber)
                {
                    case 1:
                        StartCoroutine(RazerPattern());
                        break;
                    case 2:
                        StartCoroutine(MobSpawnPattern());
                        break;
                    case 3:
                        StartCoroutine(EnergyBombPattern());
                        break;
                    case 4:
                        StartCoroutine(RockShotPattern());
                        break;
                    default:
                        Debug.LogWarning("Invalid pattern number");
                        break;
                }
            }
        }
    }

    IEnumerator RazerPattern() //1.레이저 패턴
    {
        Debug.Log("1번");
        isPatternRunning = true;
        
        float patternEndTime = Time.time + 17f; //*초 동안 패턴 실행

        while (Time.time < patternEndTime)
        {
            
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; //플레이어의 현재 위치 가져오기

            Vector3 spawnPosition = playerPosition; //razerPrefab의 중심을 플레이어 위치에 맞춰 생성
            
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)); //rotation Z값 랜덤설정

           // yield return new WaitForSeconds(0.3f); //*초 딜레이

            GameObject razerWarning = Instantiate(razerWarningPrefab, spawnPosition, randomRotation); //레이저 경고 오브젝트 생성
            yield return new WaitForSeconds(0.5f); //*초 딜레이

            Destroy(razerWarning); //*초 후에 레이저 경고 파괴

            GameObject lazer = Instantiate(razerPrefab, spawnPosition, randomRotation); //레이저 오브젝트 생성
            
            Destroy(lazer, 2f); //*초 후에 레이저 파괴

            yield return new WaitForSeconds(0.1f); //*초마다 생성
        }

        // 레이저 패턴 애니메이션 종료
        isPatternRunning = false;
    }


    IEnumerator MobSpawnPattern() //2.몹소환 패턴
    {
        Debug.Log("2번");
        isPatternRunning = true; //패턴 실행중

        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1); //랜덤 수 설정
        
        for (int i = 0; i < spawnCount; i++) //몹을 랜덤 위치에 생성
        {
            float randomX = Random.Range(50f, 100f); //x축 랜덤 값
            
            float randomY = Random.Range(1.5f, -35f); //y축 랜덤 값
           
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f); //생성 위치 설정
            
            Instantiate(spawnMonsterPrefab, spawnPosition, Quaternion.identity); //몹 생성
        }

        yield return new WaitForSeconds(10f); //*초 만큼 대기

        isPatternRunning = false; //패턴 종료
    }

    IEnumerator EnergyBombPattern() //3.탄막 패턴
    {
        //bossAnimation.SetTrigger(""); //탄막 패턴 애니메이션
        isPatternRunning = true;
        Debug.Log("3번");
        yield return new WaitForSeconds(patenTime);
        isPatternRunning = false;
    }

    IEnumerator RockShotPattern() //4.낙석 패턴
    {
        //bossAnimation.SetTrigger(""); //낙석 패턴 애니메이션
        isPatternRunning = true;
        Debug.Log("4번");
        yield return new WaitForSeconds(patenTime);
        isPatternRunning = false;
    }

    void Die()
    {
        if(bossHP >= 0)
        {
            //bossAnimation.SetTrigger(""); //보스 사망 애니메이션
        }
    }
}
