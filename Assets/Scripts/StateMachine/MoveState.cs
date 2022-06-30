using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D), 
    typeof(SpriteRenderer))]
public class MoveState : State
{
    [SerializeField] private float _speed;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    public override void Enter(Player target)
    {
        base.Enter(target);
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    protected override void DoAction()
    {
        Move(Target.transform.position);
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

}
