using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandle : MonoBehaviour
{
    Car playerCar;
    void Start()
    {
        playerCar = GetComponent<Car>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 InputVector = Vector2.zero;

        InputVector.x = Input.GetAxis("Horizontal");
        InputVector.y = Input.GetAxis("Vertical");

        playerCar.SetInputVector(InputVector);
    }
}
