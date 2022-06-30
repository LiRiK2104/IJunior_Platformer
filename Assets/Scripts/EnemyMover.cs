using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D),
    typeof(SpriteRenderer),
    typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Enemy _main;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _main = GetComponent<Enemy>();
    }

    void FixedUpdate()
    {
        Player target;
        int radiusForMove = 50;
        int radiusForAttack = 1;
        
        if (TryFindTarget(radiusForAttack, out target))
        {
            _main.Attack(target);
        }
        if (TryFindTarget(radiusForMove, out target))
        {
            Move(target.transform.position);
        }
    }

    private bool TryFindTarget(int findRadius, out Player target)
    {
        target = FindTarget(findRadius);
        return target != null;
    }

    private Player FindTarget(int findRadius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findRadius);

        Player target = null;
        
        foreach (var collider in colliders)
        {
            if (target == null && collider.TryGetComponent(out Player player))
            {
                target = player;
            }
        }

        return target;
    }

    private void Move(Vector3 target)
    {
        if (target != transform.position)
        {
            _rigidbody.velocity = (target - transform.position).normalized * _speed;
            Rotate();
        }
    }

    private void Rotate()
    {
        if (_rigidbody.velocity.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_rigidbody.velocity.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
    
    private void OnValidate()
    {
        _speed = Mathf.Max(_speed, 0);
    }
}
