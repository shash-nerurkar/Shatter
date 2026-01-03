using UnityEngine;
using Scripts.Entity.Behaviours.Damaging;

namespace Scripts.Entity.Weapons
{
    /// <summary>
    /// Represents a ranged weapon entity in the game.
    /// </summary>
    public class RangedWeapon : MonoBehaviour, IWeapon
    {
        #region Fields

        public float FireRatePerSecond { get; private set; }

        #endregion


        #region Methods

        public void Fire()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}