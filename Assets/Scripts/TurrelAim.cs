using UnityEngine;

namespace DefaultNamespace
{
    public class TurrelAim : MonoBehaviour
    {
        [Header("Turret")]
        [SerializeField] private Transform turretPivot;
        [SerializeField] private float turretKeyboardRotationSpeed = 75f;
        
        public Transform TurretPivot => turretPivot;
       
        
        private void Update()
        {
            RotateTurret();
        }
        
        private void RotateTurret()
        {
            if (turretPivot == null) return;

            float keyboardInput = GetButtonAxis(KeyCode.Z, KeyCode.C);
            float rotationAmount = keyboardInput * turretKeyboardRotationSpeed * Time.deltaTime;

            turretPivot.Rotate(0f, rotationAmount, 0f, Space.Self);
        }
        
        private float GetButtonAxis(KeyCode negativeKey, KeyCode positiveKey)
        {
            float negative = Input.GetKey(negativeKey) ? 1f : 0f;
            float positive = Input.GetKey(positiveKey) ? 1f : 0f;
            return positive - negative;
        }
    }
}