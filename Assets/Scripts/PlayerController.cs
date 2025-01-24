using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineFX;
    [SerializeField] ParticleSystem rightRotationEngineFX;
    [SerializeField] ParticleSystem leftRotationEngineFX;
    [SerializeField] UnityEngine.UI.Slider fuelbar;
    [SerializeField] float fuelSpendOnThrust = 5f;
    [SerializeField] float maxFuel = 100f;
    [SerializeField] Vector3 checkpointOffset = Vector3.zero;
    [HideInInspector] public float fuel;

    //Vector2 minBounds;
    //Vector2 maxBounds;

    Rigidbody rb;
    AudioSource audioSource;
    CollisionHandler collisionHandler;

    // Start is called before the first frame update
    void Start()
    {
        if (CheckpointHolder.checkpointPosition != Vector3.zero)
        {
            transform.position = CheckpointHolder.checkpointPosition + checkpointOffset; //static variable is needed !!!
        }
        fuelbar.maxValue = maxFuel;
        fuel = maxFuel;
        fuelbar.value = fuel;
        rb = GetComponent<Rigidbody>();
        audioSource = FindObjectOfType<AudioSource>();
        collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        fuelbar.value = fuel;
        if (fuel == 0)
        {
            collisionHandler.StartCrashSequence();
        }
        else
        {
            ProcessThrust();
            ProcessRotation();
        }
    }

    /*void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    } */
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
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

            fuel -= fuelSpendOnThrust;
            Debug.Log("Fuel: " + fuel);
            ClampFuel();
        }
        else
        {
            audioSource.Stop();
            mainEngineFX.Stop();
        }
    }

    public void ClampFuel()
    {
        if (fuel > maxFuel)
        {
            fuel = maxFuel;
        }
        else if (fuel < 0)
        {
            fuel = 0;
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationThrust); //time.deltatime kullanmak gerekmeyebilir
            if (!leftRotationEngineFX.isPlaying)
            {
                leftRotationEngineFX.Play();
            }
            //Debug.Log("Rotating Left");
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationThrust); //time.deltatime kullanmak gerekmeyebilir
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
