using UnityEngine;
using Scripts.Entity.Behaviours.Damaging;
using Scripts.Entity.Behaviours.Targetability;

namespace Scripts.Entity.Shootables
{
    /// <summary>
    /// Represents a shootable bullet entity in the game.
    /// </summary>
    public class Bullet : MonoBehaviour, IShootable, IDamageCountable
    {
        #region Fields
        
        public Vector3 CurrentPosition { get; private set; }

        public float BaseDamage { get; private set; }

        public float Speed { get; private set; }

        public Vector2 MoveDirection { get; private set; }

        public float Health { get; private set; }

        #endregion


        #region Methods
        
        public void Damage()
        {
            
        }

        public void GetDamaged()
        {
            
        }

        public void GetTargeted()
        {
            
        }

        public void Move(Vector2 direction)
        {
            MoveDirection = direction;
        }

        #endregion
    }
}