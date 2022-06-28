using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    internal void Move(float speed)
    {
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }
    
    internal void Jump()
    {
        float jumpForce = 5;
        _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
