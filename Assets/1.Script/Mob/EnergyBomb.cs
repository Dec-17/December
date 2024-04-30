using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBomb : MonoBehaviour
{
    public GameObject reazerPrefab; //레이저
    public GameObject razerLinePrefab; //레이저 경고표시
    public float razerLineDuration = 1f; //레이저 경고표시 지속 시간
    public float razerDuration = 0.3f; //레이저 지속 시간

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "collider") //충돌한 오브젝트의 태그가 collider일 때
        {
            //파괴되기 전 위치의 X축은 프리팹 원본 그대로 사용, Y축은 현재 오브젝트의 Y축으로 설정하여 레이저 경고표시 생성
            Vector3 spawnPosition = new Vector3(razerLinePrefab.transform.position.x, transform.position.y, 0f);
            GameObject razerLine = Instantiate(razerLinePrefab, spawnPosition, Quaternion.identity);

            Destroy(razerLine, razerLineDuration); //지정된 시간 후에 레이저 경고표시 파괴

            //레이저 생성 코루틴 작성 해야함

            Destroy(gameObject); //오브젝트 파괴
        }
    }
}
