using UnityEngine;

[RequireComponent(
    typeof(Enemy), 
    typeof(Rigidbody2D))]
public class AttackState : State
{
    private Rigidbody2D _rigidbody;
    
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
    
    public override void Enter(Player target)
    {
        base.Enter(target);
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    protected override void DoAction()
    {
        _enemy.Attack(Target);
    }
}
