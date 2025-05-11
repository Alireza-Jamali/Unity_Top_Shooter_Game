using System;
using System.Collections;
using Core;
using Player;
using Services;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Weapons
{
    /// <summary>
    /// Is placed on Ammo Box Manager
    /// </summary>
    public class AmmoBoxManager : MonoBehaviour
    {
        const float _groundLimit = 3.65f;
        [SerializeField] GameObject ammoBoxPrefab;
        [SerializeField] float timerInSeconds = 5;

        Vector3 _randomPosition = Vector3.zero;
        private bool _placeAmmo = false;

        void Awake()
        {
            PlayerHealthManager.OnGameOver += () => _placeAmmo = false;
            GameManager.OnRestart += () => _placeAmmo = true;
        }

        IEnumerator Start()
        {
            ServiceLocator.Get<IWaveManager>().OnWaveStarted += (_,_) => _placeAmmo = true;
            while (true)
            {
                yield return new WaitForSeconds(timerInSeconds);
                if (!_placeAmmo) continue;
                if (!GameObject.FindWithTag(Tags.AmmoBox))
                {
                    _randomPosition.y = 0.3f;
                    _randomPosition.x = Random.Range(-_groundLimit, _groundLimit);
                    _randomPosition.z = Random.Range(-_groundLimit, _groundLimit);
                    Instantiate(ammoBoxPrefab, _randomPosition, Quaternion.identity);
                }
            }
        }
    }
}