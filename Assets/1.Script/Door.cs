using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject closedDoor;
    public GameObject openDoor;
    public GameObject doorAnimation;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            StartCoroutine(DoorOpen());
        }
    }

    IEnumerator DoorOpen()
    {
        Debug.Log("¹® ¿­¸²");

        closedDoor.SetActive(false);
        doorAnimation.SetActive(true);
        openDoor.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        doorAnimation.SetActive(false);
    }
}