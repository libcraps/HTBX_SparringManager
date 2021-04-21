using System.Collections;
using UnityEngine;

namespace SparringManager.Scenarios.SimpleHit
{
    /// <summary>
    /// Class that manage the prefab object that will represent an hit
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SimpleHit: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Time until the target fades away completely (in sec).")]
        private float _fadeTime = 1.0f;
        private SpriteRenderer _renderer;
        private Color _targetColor;
        private float _startTime;

        protected void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _targetColor = new Color(_targetColor.r, _targetColor.g, _targetColor.b, 0.0f);
            _startTime = Time.time;
            StartCoroutine(DisplaySprite());
        }

        private IEnumerator DisplaySprite()
        {
            while (_renderer.color.a > 0.0f)
            {
                _renderer.color = Color.Lerp(_renderer.color, _targetColor, (Time.time - _startTime) / _fadeTime);
                if (_renderer.color.a <= 0.0f)
                    Destroy(gameObject);
                yield return null;
            }
        }
    }
}