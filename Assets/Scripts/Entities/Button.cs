using Commands;
using Leopotam.EcsLite;
using UnityEngine;

namespace Entities
{
    public class Button : Entity
    {
        public int Entity { get; set; }
        public EcsPool<ButtonTriggerCommand> TriggerPool { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            TriggerPool.Add(Entity);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerPool.Del(Entity);
        }
    }
}
