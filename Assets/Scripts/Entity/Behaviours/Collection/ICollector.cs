namespace Scripts.Entity.Behaviours.Collection
{
    /// <summary>
    /// An entity that collects <see cref="ICollectible"/> entities.
    /// </summary>
    public interface ICollector
    {
        /// <summary>
        /// Collects an <see cref="ICollectible"/>.
        /// </summary>
        public void Collect();
    }
}