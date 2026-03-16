using Scripts.Entity.Behaviours.Movement;

namespace Scripts.Entity.Behaviours.Damaging
{
    /// <summary>
    /// An entity that is fired to cause damage.
    /// </summary>
    public interface IShootable : IDamager, IMovable
    {
        
    }
}