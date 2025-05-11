using System.Numerics;

namespace States
{
    public class PlayerDiedEvent
    {
        public Vector3 DeathPosition { get; }
    
        public PlayerDiedEvent(Vector3 position)
        {
            DeathPosition = position;
        }
    }
}