using UnityEngine;

public class SkyboxScroller : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float skyboxSpeedMultiplier = 0.1f;

    private Vector3 lastPlayerPosition;
    private Vector3 playerVelocity;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }

        lastPlayerPosition = player.position;
    }

    private void Update()
    {
        if (player == null) return;

        // Calculate player velocity based on movement
        playerVelocity = (player.position - lastPlayerPosition) / Time.deltaTime;

        // Calculate the magnitude of the velocity to control the skybox rotation speed
        float skyboxRotationSpeed = playerVelocity.magnitude * skyboxSpeedMultiplier;

        // Apply the rotation speed to the skybox
        float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation + skyboxRotationSpeed * Time.deltaTime);

        // Update the last player position for the next frame
        lastPlayerPosition = player.position;
    }
}
