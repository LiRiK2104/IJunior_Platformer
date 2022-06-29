using UnityEngine;

[RequireComponent(
    typeof(SpriteRenderer), 
    typeof(Animator), 
    typeof(PlayerMove))]

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    
    private const float WalkSpeed = 2;
    private const float RunSpeed = 4;
    
    private static readonly int Speed = Animator.StringToHash(PlayerAnimator.Params.Speed);
    private static readonly int JumpState = Animator.StringToHash(PlayerAnimator.States.Jump);
    private static readonly int AttackState = Animator.StringToHash(PlayerAnimator.States.Attack);
    
    private float _speed;
    
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
            _weapon.Shoot(transform.position);
        }
    }

    private void Move(float speed)
    {
        _playerMove.Move(speed);
        _animator.SetFloat(Speed,  Mathf.Abs(speed));
    }
    
    private void Jump()
    {
        _playerMove.Jump();
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
    }
}
