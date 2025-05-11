using System;
using UnityEngine;
using Utility;

namespace Weapons
{
    /// <summary>
    /// Sniper weapon implementation
    /// </summary>
    public class SniperWeapon : WeaponBase
    {
        public override int MaxAmmo => 5;
        public override float FireRate => 5f;
        public SniperWeapon(ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
            Reload();
        }
        protected override void PerformFire(Projectile projectile, Vector3 direction)
        {
            projectile.Fire(direction);
        }
        protected override bool CanFire()
        {
            return base.CanFire() && Time.time > lastFireTime + FireRate;
        }
        public sealed override void Reload() => currentAmmo = MaxAmmo;
    }
}