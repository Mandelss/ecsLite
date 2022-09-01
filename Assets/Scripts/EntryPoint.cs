using Entities;
using Leopotam.EcsLite;
using Systems;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private EcsWorld _world;

    private IEcsSystems _systemsFixed;
    private IEcsSystems _systemsUpdate;

    private void Start()
    {
        _world = new EcsWorld();
        _systemsFixed = new EcsSystems(_world);
        _systemsUpdate = new EcsSystems(_world);

        _systemsUpdate
            .Add(new PlayerInitSystem(_player))
            .Add(new ButtonsInitSystem(FindObjectsOfType<Button>()))
            .Add(new DoorsInitSystem(FindObjectsOfType<Door>()))
            .Add(new MouseInputSystem())
            .Init();

        _systemsFixed
            .Add(new DoorMoveSystem())
            .Add(new PlayerMoveSystem())
            .Init();
    }

    private void Update() {
        _systemsUpdate?.Run();
    }

    private void FixedUpdate() {
        _systemsFixed?.Run();
    }
}
