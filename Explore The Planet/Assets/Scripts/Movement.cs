using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 50f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;

    Rigidbody myRigidbody;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            myAudioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void StartThrusting()
    {
        myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(mainEngine);
        }
        PlayEngineParticles(mainEngineParticles);
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartRotation(rotateThrust);
            PlayEngineParticles(rightEngineParticles);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            StartRotation(-rotateThrust);
            PlayEngineParticles(leftEngineParticles);
        }
        else
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
    }

    void PlayEngineParticles(ParticleSystem engineParticles)
    {
        if (!engineParticles.isPlaying)
        {
            engineParticles.Play();
        }
    }

    void StartRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}
