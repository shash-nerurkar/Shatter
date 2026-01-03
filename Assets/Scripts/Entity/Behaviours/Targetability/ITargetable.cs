namespace Scripts.Entity.Behaviours.Targetability
{
    /// <summary>
    /// An entity that can be targeted.
    /// </summary>
    public interface ITargetable
    {
        /// <summary>
        /// Called when the entity is targeted.
        /// </summary>
        public void GetTargeted();
    }
}