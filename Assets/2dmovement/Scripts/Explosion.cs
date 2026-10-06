using System.Collections;
using UnityEngine;

namespace _2dmovement.Scripts {
  [RequireComponent(typeof(SpriteRenderer))]
  public class Explosion : MonoBehaviour {
    public float maxImpulse;
    public float expansionSpeed;
    public float maxRadius;
    
    private SpriteRenderer _renderer;
    private float _radius;

    private void OnEnable()
    {
      _renderer = GetComponent<SpriteRenderer>();
      _radius = transform.localScale.x / 2;
      PushAll();
      StartCoroutine(Fade());
    }

    private void PushAll()
    {
      var layerMask = ~(1 << LayerMask.NameToLayer("Ground"));
      // ReSharper disable once Unity.PreferNonAllocApi
      foreach (var collider in Physics2D.OverlapCircleAll(transform.position, maxRadius, layerMask)) {
        var colRb = collider.attachedRigidbody;
        Debug.Log(collider.gameObject);
        if (colRb == null) {
          continue;
        }
        var impulse = collider.transform.position - transform.position;
        var dist = impulse.magnitude;
        impulse = impulse.normalized * maxImpulse / Mathf.Pow(1 + dist, 2);
        collider.attachedRigidbody.AddForce(impulse, ForceMode2D.Impulse);
      }
    }
    private IEnumerator Fade()
    {
      var steps = Mathf.FloorToInt((maxRadius - _radius) / expansionSpeed);
      var expansionVector = new Vector3(expansionSpeed, expansionSpeed, 0);
      var fadeSpeed = 1 / steps;

      for (var i = 0; i < steps; i++) {
        transform.localScale += expansionVector;
        _renderer.color -= new Color(0, 0, 0, fadeSpeed);
        yield return null;
      }
      Destroy(gameObject);
    }
  }
}