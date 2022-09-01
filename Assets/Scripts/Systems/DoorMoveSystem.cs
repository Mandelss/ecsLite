using Commands;
using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class DoorMoveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private static readonly Vector3 MoveDisPerFrame = Vector3.down / 100;

        private EcsFilter _doorsFilter;
        private EcsFilter _buttonsFilter;

        private EcsPool<DoorComponent> _doorsPool;
        private EcsPool<ButtonComponent> _buttonsPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _doorsFilter = world.Filter<DoorComponent>().End();
            _buttonsFilter = world.Filter<ButtonComponent>().Inc<ButtonTriggerCommand>().End();

            _doorsPool = world.GetPool<DoorComponent>();
            _buttonsPool = world.GetPool<ButtonComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int buttonEntity in _buttonsFilter)
            {
                ButtonComponent buttonComponent = _buttonsPool.Get(buttonEntity);

                foreach (int doorEntity in _doorsFilter)
                {
                    DoorComponent doorComponent = _doorsPool.Get(doorEntity);

                    if (doorComponent.Id == buttonComponent.Id &&
                        doorComponent.Transform.position.y > -1)
                    {
                        doorComponent.Transform.position += MoveDisPerFrame;
                    }
                }
            }
        }
    }
}
