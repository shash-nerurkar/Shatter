using UnityEngine;
using Scripts.Entity.Behaviours.Damaging;

namespace Scripts.Entity.Weapons
{
    /// <summary>
    /// Represents a melee weapon entity in the game.
    /// </summary>
    public class MeleeWeapon : MonoBehaviour, IWeapon, IDamager
    {
        #region Fields

        public float FireRatePerSecond { get; private set; }

        public float BaseDamage { get; private set; }

        #endregion


        #region Methods

        public void Damage()
        {
            throw new System.NotImplementedException();
        }

        public void Fire()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}