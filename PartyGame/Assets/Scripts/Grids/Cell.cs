using UnityEngine;

namespace Assets.Scripts.Grids
{
    public class Cell : MonoBehaviour
    {
        public int Row;

        public int Column;

        public Collider2D Collider { get; private set; }

        void Start()
        {
            Collider = this.GetComponent<Collider2D>();
            if (Collider == null)
            {
                Debug.LogError($"No {nameof(UnityEngine.Collider)} is attached to {this}.");
            }
        }


        public override string ToString()
        {
            return "[" + this.name + " Row " + Row + " Col " + Column + "]";
        }
    }
}