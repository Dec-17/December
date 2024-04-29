using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mob : MonoBehaviour
{
    public float mobHP; //몹 오브젝트의 체력
    public GameObject[] NomalItem; //일반 아이템 배열
    public GameObject[] EpicItem; //희귀 아이템 배열
    public float NomalItemProbability; //일반 아이템 드랍 확률
    public float EpicItemProbability; //희귀 아이템 드랍 확률    // 확률 1 = 100%

    public float mobSpeed; //몬스터 이동 속도
    public float detectionRange; //몬스터가 플레이어를 감지할 범위     //20으로 하면 화면에 보일때만 따라옴

    public float damageColorDuration = 0.2f; //피격 시 스프라이트 색상이 변경되는 시간
    public Color damageColor = new Color(1f, 0.5f, 0.5f); //피격 시 변경될 스프라이트가 색상
    public Color originalColor = new Color(1f, 1f, 1f); //몹의 기본 스프라이트 색상

    PlayerController playerController;
    public Rigidbody2D mobRigidbody;
    public Animation mobAnimation;
    SpriteRenderer mobRenderer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        mobRigidbody = GetComponent<Rigidbody2D>();
        mobAnimation = GetComponent<Animation>();
        mobRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        
    }
  
    virtual public void Testvirtual()
    {
        Debug.Log("오버라이드 테스트");
    }

    void Update()
    {
        FindPlayer();
    }

    public void FindPlayer() //플레이어 따라감
    {
        PlayerController player = FindObjectOfType<PlayerController>(); //씬에서 플레이어 찾기

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionRange) //플레이어가 감지 범위 내에 있을 때 이동
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mobSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) //충돌처리
    {
        if (other.CompareTag("Arrow")) //충돌이 일어난 오브젝트의 태그가 "Arrow"일떄
        {
            mobHP--; //playerController.playerATK; //HP감소



            if (mobHP <= 0) //체력이 0이하가 된다면
            {
                StartCoroutine(DestroyWithFade()); //서서히 투명해지고 파괴
            }
            else //체력이 0이하가 아니라면
            {
                StartCoroutine(FlashDamageColor()); //피격 시 일시적으로 스프라이트 색상 변경
            }
        }
    }

    IEnumerator FlashDamageColor()
    {
        mobRenderer.color = damageColor; //색상 변경
        yield return new WaitForSeconds(damageColorDuration); //damageColorDuration시간 동안 대기
        mobRenderer.color = originalColor; //원래 색상으로 되돌림
    }

    IEnumerator DestroyWithFade()
    {
        float fadeDuration = 0.5f; //서서히 사라지는 시간
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float startAlpha = renderer.color.a;

        //알파값 서서히 감소
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            Color newColor = renderer.color;
            newColor.a = Mathf.Lerp(startAlpha, 0.0f, t / fadeDuration);
            renderer.color = newColor;
            yield return null;
        }
        Destroy(gameObject); //오브젝트 파괴
        DropNomalItems(); //일반 아이템 드롭 메서드 실행
        DropEpicItems(); //희귀 아이템 드롭 메서드 실행
    }

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
