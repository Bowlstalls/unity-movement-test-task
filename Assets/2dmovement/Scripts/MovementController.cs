using System;
using UnityEngine;

namespace _2dmovement.Scripts {
  [RequireComponent(typeof(Rigidbody2D))]
  public class MovementController : MonoBehaviour {
    public float topSpeed;
    public float horizontalAcceleration;
    public float verticalImpulse;
    
    [NonSerialized] public Rigidbody2D rb;
    private Vector2 _moveVector;

    public bool IsGrounded => Physics2D.OverlapBox(transform.position, transform.localScale, 0,
        1 << LayerMask.NameToLayer("Ground"));

    private void OnEnable()
    {
      rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
      if (IsGrounded) {
        ApplyMove();
      }
      _moveVector = Vector2.zero;
    }

    public void Move(Vector2 value) => _moveVector += value;
    public void MoveRight() => Move(new Vector2(1, 0));
    public void MoveLeft() => Move(new Vector2(-1, 0));
    public void MoveUp() => Move(new Vector2(0, 1));

    private void ApplyMove()
    {
      if (_moveVector.x != 0) {
        var targetV = Mathf.Clamp(_moveVector.x, -1, 1) * topSpeed;
        var sign = targetV > rb.linearVelocityX ? 1 : -1;
        var factor = Mathf.Clamp(1 - rb.linearVelocityX / targetV, 0, 1);
        rb.AddForce(new Vector2(sign * rb.mass * horizontalAcceleration * factor, 0));
      }
      rb.AddForce(new Vector2(0, rb.mass * verticalImpulse * _moveVector.y), ForceMode2D.Impulse);
    }
  }
}