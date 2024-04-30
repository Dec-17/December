using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeBoss : MonoBehaviour
{
    //체력
    public float bossHP = 50f; //체력
    public float bossLeftHandHP = 30f; //왼손 체력
    public float bossRightHandHP = 30f; //오른손 체력

    private bool isPattern = false; //패턴 실행여부


    //에너지볼 패턴
    public GameObject energyBombPrefab; //에너지볼 프리팹
    public float energyBombSpeed = 20f; //속도
    private float ShootMin = 0.5f; //발사 최소 간격
    private float ShootMax = 1.5f; //발사 최대 간격
    private float energyCoolTime = 20f; //패턴 쿨타임
    private float energyDurationTime = 5f; //패턴 지속시간
    private float nextShootTime; //다음 발사 시간

    //몹 소환 패턴
    public GameObject spawnMonsterPrefab; //소환 할 몬스터 프리팹
    private float spawnTime = 30f; //패턴 쿨타임
    public int minSpawnCount = 1; //최소 소환 몬스터 수
    public int maxSpawnCount = 3; //최대 소환 몬스터 수

    //바닥 내려찍기


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

        nextShootTime = Time.time + Random.Range(ShootMin, ShootMax); //첫 발사 시간 설정
        StartCoroutine(SpawnMonsterPattern()); //패턴시작
        StartCoroutine(EnergyePattern()); //패턴 시작
    }

    void Update()
    {

    }

    IEnumerator EnergyePattern() //에너지볼 발사 패턴
    {
        while (true)
        {
            yield return new WaitForSeconds(energyCoolTime); //energyCoolTime만큼 대기

            isPattern = true; //패턴 실행

            float patternEndTime = Time.time + energyDurationTime; //패턴 시작
            while (Time.time < patternEndTime)
            {
                if (isPattern && Time.time >= nextShootTime) //패턴 실행 여부 확인 후 발사 시간이 되면 에너지볼 발사
                {
                    ShootEnergyBomb(); //에너지볼 발사
                    nextShootTime = Time.time + Random.Range(ShootMin, ShootMax); //다음 발사 시간 설정
                }
                yield return null;
            }
            isPattern = false; // 패턴 종료
        }
    }

    void ShootEnergyBomb() //에너지볼 발사
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //플레이어의 위치 찾기
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position; //플레이어의 위치를 타겟으로 설정
            targetPosition.y -= 1f; //플레이어 위치의 y좌표를 -1만큼 조정

            GameObject energyBomb = Instantiate(energyBombPrefab, transform.position, Quaternion.identity); //에너지 볼 생성
            Vector2 direction = (targetPosition - transform.position).normalized; //타겟을 향하는 방향 계산
            Rigidbody2D rb = energyBomb.GetComponent<Rigidbody2D>(); //에너지 볼에 힘을 가해 발사
            rb.velocity = direction * energyBombSpeed;
        }
    }
    IEnumerator SpawnMonsterPattern() //몬스터 소환 패턴
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime); //spawnTime만큼 대기

            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1); //생성할 몬스터 수 랜덤 설정

            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 spawnPosition = transform.position; //생성 위치를 해당 오브젝트의 위치로 설정
                spawnPosition.y += Random.Range(-5f, -40f); //Y 좌표를 랜덤하게 설정
                spawnPosition.x += Random.Range(-40f, 40f); //X 좌표를 랜덤하게 설정

                Instantiate(spawnMonsterPrefab, spawnPosition, Quaternion.identity); //몬스터 생성
            }
        }
    }
}
