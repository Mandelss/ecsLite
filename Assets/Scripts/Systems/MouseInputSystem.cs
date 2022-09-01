using Commands;
using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    internal sealed class MouseInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _playersFilter;
        private EcsPool<MoveCommand> _moveCommandPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playersFilter = world.Filter<PlayerComponent>().End();

            _moveCommandPool = world.GetPool<MoveCommand>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playersFilter)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100) &&
                        hit.transform.gameObject.CompareTag("plane"))
                    {
                        _moveCommandPool.Del(entity);
                        ref MoveCommand moveCommand = ref _moveCommandPool.Add(entity);
                        moveCommand.Destination = hit.point;
                    }
                }
            }

        }
    }
}
