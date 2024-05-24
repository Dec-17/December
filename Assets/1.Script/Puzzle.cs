using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public string password = "";
    public string passwordO = "1234";
    public GameObject puzzleDoor;
    public GameObject doorAnimation;

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
        }
    }
    IEnumerator DoorOpen()
    {
        Debug.Log("���� ���Ƚ��ϴ�");  

        puzzleDoor.SetActive(false);
        doorAnimation.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        doorAnimation.SetActive(false);
        password += "������� �Ϸ�";
    }
}
