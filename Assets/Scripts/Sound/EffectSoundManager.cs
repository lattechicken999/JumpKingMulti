using System.Collections;
using UnityEngine;

public enum eUISound
{
    Ok, Error, _end
}

public enum eCharacterEffectSound
{
    Jump, Walk, Collision, _end
}
[RequireComponent(typeof(AudioSource))]
public class EffectSoundManager : Singleton<EffectSoundManager>
{
    [Header("UI Sound Clip")]
    [SerializeField] AudioClip[] _uiClips;

    [Header("Character Effect Clip")]
    [SerializeField] AudioClip[] _effectClip;

    private AudioSource _audio;
    private WaitForSeconds _sleep;
    private Coroutine _walkCoroutine;

    protected override void  Awake()
    {
        base.Awake();
        _audio = GetComponent<AudioSource>();
        _sleep = new WaitForSeconds(0.8f);
    }

    public void PlayClickSound()
    {
        _audio.PlayOneShot(_uiClips[(int)eUISound.Ok]);
    }
    public void PlayErrorSound()
    {
        _audio.PlayOneShot(_uiClips[(int)eUISound.Error]); 
    }
    public void PlayJumpSound()
    {
        _audio.PlayOneShot(_effectClip[(int)eCharacterEffectSound.Jump]);
    }
    public void PlayCollisionSound()
    {
        _audio.PlayOneShot(_effectClip[(int)eCharacterEffectSound.Collision]);
    }
    public void StartPlayWalkSound()
    {
        if(_walkCoroutine == null)
            _walkCoroutine = StartCoroutine(PlayWalkSound()); 
    }
    public void StopPlayWalkSound()
    {
        if (_walkCoroutine != null)
            StopCoroutine(_walkCoroutine);
    }

    private IEnumerator PlayWalkSound()
    {
        while(true)
        {
            _audio.PlayOneShot(_effectClip[(int)eCharacterEffectSound.Walk]);
            yield return _sleep;
        }
       
    }
}
