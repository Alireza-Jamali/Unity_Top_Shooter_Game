using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Weapons
{
    /// <summary>
    /// Heat-based weapon implementation
    /// </summary>
    public class HeatWeapon : WeaponBase
    {
        const float heatPerShot = 0.05f;
        public const float coolRate = 0.05f;

        private float _currentHeat;
        public override int MaxAmmo => 5000;
        public override float FireRate => 0.25f;
        
        public override float HeatPercentage => _currentHeat;

        public float HeatPerShot => heatPerShot;
        public HeatWeapon(ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
            Reload();
        }
        protected override bool CanFire()
        {
            return base.CanFire() && Time.time > lastFireTime + FireRate && _currentHeat < 0.9f;
        }

        protected override void PerformFire(Projectile projectile, Vector3 direction)
        {
            _currentHeat = Mathf.Clamp01(_currentHeat + heatPerShot);
            projectile.Fire(direction);
        }
        public sealed override void Reload() => currentAmmo = MaxAmmo;

        public float GetCurrentHeat() => _currentHeat;
        public void SetCurrentHeat(float newHeat, Image heatIndicator)
        {
            _currentHeat = newHeat;
            heatIndicator.fillAmount = _currentHeat;
        }
    }
}