namespace Scripts.Entity.Behaviours.Damaging
{
    /// <summary>
    /// An entity that damages <see cref="IDamageable"/> entities.
    /// </summary>
    public interface IDamager
    {
        /// <summary>
        /// The base damage dealt by this damager.
        /// </summary>
        public float BaseDamage { get; }

        /// <summary>
        /// Deals damage to a <see cref="IDamageable"/> entity.
        /// </summary>
        public void Damage();
    }
}