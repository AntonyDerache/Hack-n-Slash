using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SFX
{
    Default,
}

public enum Music
{
    MainMenu,
    InGame
}

[Serializable]
struct AudioItem<T>
{
    public T key;
    public AudioClip audioClip;
}

public class AudioManager : Singleton<AudioManager>
{
    private Dictionary<SFX, AudioClip> _sfxRepository = new Dictionary<SFX, AudioClip>();
    private Dictionary<Music, AudioClip> _musicRepository = new Dictionary<Music, AudioClip>();

    [SerializeField] private AudioMixer _mixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    [Header("Repositories")]
    [SerializeField] private List<AudioItem<SFX>> _sfxList;
    [SerializeField] private List<AudioItem<Music>> _musicList;

    protected override void Awake()
    {
        if (instance == null) {
            foreach (AudioItem<SFX> SFXItem in _sfxList) {
                if (SFXItem.audioClip == null)
                    continue;
                _sfxRepository.Add(SFXItem.key, SFXItem.audioClip);
            }
            foreach (AudioItem<Music> musicItem in _musicList) {
                if (musicItem.audioClip == null)
                    continue;
                _musicRepository.Add(musicItem.key, musicItem.audioClip);
            }
        }
        base.Awake();
    }

    public void PlaySoundEffect(SFX SFX_ID)
    {
        if (_sfxRepository.ContainsKey(SFX_ID)) {
            _sfxAudioSource.clip = _sfxRepository[SFX_ID];
            _sfxAudioSource.PlayOneShot(_sfxAudioSource.clip);
        }
    }

    public void PlayMusic(Music musicID)
    {
        if (_musicRepository.ContainsKey(musicID)) {
            if (_musicAudioSource.clip != _musicRepository[musicID]) {
                _musicAudioSource.clip = _musicRepository[musicID];
                _musicAudioSource.Play();
            }
        }
    }

    public void PauseCurrentMusic()
    {
        if (_musicAudioSource.clip != null && _musicAudioSource.isPlaying) {
            _musicAudioSource.Pause();
        }
    }

    public void ResumeCurrentMusic()
    {
        if (_musicAudioSource.clip != null && !_musicAudioSource.isPlaying) {
            _musicAudioSource.Play();
        }
    }

    public void MuteMusic(bool mute)
    {
        if (mute) {
            SetMusicVolume(-80);
        } else {
            SetMusicVolume(0);
        }
    }

    public void MuteSfx(bool mute)
    {
        if (mute) {
            SetSfxVolume(-80);
        } else {
            SetSfxVolume(0);
        }
    }

    public void MuteMusic()
    {
        _mixer.GetFloat("MusicVolume", out float currentVolume);
        if (currentVolume == -80) {
            MuteMusic(false);
        } else {
            MuteMusic(true);
        }
    }

    public void MuteSfx()
    {
        _mixer.GetFloat("SfxVolume", out float currentVolume);
        if (currentVolume == -80) {
            MuteSfx(false);
        } else {
            MuteSfx(true);
        }
    }

    public void SetMusicVolume(float value)
    {
        _mixer.SetFloat("MusicVolume", value);
    }

    public void SetSfxVolume(float value)
    {
        _mixer.SetFloat("SfxVolume", value);
    }

    public void SetMasterVolume(float value)
    {
        _mixer.SetFloat("MasterVolume", value);
    }
}