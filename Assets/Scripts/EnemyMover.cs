using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move(FindTarget());
    }

    private Vector3 FindTarget()
    {
        int findRadius = 50;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findRadius);
        
        Vector3 target = transform.position;
        
        foreach (var collider in colliders)
        {
            if (target == transform.position && collider.TryGetComponent(out Player player))
            {
                target = player.transform.position;
            }
        }

        return target;
    }

    private void Move(Vector3 target)
    {
        if (target != transform.position)
        {
            _rigidbody.velocity = (target - transform.position).normalized * _speed;   
        }
    }

    private void OnValidate()
    {
        _speed = Mathf.Max(_speed, 0);
    }
}
