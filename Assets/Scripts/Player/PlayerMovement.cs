using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    private float _horizontal, _vertical;
    private Vector3 _playerInput;

    [Header("Animation")]
    [SerializeField] private Animator _anim;

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
        
        if (_horizontal != 0 || _vertical != 0)
            _anim.SetBool("IsWalking", true);
        else
            _anim.SetBool("IsWalking", false);

        HandleFlipPlayer();
    }

    private void HandleFlipPlayer()
    {
        if (_horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
