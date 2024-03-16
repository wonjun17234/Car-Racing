using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailHandle : MonoBehaviour
{
    Car playerCar;
    TrailRenderer trailRenderer;
    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        playerCar = GetComponentInParent<Car>();
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCar.IsTireScreeching(out float lateralVelocity, out bool isBreaking))
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
}
