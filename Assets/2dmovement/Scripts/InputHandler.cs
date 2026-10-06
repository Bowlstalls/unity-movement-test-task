using UnityEngine;

namespace _2dmovement.Scripts {
  public class InputHandler : MonoBehaviour {
    [SerializeField] private MovementController player;
    [SerializeField] private float pushForce;
    private Camera _camera;

    private void OnEnable() => _camera = Camera.main;
    private void Update()
    {
      if (Input.GetMouseButtonDown(0)) {
        CreateExplosion();
      }
      if (Input.GetKey(KeyCode.A)) {
        player.MoveLeft();
      }
      if (Input.GetKey(KeyCode.D)) {
        player.MoveRight();
      }
    }
    private void CreateExplosion()
    {
      var origin = _camera.ScreenToWorldPoint(Input.mousePosition);
      origin.z = 0;
      Instantiate(Resources.Load<Explosion>("Prefabs/Explosion"), origin, Quaternion.identity);
    }
  }
}