using ScripableObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public class TankShooter : MonoBehaviour
    {
        [SerializeField] private TankConfig config;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Collider ownerCollider;
        
        [SerializeField] private bool usePlayerInput = true;

        private float nextFireTime;

        private void Update()
        {
            if (usePlayerInput && Input.GetMouseButtonDown(0))
                TryShoot();
        }

        public void TryShoot()
        {
            if (config == null || config.projectilePrefab == null || firePoint == null)
            {
                Debug.LogWarning("TankShooter налаштований не повністю.");
                return;
            }

            if (Time.time < nextFireTime) return;

            Projectile projectile = Instantiate(config.projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.Initialize(config.damage, config.projectileSpeed, config.projectileLifetime, ownerCollider);
            nextFireTime = Time.time + config.fireCooldown;
        }
    }
}