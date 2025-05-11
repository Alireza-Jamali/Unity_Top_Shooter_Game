using System;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Weapon contract using Strategy pattern
    /// </summary>
    public interface IWeapon
    {
        /// <summary>Attempt to fire the weapon</summary>
        void Fire(Vector3 direction, Action updateAmmoDelegate);
    
        /// <summary>Reload ammunition</summary>
        void Reload();
    
        /// <summary>Current ammo count</summary>
        int CurrentAmmo { get; }
    
        /// <summary>Maximum ammo capacity</summary>
        int MaxAmmo { get; }
        
        /// <summary>Weapon's rate of fire</summary>
        float FireRate { get; }
    
        /// <summary>Heat percentage (0-1) for heat-based weapons</summary>
        float HeatPercentage { get; }
    }
}