using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

// PARAMETERS

[SerializeField] float mainThrust = 1f;
[SerializeField] float roationSpeed = 1f;
[SerializeField] AudioClip mainEngine;
[SerializeField] ParticleSystem rocketJetParticles;
[SerializeField] ParticleSystem rightThrusterParticles;
[SerializeField] ParticleSystem leftThrusterParticles;


Rigidbody rb;
AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        ProcessThrust();
        ProcessRotaion();
    }


    void ProcessThrust()
    {
    
    if ( Input.GetKey(KeyCode.Space))
            StartThrusting();
        else
            StopThrusting();
    }



    void ProcessRotaion()
    {

    
    if ( Input.GetKey(KeyCode.A))
        LeftRotationControl();
        else if ( Input.GetKey(KeyCode.D))
        RightRotaionControl();
        else
        {
            StopRotating();
        }
    }



    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }


    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!rocketJetParticles.isPlaying)
        {
            rocketJetParticles.Play();
        }
    }

    
        void StopThrusting()
    {
        audioSource.Stop();
        rocketJetParticles.Stop();
    }


        private void RightRotaionControl()
    {
        ApplyRotaion(-roationSpeed);
        if (!rightThrusterParticles.isPlaying)
            rightThrusterParticles.Play();
    }

    private void LeftRotationControl()
    {
        ApplyRotaion(roationSpeed);
        if (!leftThrusterParticles.isPlaying)
            leftThrusterParticles.Play();
    }

        void ApplyRotaion(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing roation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing roation so the ohysic system can take over

    }
}
