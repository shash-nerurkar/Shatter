namespace Scripts.Entity.Behaviours.Targetability
{
    /// <summary>
    /// An entity that takes damage based on the amount dealt to it.
    /// </summary>
    public interface IDamageCountable : IDamageable
    {
        /// <summary>
        /// Called when the entity is damaged by an <see cref="IDamager"/>.
        /// </summary>
        public new virtual void GetDamaged()
        {
            throw new System.NotImplementedException();
        }
    }
}