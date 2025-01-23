using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineFX;
    [SerializeField] ParticleSystem rightRotationEngineFX;
    [SerializeField] ParticleSystem leftRotationEngineFX;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = FindObjectOfType<AudioSource>();
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
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            Debug.Log("Thrusting");

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSFX);
            }
            if (!mainEngineFX.isPlaying)
            {
                mainEngineFX.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainEngineFX.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationThrust * Time.deltaTime); //time.deltatime kullanmak gerekmeyebilir
            if (!leftRotationEngineFX.isPlaying)
            {
                leftRotationEngineFX.Play();
            }
            //Debug.Log("Rotating Left");
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationThrust * Time.deltaTime); //time.deltatime kullanmak gerekmeyebilir
            if (!rightRotationEngineFX.isPlaying)
            {
                rightRotationEngineFX.Play();
            }
            //Debug.Log("Rotating Right");
        }
        else
        {
            leftRotationEngineFX.Stop();
            rightRotationEngineFX.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
