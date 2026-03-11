using UnityEngine;
using Scripts.Entity.Behaviours.Collection;
using Scripts.Entity.Behaviours.Movement;
using Scripts.Entity.Behaviours.Playability;
using Scripts.Entity.Behaviours.Targetability;
using Scripts.Gameplay.Commands;
using Scripts.Gameplay.Commands.Board;

namespace Scripts.Entity.Playables
{
    /// <summary>
    /// Represents the player entity in the game.
    /// </summary>
    public class Player : MonoBehaviour, IDamageCountable, IMovable, ICollector, IPlayable
    {
        #region Fields

        public float Health { get; private set; }

        public float Speed { get; private set; }

        public Vector2 MoveDirection { get; private set; }

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

        public void OnGameCommandPublished(IGameCommand<IGameCommandContext> command, IGameCommandContext context)
        {
            switch (command, context)
            {
                case (MoveCommand moveCommand, MoveCommandContext moveContext):
                    moveCommand.Execute(this, moveContext);
                    break;

                case (AimCommand aimCommand, AimCommandContext aimContext):
                    aimCommand.Execute(this, aimContext);
                    break;
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

        #endregion
    }
}