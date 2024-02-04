using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    private float _horizontal, _vertical;
    private Vector3 _playerInput;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;


    private void FixedUpdate()
    {
        GetInputs();
        MovePlayer();
    }

    private void GetInputs()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical"); 
        _playerInput = new Vector3(_horizontal, _vertical, 0);
    }

    private void MovePlayer()
    {
        _rb.MovePosition(transform.position + _moveSpeed * Time.deltaTime * _playerInput);
    }
}
