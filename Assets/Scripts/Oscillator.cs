﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField]  float period = 2f; 

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period == 0) return;
        float cycles = Time.time / period; // contunually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f)  / 2f; // recalculatd to go from 0 to 1 so its cleaner

        Vector3 offest = movementVector * movementFactor;
        transform.position = startingPosition +offest;
    }
}
