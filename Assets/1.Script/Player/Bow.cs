using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public   bool bowVisible = false; //활이 보이는지 여부를 나타내는 변수
    public GameObject BowObj; //활 오브젝트를 담을 공간
    public Animator bowAnimator; //활의 애니메이터
    public GameObject ArrowObj; //화살 오브젝트를 담을 공간
    public Transform ArrowSpawnPoint; //화살이 생성될 위치
    public float arrowInterval = 0.7f; //화살 생성 간격

    public GameObject PlayerObj; //플레이어 오브젝트를 담을 공간
    public Animator PlayerAnimator; //플레이어의 애니메이터
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    public bool IsPlayerX = false;
    public bool IsPlayerDown = false;
    public bool IsPlayerUp = false;


    void Start()
    {
        bowAnimator = BowObj.GetComponent<Animator>();
        playerController = PlayerObj.GetComponent<PlayerController>();
        spriteRenderer = PlayerObj.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        //마우스 위치에 따른 활 회전
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        float bowRotationZ = transform.rotation.z;
        Vector3 playerToMouse = mousePos - transform.position;

        //플레이어 이동 방향에 따른 애니메이션 실행

        if (playerController.moveHorizontal < 0f) //플레이어 좌측 이동
        {
            IsPlayerX = true;
            spriteRenderer.flipX = true; //좌우 플립
            if (playerToMouse.x > 0 && Input.GetMouseButton(0)) //우측으로 활을 쏜다면
            {
                spriteRenderer.flipX = false;
            }
            else if (playerToMouse.x < 0 && Input.GetMouseButton(0)) //좌측으로 활을 쏜다면
            {
                spriteRenderer.flipX = true;
            }
        }
        else if(playerController.moveHorizontal == 0f)
        {
            IsPlayerX = false;
        }

        if (playerController.moveHorizontal > 0f) //플레이어 우측 이동
        {

            IsPlayerX = true;
            spriteRenderer.flipX = false; //좌우 플립 끄기
            if (playerToMouse.x > 0 && Input.GetMouseButton(0)) //우측으로 활을 쏜다면
            {
                spriteRenderer.flipX = false;
            }
            else if (playerToMouse.x < 0 && Input.GetMouseButton(0)) //좌측으로 활을 쏜다면
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (playerController.moveHorizontal == 0f)
        {
            IsPlayerX = false;
        }

        if (playerController.moveVertical < 0f) //플레이어 하단 이동
        {
            IsPlayerDown = true;
        }
        else
        {
            IsPlayerDown = false;
        }

        if (playerController.moveVertical > 0f) //플레이어 상단 이동
        {
            IsPlayerUp = true;
        }
        else
        {
            IsPlayerUp = false;
        }


        if (Input.GetMouseButton(0))
        {
            if (!bowVisible)
            {
                SetBowAlpha(1f); //알파 값을 1로 설정하여 보이게 함
                bowAnimator.SetBool("Attack", true); //화살 발사 애니메이션 재생
                bowVisible = true; //활이 보이는 상태로 변경    
                InvokeRepeating(nameof(CreateArrow), 0f, arrowInterval);//일정 간격으로 화살 생성하는 함수 호출
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.flipX = false; //활을 쏘고 있지 않으므로 플립 비활성화

            StartCoroutine(HideBowAfterDelay(0.1f)); //x초 후에 활을 숨기는 코루틴 시작

            CancelInvoke(nameof(CreateArrow));//화살 생성 간격 함수 호출 중지
        }

        PlayerAnimator.SetBool("PlayerX", IsPlayerX); //좌우이동 애니메이션 실행
        PlayerAnimator.SetBool("PlayerDown", IsPlayerDown); //하단이동 애니메이션 실행
        PlayerAnimator.SetBool("PlayerUp", IsPlayerUp); //상단이동 애니메이션 실행

    }

    void CreateArrow()
    {
        // 화살 생성 및 회전 설정
        GameObject arrow = Instantiate(ArrowObj, ArrowSpawnPoint.position, Quaternion.identity);
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z));
    }

    void SetBowAlpha(float alpha) //활의 알파 값을 변경
    {
        SpriteRenderer bowSpriteRenderer = BowObj.GetComponent<SpriteRenderer>();
        if (bowSpriteRenderer != null)
        {
            Color bowColor = bowSpriteRenderer.color;
            bowColor.a = alpha;
            bowSpriteRenderer.color = bowColor;
        }
    }

    IEnumerator HideBowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //delay초 동안 대기

        SetBowAlpha(0f); //활의 알파 값을 0으로 설정하여 숨김
        bowAnimator.SetBool("Attack", false); //화살 발사 애니메이션 종료
        bowVisible = false; //활을 숨겨짐 상태로 변경
    }
}
