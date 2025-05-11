using System;
using UnityEngine;
using Utility;

namespace Weapons
{
    /// <summary>
    /// Base weapon class using Template Method pattern
    /// </summary>
    public abstract class WeaponBase : IWeapon
    {
        protected int currentAmmo;
        protected float lastFireTime;
        protected ProjectilePool _projectilePool;

        public int CurrentAmmo => currentAmmo;
        public abstract int MaxAmmo { get; }
        public abstract float FireRate { get; }

        public virtual float HeatPercentage => 0;

        public void IncreaseAmmo(int amount)
        {
            currentAmmo += amount;
        }

        public void Fire(Vector3 direction, Action updateAmmoDelegate)
        {
            if (CanFire())
            {
                PerformFire(_projectilePool.Get(), direction);
                PostFire(updateAmmoDelegate);
            }
        }

        protected virtual bool CanFire()
        {
            return currentAmmo > 0;
        }

        protected abstract void PerformFire(Projectile projectile, Vector3 direction);

        protected void PostFire(Action updateAmmoDelegate)
        {
            lastFireTime = Time.time;
            --currentAmmo;
            updateAmmoDelegate.Invoke();
        }

        public abstract void Reload();
    }
}