using System;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Weapons
{
    /// <summary>
    /// Is placed on Player Weapon
    /// </summary>
    public class ShootingManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private LayerMask _shootingPlaneMask; // Layer for ground/aim plane
        [SerializeField] private ProjectilePool _projectilePool;

        [Header("Settings")]
        [SerializeField] private float _projectileSpeed = 20f;

        private Camera _mainCamera;
        WeaponManager weaponManager;

        private void Awake()
        {
            _mainCamera = Camera.main;
            weaponManager = GetComponent<WeaponManager>();
        }

        void Start()
        {
            InputHandler.OnAction = ProcessShot;
        }

        private void ProcessShot(Vector2 screenPosition)
        {
            // Convert screen position to world position using raycast
            if (TryGetWorldPosition(screenPosition, out Vector3 worldPosition))
            {
                Vector3 shootDirection = (worldPosition - transform.position).normalized;
                FireProjectile(shootDirection);
            }
        }

        private bool TryGetWorldPosition(Vector2 screenPosition, out Vector3 worldPosition)
        {
            Ray ray = _mainCamera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _shootingPlaneMask))
            {
                worldPosition = hit.point;
                return true;
            }

            worldPosition = Vector3.zero;
            return false;
        }

        private void FireProjectile(Vector3 direction)
        {

            weaponManager.Fire(direction);

            transform.up = direction;
        }
    }
}