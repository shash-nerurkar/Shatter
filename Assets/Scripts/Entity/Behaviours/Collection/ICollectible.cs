namespace Scripts.Entity.Behaviours.Collection
{
    /// <summary>
    /// An entity that gets collected by an <see cref="ICollector"/>.
    /// </summary>
    public interface ICollectible
    {
        /// <summary>
        /// Called when this collectible is collected.
        /// </summary>
        public void GetCollected();
    }
}