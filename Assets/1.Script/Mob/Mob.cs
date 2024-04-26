using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float mobHP; //몹 오브젝트의 체력
    public GameObject[] NomalItem; //일반 아이템 배열
    public GameObject[] EpicItem; //희귀 아이템 배열
    public float NomalItemProbability = 0.85f; //일반 아이템 드랍 확률
    public float EpicItemProbability = 0.15f; //희귀 아이템 드랍 확률

    public Rigidbody2D mobRigidbody;
    public Animation mobAnimation;

    private void Awake()
    {
        mobRigidbody = GetComponent<Rigidbody2D>();
        mobAnimation = GetComponent<Animation>();
    }
    //public float mobSpeed = 3f; //몬스터 이동 속도
    //private float detectionRange = 20f; //몬스터가 플레이어를 감지할 범위
    void Start()
    {

    }

    virtual public void Test12()
    {
        Debug.Log("나는 몬스터의 상속인 입니다.");
    }

    void Update()
    {
        //PlayerController player = FindObjectOfType<PlayerController>(); //씬에서 플레이어 찾기

        //if (player != null)
        //{
        //    float distance = Vector3.Distance(transform.position, player.transform.position);

        //    if (distance <= detectionRange) //플레이어가 감지 범위 내에 있을 때 이동
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mobSpeed * Time.deltaTime);
        //    }
        //}
    }

    //void OnTriggerEnter2D(Collider2D other) //충돌처리
    //{
    //    if (other.CompareTag("Arrow")) //충돌이 일어난 오브젝트의 태그가 "Arrow"일떄
    //    {
    //        mobHP--; //오브젝트의 체력 -1

    //        if (mobHP <= 0) //체력이 0이하가 된다면
    //        {
    //            Destroy(gameObject); //오브젝트 파괴
    //            DropNomalItems(); //일반 아이템 드롭 메서드 실행
    //            DropEpicItems(); //희귀 아이템 드롭 메서드 실행
    //        }
    //    }
    //}

    void DropNomalItems() //일반 아이템 드롭 메서드
    {
        foreach (GameObject item in NomalItem) //NomalItem 배열만큼 반복
        {
            if (Random.value < NomalItemProbability) //NomalItemProbability 확률로 아이템을 드롭
            {
                Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f), //아이템 생성위치 랜덤지정
                                                    transform.position.y + Random.Range(-1f, 1f),
                                                    transform.position.z);
                Instantiate(item, spawnPosition, Quaternion.identity); //아이템을 생성
            }
        }
    }
    void DropEpicItems() //희귀 아이템 드롭 메서드
    {
        foreach (GameObject item in EpicItem) //EpicItem 배열만큼 반복
        {
            if (Random.value < EpicItemProbability) //EpicItemProbability 확률로 아이템을 드롭
            {
                Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f), //아이템 생성위치 랜덤지정
                                                    transform.position.y + Random.Range(-1f, 1f),
                                                    transform.position.z);
                Instantiate(item, spawnPosition, Quaternion.identity); //아이템을 생성
            }
        }
    }
}
