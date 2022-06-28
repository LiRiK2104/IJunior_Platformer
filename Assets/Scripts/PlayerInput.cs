using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] [RequireComponent(typeof(Animator))] [RequireComponent(typeof(PlayerMove))]
public class PlayerInput : MonoBehaviour
{
    private const float _walkSpeed = 2;
    private const float _runSpeed = 4;
    private float _speed = 0;
    
    private Animator _animator;
    private SpriteRenderer _renderer;
    private PlayerMove _playerMove;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        _speed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? _runSpeed : _walkSpeed;
        
        float axisValue = Input.GetAxis("Horizontal") * _speed;
        
        if (axisValue != 0)
        {
            Move(axisValue);
        }

        Move(axisValue);

        if (axisValue > 0)
        {
            _renderer.flipX = false;
        }
        else if (axisValue < 0)
        {
            _renderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

    private void Move(float speed)
    {
        _playerMove.Move(speed);
        _animator.SetFloat("Speed",  Mathf.Abs(speed));
    }
    
    private void Jump()
    {
        _playerMove.Jump();
        _animator.SetTrigger("Jump");
    }
    
    private void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}
