using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private MeshRenderer body;

        private Rigidbody projectileRigidbody;
        private int damage;
        private float lifetime;
        private Collider ownerCollider;
        private bool isInitialized;

        private const float particleLifetime = 2;

        private void Awake()
        {
            projectileRigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(int newDamage, float speed, float newLifetime, Collider newOwnerCollider)
        {
            damage = newDamage;
            lifetime = newLifetime;
            ownerCollider = newOwnerCollider;
            isInitialized = true;

            if (ownerCollider != null && TryGetComponent(out Collider projectileCollider))
            {
                Physics.IgnoreCollision(projectileCollider, ownerCollider);
            }
            particles.Play();
            projectileRigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
            //Destroy(gameObject, lifetime);
            StartCoroutine(DestroyProjectile(lifetime));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!isInitialized) return;

            if (collision.collider == ownerCollider) return;

            if (collision.collider.TryGetComponent(out DamageableTarget target))
            {
                target.TakeDamage(damage);
            }
            Debug.Log(collision.collider.name);
            StartCoroutine(DestroyProjectile(0));
        }

        private IEnumerator DestroyProjectile(float delay)
        {
            yield return new WaitForSeconds(delay);
            body.enabled = false;
            projectileRigidbody.isKinematic = true;
            particles.Play();
            yield return new WaitForSeconds(particleLifetime);
            Destroy(gameObject);

        }
    }
}