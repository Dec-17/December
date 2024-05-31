using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackDestroy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (gameObject.name.Contains("Energy")) //이름에 Energy가 들어간다면
        {
            Destroy(gameObject, 5f); //*초 후 오브젝트 파괴
        }
        if (gameObject.name.Contains("Razer")) //Razer이 들어간다면
        {
            Destroy(gameObject, 2f); //*초 후 오브젝트 파괴
        }
        if (gameObject.name.Contains("warning")) //warning이 들어간다면
        {
            Destroy(gameObject, 0.5f); //*초 후 오브젝트 파괴
        }
    }
}
