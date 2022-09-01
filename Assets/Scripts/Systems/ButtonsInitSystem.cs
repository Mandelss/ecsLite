using Commands;
using Components;
using Entities;
using Leopotam.EcsLite;

namespace Systems
{
    public class ButtonsInitSystem : IEcsInitSystem
    {
        private readonly Button[] _buttons;

        public ButtonsInitSystem(Button[] buttons)
        {
            _buttons = buttons;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsPool<ButtonComponent> buttonsPool = world.GetPool<ButtonComponent>();
            EcsPool<ButtonTriggerCommand> buttonTriggerPool = world.GetPool<ButtonTriggerCommand>();

            foreach (Button button in _buttons)
            {
                int buttonEntity = world.NewEntity();

                ref ButtonComponent buttonComponent = ref buttonsPool.Add(buttonEntity);
                buttonComponent.Id = button._id;

                button.Entity = buttonEntity;
                button.TriggerPool = buttonTriggerPool;
            }
        }
    }
}
