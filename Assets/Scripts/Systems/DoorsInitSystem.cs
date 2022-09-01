using Components;
using Entities;
using Leopotam.EcsLite;

namespace Systems
{
    public class DoorsInitSystem : IEcsInitSystem
    {
        private readonly Door[] _doors;

        public DoorsInitSystem(Door[] doors)
        {
            _doors = doors;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsPool<DoorComponent> doorsPool = world.GetPool<DoorComponent>();

            foreach (Door door in _doors)
            {
                int doorEntity = world.NewEntity();

                ref DoorComponent doorComponent = ref doorsPool.Add(doorEntity);

                doorComponent.Id = door._id;
                doorComponent.Transform = door.transform;
            }
        }
    }
}
