using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UiBar : MonoBehaviour
    {
        public SpriteRenderer[] numbers;

        private Transform _tensLocation;
        private Transform _unitsLocation;

        private readonly Dictionary<int, SpriteRenderer> _tensSprites = new Dictionary<int, SpriteRenderer>();
        private readonly Dictionary<int, SpriteRenderer> _unitsSprites = new Dictionary<int, SpriteRenderer>();

        void Start()
        {
            for (var i = 0; i < numbers.Length; i++)
            {
                var numberSprite = numbers[i];

                var tensClone = Instantiate(numberSprite, _tensLocation.position, Quaternion.identity);
                _tensSprites.Add(i, tensClone);

                var unitsClone = Instantiate(numberSprite, _tensLocation.position, Quaternion.identity);
                _unitsSprites.Add(i, unitsClone);
            }
        }

        public void DisplaySeconds(int secondsToDisplay)
        {
            var ten = secondsToDisplay / 10;
            var unit = secondsToDisplay % 10;

            var tenToDisable = ten + 9 % 10;
            var unitToDisable = unit + 9 % 10;

            _tensSprites[tenToDisable].gameObject.SetActive(false);
            _tensSprites[ten].gameObject.SetActive(true);

            _unitsSprites[unitToDisable].gameObject.SetActive(false);
            _unitsSprites[unit].gameObject.SetActive(true);
        }
    }
}