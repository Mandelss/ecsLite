using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly GameObject _player;

        public PlayerInitSystem(GameObject player)
        {
            _player = player;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsPool<PlayerComponent> playersPool = world.GetPool<PlayerComponent>();

            int playerEntity = world.NewEntity();

            ref PlayerComponent playerComponent = ref playersPool.Add(playerEntity);

            playerComponent.Transform = _player.transform;
            playerComponent.Animator = _player.GetComponent<Animator>();
        }
    }
}
