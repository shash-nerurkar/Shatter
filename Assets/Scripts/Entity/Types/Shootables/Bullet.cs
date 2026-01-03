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

        public float BaseDamage { get; private set; }

        public float Speed { get; private set; }

        public Vector2 MoveDirection { get; private set; }

        public float Health { get; private set; }

        #endregion


        #region Methods
        
        public void Damage()
        {
            throw new System.NotImplementedException();
        }

        public void GetDamaged()
        {
            throw new System.NotImplementedException();
        }

        public void GetTargeted()
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}