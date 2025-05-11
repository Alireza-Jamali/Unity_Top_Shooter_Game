using System;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] Text basicWeaponAmmo;
        [SerializeField] Text heatWeaponAmmo;
        [SerializeField] Text sniperWeaponAmmo;
        [SerializeField] ProjectilePool _projectilePool;
        [SerializeField] Image heatIndicator;
        [SerializeField] GameObject baseWeaponPrefab;
        [SerializeField] GameObject heatWeaponPrefab;
        [SerializeField] GameObject sniperWeaponPrefab;

        private Action UpdateAmmoDelegate;

        private IWeapon _currentWeapon;
        private IWeapon _basicWeapon;
        private IWeapon _sniperWeapon;
        private IWeapon _heatWeapon;

        void Awake()
        {
            GameManager.OnRestart += OnRestart;
            _currentWeapon = _basicWeapon = new BasicWeapon(_projectilePool);
            _sniperWeapon = new SniperWeapon(_projectilePool);
            _heatWeapon = new HeatWeapon(_projectilePool);
            UpdateAmmoDelegate = () =>
            {
                switch (_currentWeapon)
                {
                    case BasicWeapon b:
                        basicWeaponAmmo.text = $"{b.CurrentAmmo}";
                        break;
                    case HeatWeapon h:
                        heatWeaponAmmo.text = $"{h.CurrentAmmo}";
                        break;
                    case SniperWeapon s:
                        sniperWeaponAmmo.text = $"{s.CurrentAmmo}";
                        break;
                }
            };
        }
        private void Update()
        {
            if (_currentWeapon is HeatWeapon heatWeapon)
            {
                if (heatWeapon.GetCurrentHeat() > 0)
                {
                    heatWeapon.SetCurrentHeat(
                        Mathf.Clamp01(heatWeapon.GetCurrentHeat() - HeatWeapon.coolRate * Time.deltaTime),
                        heatIndicator);
                }
            }
        }
        void OnRestart()
        {
            _basicWeapon.Reload();
            _sniperWeapon.Reload();
            _heatWeapon.Reload();
            _currentWeapon = _basicWeapon;
            ReDrawUI();
        }
        public void ReDrawUI()
        {
            basicWeaponAmmo.text = $"{_basicWeapon.CurrentAmmo}";
            heatWeaponAmmo.text = $"{_heatWeapon.CurrentAmmo}";
            sniperWeaponAmmo.text = $"{_sniperWeapon.CurrentAmmo}";
        }

        public void SwitchToBasicWeapon()
        {
            DeactivateAllWeapons();
            baseWeaponPrefab.SetActive(true);
            _currentWeapon = _basicWeapon;
        }
        public void SwitchToHeatWeapon()
        {
            DeactivateAllWeapons();
            heatWeaponPrefab.SetActive(true);
            _currentWeapon = _heatWeapon;
        }
        public void SwitchToSniperWeapon()
        {
            DeactivateAllWeapons();
            sniperWeaponPrefab.SetActive(true);
            _currentWeapon = _sniperWeapon;
        }

        private void DeactivateAllWeapons()
        {
            baseWeaponPrefab.SetActive(false);
            heatWeaponPrefab.SetActive(false);
            sniperWeaponPrefab.SetActive(false);
        }

        public void Fire(Vector3 direction)
        {
            _currentWeapon.Fire(direction, UpdateAmmoDelegate);
        }


        public WeaponBase GetCurrentWeapon() => _currentWeapon as WeaponBase;
    }
}