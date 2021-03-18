using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hitbox.PingPong2X
{
    public class TargetBehavior : MonoBehaviour
    {
        private int _index = 0;
        private Renderer _render;
        private Color _color = Color.green;
		private float _scale;
        

        [SerializeField]
        private GameObject _hitFeedbackPrefab;

        void Awake()
        {
            _render = GetComponent<Renderer>();
        }

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }

		public Color TargetColor {
			get
			{
				return _color;
			}
			set
			{
				_color = value;
			}
		}
		
		public float Scale
		{
			get {
				return _scale;
			}
			set {
				_scale = value;
			}

		}

        public void SetHit()
        {
			// Trigger explose animation = instantiate impact explosion
            if (_hitFeedbackPrefab != null)
            {
                var go = Instantiate(_hitFeedbackPrefab, this.transform.position, Quaternion.identity);
                go.gameObject.layer = this.gameObject.layer;
            }
        }

		private void Update()
		{
			_render.material.SetColor("_Color", _color);
			this.transform.localScale = new Vector3(_scale, _scale, _scale);
		}

		void OnBecameInvisible()
        {
            this.destroyTarget();
        }

        public void destroyTarget()
        {
            Destroy(this.gameObject);
        }
    }
}
