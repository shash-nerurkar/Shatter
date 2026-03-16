namespace Scripts.Entity.Behaviours.Damaging
{
    /// <summary>
    /// An entity which performs damaging actions at a specific fire-rate.
    /// </summary>
    public interface IWeapon
    {
        /// <summary>
        /// The number of times this weapon can fire in one second.
        /// </summary>
        public float FireRatePerSecond { get; }

        /// <summary>
        /// Fires the weapon, causing it to deal damage.
        /// </summary>
        public void Fire();
    }
}