using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;
    PlayerController playerController;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Obstacle":
                Debug.Log("Hit Obstacle");
                StartCrashSequence();
                break;
            case "Start":
                Debug.Log("Start Platform");
                break;
            case "Finish":
                Debug.Log("Finish Platform");
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Hit ground");
                StartCrashSequence();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            playerController.fuel += 500;
            playerController.ClampFuel();
            Debug.Log("Fuel collected, fuel:" + playerController.fuel);
            Destroy(other.gameObject);
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartSuccessSequence()
    {
        audioSource.PlayOneShot(success);
        GetComponent<PlayerController>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    public void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash);
        GetComponent<PlayerController>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }
}
