using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public class DoubleDigitsDisplay : MonoBehaviour
    {
        public SpriteRenderer[] numbers;

        private readonly Dictionary<int, SpriteRenderer> _tensSprites = new Dictionary<int, SpriteRenderer>();
        private readonly Dictionary<int, SpriteRenderer> _unitsSprites = new Dictionary<int, SpriteRenderer>();

        private void Awake()
        {
            for (var i = 0; i < numbers.Length; i++)
            {
                var numberSprite = numbers[i];

                Vector3 tensLocation = gameObject.transform.position + new Vector3(numberSprite.size.x * -1, 0.0f, 0.0f);
                var tensClone = Instantiate(numberSprite, tensLocation, Quaternion.identity, gameObject.transform);
                tensClone.gameObject.SetActive(false);
                _tensSprites.Add(i, tensClone);

                var unitsLocation = gameObject.transform.position;
                var unitsClone = Instantiate(numberSprite, unitsLocation, Quaternion.identity, gameObject.transform);
                unitsClone.gameObject.SetActive(false);
                _unitsSprites.Add(i, unitsClone);
            }
        }
        
        public void UpdateValue(int _value)
        {
            var ten = _value / 10;
            var unit = _value % 10;

            DisableAll();

            _tensSprites[ten].gameObject.SetActive(true);
            _unitsSprites[unit].gameObject.SetActive(true);
        }

        private void DisableAll()
        {
            foreach (var entry in _tensSprites)
                entry.Value.gameObject.SetActive(false);
            
            foreach (var entry in _unitsSprites)
                entry.Value.gameObject.SetActive(false);
        }
    }
}