using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        public int _id;

        public void Awake()
        {
            Color color = _id switch
            {
                1 => Color.blue,
                2 => Color.magenta,
                3 => Color.green,
                4 => Color.yellow,
                5 => Color.cyan,
                _ => Color.white
            };

            gameObject.GetComponent<Renderer>().material.color = color;
        }
    }
}
