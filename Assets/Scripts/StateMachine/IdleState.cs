using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IdleState : State
{
    private Rigidbody2D _rigidbody;

    public override void Enter(Player target)
    {
        base.Enter(target);
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }
    
    protected override void DoAction() { }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

}
