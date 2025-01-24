using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] Vector2 offset;
    Material material;
    PlayerController playerController;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rb = playerController.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = moveSpeed * Time.deltaTime * Mathf.Sign(rb.velocity.y);
        material.mainTextureOffset += offset;
    }
}
