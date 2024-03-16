using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandle : MonoBehaviour
{
    float emission = 0;


    Car playerCar;
    ParticleSystem particle;
    ParticleSystem.EmissionModule emissionParticle;
    

    // Start is called before the first frame update
    void Awake()
    {
        playerCar = GetComponentInParent<Car>();

        particle = GetComponent<ParticleSystem>();

        emissionParticle = particle.emission;

        emissionParticle.rateOverTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCar.IsTireScreeching(out float lateralVelocity, out bool isBreaking))
        {
            emissionParticle.rateOverTime = 30;
        }
        else
        {
            emissionParticle.rateOverTime = 0;
        }
    }
}
