using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hitbox.PingPong2X
{
    public class TargetsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _targetPrefab;
		private List<GameObject> _targetsCollection;

		private void Awake()
		{
			_targetsCollection = new List<GameObject>();
		}

		public Color GetTargetColor(int index_)
		{
			Color col_ = Color.magenta;
			if (index_ >= 0 && index_ < _targetsCollection.Count)
			{
				col_ = _targetsCollection[index_].GetComponent<TargetBehavior>().TargetColor;
			}
			return col_;
		}

		public void SetCrownTargets(int nTargets_, float radius_, Color color_, float scale_)
        {
            for (int i = 0; i < nTargets_; i++)
            {
                float angle_ = i * 2f * Mathf.PI / nTargets_ + Mathf.PI / 2f;
                float posX_ = this.transform.position.x + radius_ * Mathf.Cos(angle_);
                float posY_ = this.transform.position.y + radius_ * Mathf.Sin(angle_);
                float posZ_ = this.transform.position.z + 50;

                SetTarget(new Vector3(posX_, posY_, posZ_), i, color_, scale_);
            }
        }

        private void SetTarget(Vector3 position_, int targetIndex_, Color color_, float scale_)
        {
			// Instantiate target
			GameObject newTarget_ = Instantiate(_targetPrefab, position_, Quaternion.identity, this.gameObject.transform);     // create target

			// Manage new target
			_targetsCollection.Add(newTarget_);
			newTarget_.GetComponent<TargetBehavior>().Index = targetIndex_; // set target index
			newTarget_.GetComponent<TargetBehavior>().TargetColor = color_; // set target color
			newTarget_.GetComponent<TargetBehavior>().Scale = scale_;
			newTarget_.layer = this.gameObject.layer;
		}

        public void SetImpact(Vector2 position2D_)
        {
			/// Get position and set raycast vector
			Vector3 cameraForward = this.transform.forward;
            Debug.DrawRay(position2D_, cameraForward * 10000, Color.yellow, 10.0f);

			/// Raycast target and action
			RaycastHit hit;
			int layerMask_ = 1 << this.gameObject.layer; // cast only on gameObject layer
			if (Physics.Raycast(position2D_, cameraForward, out hit, Mathf.Infinity, layerMask_))
			{
				Debug.Log("ici " + this.gameObject.layer);
				if (hit.collider != null && hit.transform.tag == "target")
				{
					int playerIndex_ = this.gameObject.GetComponent<ImpactManager>().PlayerIndex;			// get player index
					int targetIndex_ = hit.collider.GetComponent<TargetBehavior>().Index;                   // get target index
					hit.collider.GetComponent<TargetBehavior>().SetHit();                                   // set hit to target

					Debug.Log(playerIndex_);
					transform.parent.GetComponent<PingPongManager>().SetHit(playerIndex_, targetIndex_) ;	// return hit to ping pong manager
				}
			}
        }

		public void SetTarget(int targetIndex_, Color color_)
		{
			_targetsCollection[targetIndex_].GetComponent<TargetBehavior>().TargetColor = color_;
		}
    }
}
