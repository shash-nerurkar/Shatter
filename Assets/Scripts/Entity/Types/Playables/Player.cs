using UnityEngine;
using Scripts.Entity.Behaviours.Collection;
using Scripts.Entity.Behaviours.Movement;
using Scripts.Entity.Behaviours.Playability;
using Scripts.Entity.Behaviours.Targetability;

namespace Scripts.Entity.Playables
{
    /// <summary>
    /// Represents the player entity in the game.
    /// </summary>
    public class Player : MonoBehaviour, IDamageCountable, IMovable, ICollector, IPlayable
    {
        #region Fields

        public float Health { get; private set; }

        public float Speed { get; private set; }

        public Vector2 MoveDirection { get; private set; }

        #endregion

        
        #region Methods

        public void Collect()
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