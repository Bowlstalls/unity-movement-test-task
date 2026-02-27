using UnityEngine;

namespace _2dmovement.Scripts {
  [RequireComponent(typeof(SpriteRenderer))]
  public class Explosion : MonoBehaviour {
    [SerializeField] private float expansionSpeed;
    [SerializeField] private float fadeSpeed;
    private SpriteRenderer _renderer;
    private Vector3 _expansionVector;

    private void OnEnable()
    {
      _renderer = GetComponent<SpriteRenderer>();
      _expansionVector = new Vector3(expansionSpeed, expansionSpeed, 0);
    }
    private void FixedUpdate()
    {
      transform.localScale += _expansionVector;
      _renderer.color -= new Color(0, 0, 0, fadeSpeed);
      if (_renderer.color.a <= 0) {
        Destroy(gameObject);
      }
    }
  }
}