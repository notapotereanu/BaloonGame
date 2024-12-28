using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyCar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
