namespace Scripts.Entity.Behaviours.Targetability
{
    /// <summary>
    ///  An entity that can be damaged by <see cref="IDamager"/>.
    /// </summary>
    public interface IDamageable : ITargetable
    {
        /// <summary>
        /// The current health of the entity.
        /// </summary>
        public float Health { get; }

        /// <summary>
        /// Called when the entity is damaged by an <see cref="IDamager"/>.
        /// </summary>
        public void GetDamaged();
    }
}