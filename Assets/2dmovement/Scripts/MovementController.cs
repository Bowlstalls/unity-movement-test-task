using System;
using UnityEngine;

namespace _2dmovement.Scripts {
  [RequireComponent(typeof(Rigidbody2D))]
  public class MovementController : MonoBehaviour {
    [SerializeField] private float topSpeed;
    [SerializeField] private float force;
    
    [NonSerialized] public Rigidbody2D Rb;
    private Vector2 _moveVector = Vector2.zero;
    private Vector2 _pushForce = Vector2.zero;

    public bool IsGrounded => Physics2D.OverlapBox(transform.position, transform.localScale, 0,
        1 << LayerMask.NameToLayer("Ground"));

    private void OnEnable()
    {
      Rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
      UpdateMove();
      UpdatePush();
    }
    
    /// <summary>
    /// Move object in a specified direction
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector2 direction) => _moveVector += direction;
    public void MoveRight() => Move(new Vector2(1, 0));
    public void MoveLeft() => Move(new Vector2(-1, 0));
    public void MoveUp() => Move(new Vector2(0, 1));
    
    /// <summary>
    /// Push object ignoring move limitations (like an explosion would, for example)
    /// </summary>
    /// <param name="pushForce"></param>
    public void Push(Vector2 pushForce) => _pushForce += pushForce;
    /// <summary>
    /// Push object from a specified location with a specified force
    /// </summary>
    /// <param name="position">Location from which to push</param>
    /// <param name="pushForce">Force with which to push</param>
    /// <param name="reduceWithDistance">Whether to apply push force reduction over distance (default true)</param>
    public void PushFrom(Vector2 position, float pushForce, bool reduceWithDistance = true)
    {
      var trPos = transform.position;
      var delta = new Vector2(trPos.x - position.x, trPos.y - position.y);
      var res = delta.normalized * pushForce;
      if (reduceWithDistance) {
        res /= Mathf.Pow(1 + Math.Abs(delta.magnitude), 2);
      }
      Push(res);
    }

    private void UpdateMove()
    {
      Rb.AddForce(_moveVector.normalized * force);
      var abs = Math.Abs(Rb.linearVelocityX);
      if (abs > topSpeed) {
        Rb.linearVelocityX *= topSpeed / abs;
      }
      _moveVector = Vector2.zero;
    }
    private void UpdatePush()
    {
      Rb.AddForce(_pushForce);
      _pushForce = Vector2.zero;
    }
  }
}