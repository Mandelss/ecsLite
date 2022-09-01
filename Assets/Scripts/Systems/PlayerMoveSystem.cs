using Commands;
using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class PlayerMoveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private const float MoveDisPerSec = 4;

        private EcsFilter _playersFilter;
        private EcsPool<PlayerComponent> _playersPool;
        private EcsPool<MoveCommand> _moveCommandPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playersFilter = world.Filter<PlayerComponent>().Inc<MoveCommand>().End();

            _playersPool = world.GetPool<PlayerComponent>();
            _moveCommandPool = world.GetPool<MoveCommand>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playersFilter)
            {
                PlayerComponent playerComponent = _playersPool.Get(entity);

                ref MoveCommand moveCommand = ref _moveCommandPool.Get(entity);

                float dis = Vector3.Distance(playerComponent.Transform.position, moveCommand.Destination);
                if (dis <= 0.1)
                {
                    _moveCommandPool.Del(entity);
                    playerComponent.Animator.Play("Idle");
                    return;
                }

                playerComponent.Transform.LookAt(moveCommand.Destination);

                float moveDis = Mathf.Clamp(MoveDisPerSec * Time.fixedDeltaTime, 0, dis);

                Vector3 move = (moveCommand.Destination - playerComponent.Transform.position).normalized * moveDis;

                playerComponent.Transform.Translate(move.x, 0, move.z, Space.World);
                playerComponent.Animator.Play("Move");
            }
        }
    }
}
