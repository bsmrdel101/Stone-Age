using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private float horizontal, vertical;
    private Vector3 playerInput;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    private void FixedUpdate()
    {
        GetInputs();
        MovePlayer();
    }

    private void GetInputs()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        playerInput = new Vector3(horizontal, vertical, 0);
    }

    private void MovePlayer()
    {
        rb.MovePosition(transform.position + moveSpeed * Time.deltaTime * playerInput);
    }
}
