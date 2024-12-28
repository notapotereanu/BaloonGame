using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject Car;

    private List<GameObject> cars;

    public GameObject Car1;

    private List<GameObject> cars1;

    [SerializeField]
    float spawnTime;

    [SerializeField]
    float speed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Spawn a new car after a chosen amount of time
        if ((Time.frameCount % spawnTime) == 0)
        {
            Instantiate(Car, new Vector3(9, -5, -2), transform.rotation);
        }

        if ((Time.frameCount % spawnTime) == 15)
        {
            Instantiate(Car1, new Vector3(30, -5, 4), transform.rotation);
        }

    }

    private void FixedUpdate()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
        GameObject[] cars1 = GameObject.FindGameObjectsWithTag("car1");

        foreach (var car in cars)
        {
            car.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        }

        foreach (var car in cars1)
        {
            car.transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        }
    }
}
