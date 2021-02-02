using SparringManager.Serial;
using UnityEngine;

namespace SparringManager.Serial.Example
{
    public class ExampleDebugTextController : MonoBehaviour
    {
        public SerialControllerCameraHitBox serialController;

        private void OnEnable()
        {
            ImpactPointControl.onImpact += OnImpact;
        }

        private void OnDisable()
        {
            ImpactPointControl.onImpact -= OnImpact;
        }

        private void OnImpact(object sender, ImpactPointControlEventArgs e)
        {
            Debug.Log(string.Format("Impact: Player [{0}], Position [{1}], Accelerometer [{2}]",
                e.playerIndex,
                e.impactPosition,
                e.accelerometer));
        }

        private void Update()
        {
            Vector3 acceleration = serialController.acceleration;
            Debug.Log(string.Format("Acceleration Player {0}", acceleration));
        }
    }
}
