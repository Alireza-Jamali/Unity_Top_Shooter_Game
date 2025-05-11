using System;
using UnityEngine;
using Utility;

namespace Weapons
{
    /// <summary>
    /// Basic weapon implementation
    /// </summary>
    public class BasicWeapon : WeaponBase
    {
        public override int MaxAmmo => 100;
        public override float FireRate => 0.7f;
        public BasicWeapon(ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
            Reload();
        }

        protected override bool CanFire()
        {
            return base.CanFire() && Time.time > lastFireTime + FireRate;
        }
        
        protected override void PerformFire(Projectile projectile, Vector3 direction)
        {
            projectile.Fire(direction);
        }
        public sealed override void Reload() => currentAmmo = MaxAmmo;
    }
}