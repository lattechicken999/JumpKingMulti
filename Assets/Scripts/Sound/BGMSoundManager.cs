using UnityEngine;

public enum eBgmClip
{
    Title, Lobby,InGame, GameClear, _end
}

[RequireComponent(typeof(AudioSource))]
public class BGMSoundManager : Singleton<BGMSoundManager>
{
    [SerializeField] AudioClip[] _bgmClips;

    private AudioSource _audio;
    protected override void Awake()
    {
        base.Awake();
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audio.loop = true;
        _audio.clip = _bgmClips[(int)eBgmClip.Title];
        _audio.Play();
    }

    public void PlayClip(eBgmClip clip)
    {
        _audio.clip = _bgmClips[(int)clip];
        _audio.Play();
    }
    public void BGMSoundVolumChange(float vol)
    {
        _audio.volume = vol;
    }
}
