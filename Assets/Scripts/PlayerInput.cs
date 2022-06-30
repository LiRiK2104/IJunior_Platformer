using UnityEngine;

[RequireComponent(
    typeof(SpriteRenderer), 
    typeof(Animator),
    typeof(Player))]

public class PlayerInput : MonoBehaviour
{
    private Player _main;
    
    private const float WalkSpeed = 2;
    private const float RunSpeed = 4;
    
    private static readonly int Speed = Animator.StringToHash(PlayerAnimator.Params.Speed);
    private static readonly int JumpState = Animator.StringToHash(PlayerAnimator.States.Jump);
    private static readonly int AttackState = Animator.StringToHash(PlayerAnimator.States.Attack);

    private float _speed;
    
    private Animator _animator;
    private SpriteRenderer _renderer;

    public Animator Animator => _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _main = GetComponent<Player>();
    }

    void Update()
    {
        _speed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? RunSpeed : WalkSpeed;
        
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

        if (Input.GetMouseButton(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _main.Weapon.Shoot(target);
        }
    }

    private void Move(float speed)
    {
        _main.PlayerMove.Move(speed);
        _animator.SetFloat(Speed,  Mathf.Abs(speed));
    }
    
    private void Jump()
    {
        _main.PlayerMove.Jump();
        _animator.SetTrigger(JumpState);
    }
    
    private void Attack()
    {
        _animator.SetTrigger(AttackState);
    }
}


public static class PlayerAnimator
{
    public static class Params
    {
        public const string Speed = "Speed";
    } 
        
    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Attack = nameof(Attack);
        public const string Jump = nameof(Jump);
        public const string TakeDamage = nameof(TakeDamage);
    }
}
