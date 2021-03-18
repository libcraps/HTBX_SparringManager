using UnityEngine;

namespace SparringManager.Serial.Example
{
	public class Player_Movement_Follower : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Tracked object to point toward")]
		private GameObject bag;
		public float _angle = 0;

		void Update()
		{
			Vector3 _bagDir = bag.transform.position - transform.position; //Computing vector between player and bag
			_bagDir = Vector3.Normalize(new Vector3(_bagDir.x, 0, _bagDir.z)); //Getting rid of the height for the vector and normalizing
			Vector3 _playerOrientation = Vector3.Normalize(transform.up); //Normalizing player orientation

			_angle = Vector3.SignedAngle(_bagDir, -bag.transform.forward, Vector3.up);
		}
	}
}
