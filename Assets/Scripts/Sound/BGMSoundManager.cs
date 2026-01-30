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
        Debug.Log(" 호출 됨");
        
        if (PlayerPrefs.HasKey("Volume"))
        {
            _audio.volume = PlayerPrefs.GetFloat("Volume");
            Debug.Log(_audio.volume);
        }
        if (PlayerPrefs.HasKey("Mute"))
        {
            SetBGMMuteState(PlayerPrefs.GetInt("Mute") == 1);
        }
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
        PlayerPrefs.SetFloat("Volume", vol);
    }
    public float GetBGMSoundVolume()
    {
        return _audio.volume;
    }
    public bool GetBGMMuteState()
    {
        return _audio.mute;
    }
    public void SetBGMMuteState(bool m)
    {
        _audio.mute = m;
    }
}
