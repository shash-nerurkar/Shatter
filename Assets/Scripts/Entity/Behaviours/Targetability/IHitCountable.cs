namespace Scripts.Entity.Behaviours.Targetability
{
    /// <summary>
    /// An entity that takes damage based on the number of times it is hit
    /// </summary>
    public interface IHitCountable : IDamageable
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