using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private float _musicVolumn, _sfxVolumn, _voiceOverVolumn ;
        [SerializeField] private AudioSource _musicAudioSource, _sfxAudioSource, _voiceOverAudioSource;

        [Header("BgMusic")]
        [SerializeField] AudioClip[] _bGAudioClips;

        [Header("SfxMusic")]
        [SerializeField] AudioClip[] _sfxAudioClips;

        public void PlayMusic(BgMusicId bGMusicID) {
            _musicAudioSource.clip= _bGAudioClips[(int)bGMusicID];
            _musicAudioSource.Play();
        }


        /// <summary>
        /// Plays one shot Sound with sfxaudio source
        /// </summary>
        /// <param name="sfxMusicId"> Plays one shot Sound with sfxaudio source this</param>
        public void PlaySfx(SfxMusicId sfxMusicId) {
            _sfxAudioSource.PlayOneShot(_sfxAudioClips[(int)sfxMusicId]);
        }

        public void PlaySfx(SfxMusicId sfxMusicId, AudioSource audioSource)
        {
            audioSource.volume = _sfxVolumn;
            audioSource.PlayOneShot(_sfxAudioClips[(int)sfxMusicId]);
        }

        public void PlaySfx(SfxMusicId sfxMusicId, AudioSource audioSource, float volumn)
        {
            audioSource.volume = volumn;
            audioSource.PlayOneShot(_sfxAudioClips[(int)sfxMusicId]);
        }

        public void SetVolmunSettings(float Mvolumn, float Svolumn, float Vvolumn) {
            _musicVolumn = Mvolumn;
            _sfxVolumn = Svolumn;
            _voiceOverVolumn = Vvolumn;
            _musicAudioSource.volume = _musicVolumn;
            _sfxAudioSource.volume = _sfxVolumn;
            _voiceOverAudioSource.volume = _voiceOverVolumn;
        }

        public void MuteVolumn(bool status) {
            _musicAudioSource.mute = status;
            _sfxAudioSource.mute = status;
            _voiceOverAudioSource.mute = status;
        }

        public void MuteVolumn(bool status, AudioSourceId ID)
        {
            switch (ID) {
                case AudioSourceId.MusicAudioSource:
                    _musicAudioSource.mute = status;
                    break;

                case AudioSourceId.SFXAudioSource:
                    _sfxAudioSource.mute = status;
                    break;

                case AudioSourceId.VoiceOverAudioSource:
                    _voiceOverAudioSource.mute = status;
                    break;

                default:
                    Debug.Log("Warning : Audio Source Does not contain :" + ID);
                    break;
            }
        }

    }

    public enum BgMusicId { 
        HorrorSoft = 0,
        HorrorHard = 1
    }

    public enum SfxMusicId
    {
        HorrorSoft = 0,
        HorrorHard = 1
    }
    public enum AudioSourceId
    {
        MusicAudioSource=1,
        SFXAudioSource=2,
        VoiceOverAudioSource=3
    }
}