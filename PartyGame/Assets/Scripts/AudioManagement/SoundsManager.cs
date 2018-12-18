using System;
using Assets.Scripts.AudioManagement;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public AudioClip takeTile;
    public AudioClip dropTile;
    public AudioClip scorePoint;
    public AudioClip tileSpawn;
    public AudioClip winGame;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError($"No {nameof(AudioSource)} is attached to {nameof(SoundsManager)}");
            return;
        }
    }
    
    public void Play(SoundName soundName)
    {
        switch (soundName)
        {
            case SoundName.TakeTile:
                _audioSource.PlayOneShot(takeTile);
                break;

            case SoundName.DropTile:
                _audioSource.PlayOneShot(dropTile);
                break;

            case SoundName.ScorePoint:
                _audioSource.PlayOneShot(scorePoint);
                break;

            case SoundName.TileSpawn:
                _audioSource.PlayOneShot(tileSpawn);
                break;

            case SoundName.WinGame:
                _audioSource.PlayOneShot(winGame);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(soundName), soundName, null);
        }
    }
}
