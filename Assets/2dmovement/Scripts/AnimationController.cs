using System;
using UnityEngine;

namespace _2dmovement.Scripts {
  [RequireComponent(typeof(Animation), typeof(MovementController))]
  public class AnimationController : MonoBehaviour {
    [SerializeField] private float runningSpeedThreshold;
    private MovementController _mc;
    private Animator _animator;
    private bool _facingPositive = true;

    private void OnEnable()
    {
      _animator = GetComponent<Animator>();
      _mc = GetComponent<MovementController>();
    }
    private void FixedUpdate()
    {
      var running = Math.Abs(_mc.rb.linearVelocityX) > runningSpeedThreshold;
      _animator.SetBool("running", running);
      _animator.SetBool("flying", !_mc.IsGrounded);
      if (!running) return;
      
      var positive = _mc.rb.linearVelocityX > 0;
      if (_facingPositive != positive) {
        transform.localScale *= new Vector2(-1, 1);
        _facingPositive = positive;
      }
    }
  }
}