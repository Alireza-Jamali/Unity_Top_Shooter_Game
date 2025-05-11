using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Straight line movement implementation
    /// </summary>
    public class StraightMovement : IEnemyMovement
    {
        private readonly float _speed;
    
        public StraightMovement(float speed) => _speed = speed;

        public void Move(Transform transform)
        {
            transform.Translate(Vector3.back * (_speed * Time.deltaTime));
        }
    }
}