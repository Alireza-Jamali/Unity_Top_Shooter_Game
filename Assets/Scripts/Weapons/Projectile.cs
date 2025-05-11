using System;
using Enemy;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Weapons
{
    /// <summary>
    /// Is placed on Projectile Prefab
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private int damage = 1;
        [SerializeField] GameObject[] explosionEffects;

        private Rigidbody _rigidBody;
        private ProjectilePool _pool;
        private float _timer;

        public void Initialize(ProjectilePool pool)
        {
            _pool = pool;
            _rigidBody = GetComponent<Rigidbody>();
        }

        public void Fire(Vector3 direction)
        {
            _rigidBody.velocity = direction * _speed;
            _timer = _lifeTime;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _pool.ReturnToPool(this);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Enemy))
            {
                if (FindObjectOfType<WeaponManager>().GetCurrentWeapon() is SniperWeapon)
                {
                    var enemies = Physics.SphereCastAll(transform.position, 1, transform.position);
                    foreach (var enemy in enemies)
                    {
                        if (enemy.collider.CompareTag(Tags.Enemy))
                            enemy.collider.gameObject.GetComponent<IEnemy>().TakeDamage(damage);
                    }
                }
                else
                {
                    other.gameObject.GetComponent<IEnemy>().TakeDamage(damage);
                    HandleProjectile();
                }
            }
            else if (other.gameObject.CompareTag(Tags.Ground))
            {
                HandleProjectile();
            }
            else if (other.CompareTag(Tags.AmmoBox))
            {
                var weaponManager = FindObjectOfType<WeaponManager>();
                int amount = weaponManager.GetCurrentWeapon() switch
                {
                    BasicWeapon => 20,
                    HeatWeapon => 500,
                    SniperWeapon => 3,
                    _ => 0
                };
                weaponManager.GetCurrentWeapon().IncreaseAmmo(amount);
                weaponManager.ReDrawUI();
                Destroy(other.gameObject);
                HandleProjectile();
            }
        }
        void HandleProjectile()
        {
            var explosion =
                Instantiate(explosionEffects[Random.Range(0, 4)], transform.position, Quaternion.identity);
            Destroy(explosion, 1);
            _pool.ReturnToPool(this);
        }
    }
}