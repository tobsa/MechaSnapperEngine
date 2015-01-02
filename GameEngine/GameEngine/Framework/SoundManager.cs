using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace GameEngine.Framework
{
    public class SoundManager
    {
        private Dictionary<string, Song> songs = new Dictionary<string, Song>();
        private Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>(); 

        private static SoundManager soundManager;
        private float masterVolume;       

        private SoundManager() {
            MediaPlayer.IsRepeating = true;
        }
        public static SoundManager Instance
        {
            get
            {
                if (soundManager == null)
                    soundManager = new SoundManager();

                return soundManager;
            }
        }
    
        public void SetVolume(float newMasterVolume)
        {
            // nödvändig?
            if (newMasterVolume > 1.0f)
                newMasterVolume = 1.0f;
            if (newMasterVolume < 0.0f)
                newMasterVolume = 0.0f;

            masterVolume = newMasterVolume;
            SoundEffect.MasterVolume = newMasterVolume;
        }

        public void PauseSong()
        {
            MediaPlayer.Pause();
        }

        public void ResumeSong()
        {
            MediaPlayer.Resume();
        }

        public void MuteAllAudio()
        {
            SoundEffect.MasterVolume = 0.0f;
        }

        public void PlayAllAudio()
        {
            SoundEffect.MasterVolume = masterVolume;
        }


        public void PlaySong(string song)
        {
            try
            {
                MediaPlayer.Play(songs[song]);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }

        public void PlaySoundEffect(string soundEffect)
        {
            try
            {
                soundEffects[soundEffect].Play();
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }

        public void LoadSong(string songName, Song song)
        {
                songs.Add(songName, song);
        }

        public void LoadSoundEffect(string soundEffectName, SoundEffect soundEffect)
        {
            soundEffects.Add(soundEffectName, soundEffect);
        }

        
    }
}
