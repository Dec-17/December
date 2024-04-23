using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f; //플레이어 이동 속도
    public float playerRunSpeed = 10; //플레이어 달리기 속도
    public float dashDistance = 3f; //플레이어 대쉬 거리

    public float maxPlayerHP = 10; //플레이어 최대 체력
    public float playerHP = 10; //플레이어 체력
    public Slider healthSlider; //플레이어 체력 슬라이더

    public float maxPlayerSP = 10; //플레이어 최대 스테미너
    public float playerSP = 10; //플레이어 스테미너
    public Slider staminaSlider; //플레이어 스테미너 슬라이더

    private bool isAttack = false; //플레이어가 공격중인지 확인하는 변수

    Animator animator;
    SpriteRenderer spriteRenderer;
    public float moveHorizontal;
    public float moveVertical;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //플레이어 이동 값
        moveHorizontal = Input.GetAxis("Horizontal");
       
        moveVertical = Input.GetAxis("Vertical");

        //플레이어 이동 속도 설정
        float speed = playerSpeed;

        //플레이어 달리기
        if (isAttack == false) //공격중이 아닐 경우
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))//쉬프트를 누르고 있다면
            {

                speed = playerRunSpeed; //플레이어 속도를 RunSpeed로 변경
            }
        }

        //플레이어 대쉬
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //대쉬 애니메이션 넣어야함**********
            Vector3 dashDirection = new Vector3(moveHorizontal, moveVertical, 0f).normalized; //대쉬 방향 설정
            transform.position += dashDirection * dashDistance; //대쉬 거리만큼 순간이동
        }

        //플레이어 공격 상태 체킹
        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true; //마우스 클릭을 하면 화살을 발사하므로 공격 상태로 변경
        }
        if (Input.GetMouseButtonUp(0))
        {
            isAttack = false; //공격중이 아닌 상태로 변경
        }

        //플레이어 이동
        Vector3 moveDirection = new Vector3(moveHorizontal, moveVertical, 0f).normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime);

        UpdatePlayerState(); //플레이어 상태 업데이트
        Dead(); //플레이어 사망
    }

    void Dead() //플레이어 사망
    {
        if (playerHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerState() //플레이어 상태 업데이트
    {
        if (healthSlider != null)
        {
            healthSlider.value = playerHP / maxPlayerHP; //플레이어의 체력 퍼센트만큼 value값 조절
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = playerSP / maxPlayerSP; //플레이어의 스테미너 퍼센트만큼 value값 조절
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //몬스터와 충돌 시
    {
        if (collision.CompareTag("Mob"))
        {
            Debug.Log("몬스터와 충돌");
            playerHP--;
            UpdatePlayerState(); //플레이어 상태 업데이트
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TileMap")
        {
            if (collision.contacts[0].normal.y>0.7f)
            {
                //플레이어의 입력값을 제어
                moveVertical = 0f;

            }
          
        }
    }

    public IEnumerator StaminaAutoHeal() //스테미너 자동 회복
    {
        yield return new WaitForSeconds(0.01f); //0.01초마다
        playerSP += 0.05f; //0.05씩 스태미너 회복
        playerSP = Mathf.Min(playerSP, maxPlayerSP); //최대 스테미너를 넘지 않도록 제한
        UpdatePlayerState(); //플레이어 상태 업데이트
    }
}
