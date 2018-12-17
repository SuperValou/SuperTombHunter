using UnityEngine;

namespace Assets.Scripts
{
    public class Cell : MonoBehaviour
    {
        public int Row;

        public int Column;

        private Collider2D _collider;

        void Start()
        {
            _collider = this.GetComponent<Collider2D>();
            if (_collider == null)
            {
                Debug.LogError($"No {nameof(Collider)} is attached to {this}.");
            }
        }


        public override string ToString()
        {
            return "[" + this.name + " Row " + Row + " Col " + Column + "]";
        }
    }
}