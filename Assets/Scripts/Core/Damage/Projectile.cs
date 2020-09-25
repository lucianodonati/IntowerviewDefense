using System;
using Core.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Damage
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rb = null;
        
        public int Damage { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            var possibleEnemy = other.GetComponent<Enemy>();
            if (null != possibleEnemy)
            {
                possibleEnemy.Health -= Damage;
                Destroy(gameObject);
            }
        }

        public void ShootInDirection(Vector3 direction, float force)
        {
            rb.AddForce(direction * force, ForceMode.VelocityChange);
        }
    }
}