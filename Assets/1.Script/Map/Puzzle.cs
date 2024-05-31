using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public string password = "";
    public string passwordO = "1234";
    public GameObject puzzleDoor;
    public GameObject doorAnimation;

    public GameObject [] statue001;
    public GameObject [] statue002;

    void Start()
    {

    }

    void Update()
    {
        if (password.Length == 4 && password == passwordO)
        {
            StartCoroutine(DoorOpen());
        }
        else if (password.Length == 4 && password != passwordO)
        {
            password = "";
            statue001[0].SetActive(true);
            statue001[1].SetActive(true);
            statue001[2].SetActive(true);
            statue001[3].SetActive(true);

            statue002[0].SetActive(false);
            statue002[1].SetActive(false);
            statue002[2].SetActive(false);
            statue002[3].SetActive(false);
        }
    }
    IEnumerator DoorOpen()
    {
        Debug.Log("문이 열렸습니다");  

        puzzleDoor.SetActive(false);
        doorAnimation.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        doorAnimation.SetActive(false);
        password += "잠금해제 완료";
    }
}
