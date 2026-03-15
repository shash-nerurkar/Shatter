using UnityEngine;
using Scripts.Entity.Behaviours.Collection;
using Scripts.Entity.Behaviours.Movement;
using Scripts.Entity.Behaviours.Targetability;
using Scripts.Gameplay.Commands;
using Scripts.Gameplay.Commands.Board;

namespace Scripts.Entity.Playables
{
    /// <summary>
    /// Represents the player entity in the game.
    /// </summary>
    public class Player : MonoBehaviour, IMovable, IAimable, IDamageCountable, ICollector
    {
        #region Fields

        public Vector3 CurrentPosition => transform.position;

        public Vector2 MoveDirection { get; private set; }

        public Vector2 AimDirection { get; private set; }

        public float Health { get; private set; }

        public float Speed { get; private set; }

        #endregion


        #region Methods

        private void Awake()
        {
            GameCommandBus.OnCommandPublished += OnGameCommandPublished;
        }

        private void OnDestroy()
        {
            GameCommandBus.OnCommandPublished -= OnGameCommandPublished;
        }

        public void OnGameCommandPublished(IGameCommandContext context)
        {
            switch (context)
            {
                case MoveCommandContext moveContext:
                    new MoveCommand().Execute(this, moveContext);
                    return;

                case AimCommandContext aimContext:
                    new AimCommand().Execute(this, aimContext);
                    return;
            }
        }

        public void Collect()
        {
            throw new System.NotImplementedException();
        }

        public void GetDamaged()
        {
            throw new System.NotImplementedException();
        }

        public void GetTargeted()
        {
            throw new System.NotImplementedException();
        }

        public void Move(Vector2 direction)
        {
            MoveDirection = direction;
        }

        public void Aim(Vector2 direction)
        {
            AimDirection = direction;
        }

        #endregion
    }
}