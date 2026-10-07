using UnityEngine;
using UnityEngine.InputSystem;

namespace _2dmovement.Scripts {
  public class InputHandler : MonoBehaviour {
    [SerializeField] private MovementController player;
    [SerializeField] private float pushForce;
    private Camera _camera;
    private int _groundMask;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _clickAction;

    private void OnEnable()
    {
      _camera = Camera.main;
      _groundMask = 1 << LayerMask.NameToLayer("Ground");
      _moveAction = InputSystem.actions.FindAction("Move");
      _jumpAction = InputSystem.actions.FindAction("Jump");
      _clickAction = InputSystem.actions.FindAction("Attack");
    } 
    private void Update()
    {
      if (_clickAction.WasPressedThisFrame()) {
        CreateExplosion();
      }
      player.Move(_moveAction.ReadValue<Vector2>());
      if (_jumpAction.IsPressed()) {
        player.MoveUp();
      }
    }
    private void CreateExplosion()
    {
      var origin = _camera.ScreenToWorldPoint(Input.mousePosition);
      if (Physics2D.OverlapPoint(origin, _groundMask)) {
        return;
      }
      origin.z = 0;
      Instantiate(Resources.Load<Explosion>("Prefabs/Explosion"), origin, Quaternion.identity);
    }
  }
}