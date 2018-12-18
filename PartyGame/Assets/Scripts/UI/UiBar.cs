using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.Teams;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UiBar : MonoBehaviour
    {
        public SpriteRenderer[] numbers;
        
        public Transform _tensLocation;
        public Transform _unitsLocation;
        
        private readonly Dictionary<int, SpriteRenderer> _tensSprites = new Dictionary<int, SpriteRenderer>();
        private readonly Dictionary<int, SpriteRenderer> _unitsSprites = new Dictionary<int, SpriteRenderer>();

        public Transform _hotScoreLocation;
        public Transform _coldScoreLocation;

        private readonly Dictionary<int, SpriteRenderer> _coldScoreSprites = new Dictionary<int, SpriteRenderer>();
        private readonly Dictionary<int, SpriteRenderer> _hotScoreSprites = new Dictionary<int, SpriteRenderer>();

        void Start()
        {
            for (var i = 0; i < numbers.Length; i++)
            {
                var numberSprite = numbers[i];

                var tensClone = Instantiate(numberSprite, _tensLocation.position, Quaternion.identity);
                tensClone.gameObject.SetActive(false);
                _tensSprites.Add(i, tensClone);

                var unitsClone = Instantiate(numberSprite, _tensLocation.position, Quaternion.identity);
                unitsClone.gameObject.SetActive(false);
                _unitsSprites.Add(i, unitsClone);

                var hotClone = Instantiate(numberSprite, _hotScoreLocation.position, Quaternion.identity);
                hotClone.gameObject.SetActive(false);
                _hotScoreSprites.Add(i, hotClone);

                var coldClone = Instantiate(numberSprite, _hotScoreLocation.position, Quaternion.identity);
                coldClone.gameObject.SetActive(false);
                _coldScoreSprites.Add(i, coldClone);
            }
        }

        public void SetScore(int score, TeamSide side)
        {
            var scoreToDisable = score - 1;

            switch (side)
            {
                case TeamSide.Cold:
                    _coldScoreSprites[scoreToDisable].gameObject.SetActive(false);
                    _coldScoreSprites[score].gameObject.SetActive(true);
                    break;

                case TeamSide.Hot:
                    _hotScoreSprites[scoreToDisable].gameObject.SetActive(false);
                    _hotScoreSprites[score].gameObject.SetActive(true);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
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