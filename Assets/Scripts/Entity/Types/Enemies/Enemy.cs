using UnityEngine;
using Scripts.Entity.Behaviours.Movement;
using Scripts.Entity.Behaviours.Targetability;

namespace Scripts.Entity.Enemies
{
    /// <summary>
    /// Represents an enemy entity in the game.
    /// </summary>
    public class Enemy : MonoBehaviour, IDamageCountable, IMovable
    {
        #region Fields

        public float Health { get; private set; }

        public float Speed { get; private set; }

        public Vector2 MoveDirection { get; private set; }

        #endregion


        #region Methods

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