using AssaultBird2454.VPTU.SoundSystem.Util;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.SoundSystem
{
    class Player
    {
        public System.Threading.Thread Music_Thread;
        //public MediaFoundationReader Audio_Reader;
        public AudioFileReader Audio_Reader;
        public float Volume
        {
            get
            {
                return Audio_Reader.Volume;
            }
            set
            {
                Audio_Reader.Volume = value;
            }
        }
        public int Music_Length = 0;
        public WaveOutEvent Music_WOE;
        public LoopStream LStr;
        public bool Playing = false;

        public void Music_Start(string FilePath)
        {
            if (Music_Playing() == false)
            {
                Music_Thread = new System.Threading.Thread(() => Music_ThreadStart(FilePath));
                Music_Thread.Start();
            }
            else
            {
                Music_Stop();
                Music_Start(FilePath);
            }
        }
        public void Music_Stop()
        {
            try
            {
                Playing = false;
                //Controller.Invoke(new Action(() => Controller.Music.BackColor = System.Drawing.Color.FromArgb(255, 128, 128)));
                //Controller.Invoke(new Action(() => Controller.Music_CurrentPos.ReadOnly = false));
                Audio_Reader.Close();
                Music_WOE.Stop();
                Music_WOE.Dispose();
                Audio_Reader = null;
                Music_Length = 0;
                Music_Thread.Abort();
                Music_Thread = null;
            }
            catch (Exception e)
            {

            }
        }
        public void Music_Pause()
        {
            Music_WOE.Pause();

            //Controller.Invoke(new Action(() => Controller.Music.BackColor = System.Drawing.Color.FromArgb(255, 255, 128)));
            //Controller.Invoke(new Action(() => Controller.Music_CurrentPos.ReadOnly = false));
        }
        public void Music_Resume()
        {
            try
            {
                Music_WOE.Play();
                //Controller.Invoke(new Action(() => Controller.Music.BackColor = System.Drawing.Color.FromArgb(128, 255, 128)));
                //Controller.Invoke(new Action(() => Controller.Music_CurrentPos.ReadOnly = true));
            }
            catch
            {

            }
        }
        public bool Music_Playing()
        {
            if (Music_Thread == null || Music_WOE.PlaybackState == PlaybackState.Stopped)
            {
                return false;
            }
            if (Music_WOE.PlaybackState != PlaybackState.Stopped)
            {
                Playing = true;
                return true;
            }
            return false;
        }
        public int Music_GetPosition()
        {
            if (Music_Playing())
            {
                return Convert.ToInt32(Audio_Reader.Position);
            }
            else
            {
                return 0;
            }
        }
        public void Music_SetPosition(int Pos)
        {
            Audio_Reader.Position = Pos;
        }
        public void Music_Seak(long offset, System.IO.SeekOrigin origin)
        {
            Audio_Reader.Seek(offset, origin);
        }

        private void Music_ThreadStart(string File)
        {
            try
            {
                Music_WOE = new WaveOutEvent();
                Audio_Reader = new AudioFileReader(File);
                LStr = new LoopStream(Audio_Reader, Convert.ToInt32(Audio_Reader.WaveFormat.AverageBytesPerSecond * 0), false);// Replace 0 and False with correct variables
                Music_WOE.Init(LStr);
                Music_WOE.PlaybackStopped += Music_WOE_PlaybackStopped;

                Music_Length = Convert.ToInt32(Audio_Reader.Length);

                Playing = true;
                Music_WOE.Play();
                //Controller.Invoke(new Action(() => Controller.Music.BackColor = System.Drawing.Color.FromArgb(128, 255, 128)));
                //Controller.Invoke(new Action(() => Controller.Music_CurrentPos.ReadOnly = true));
            }
            catch
            {

            }
            while (true)
            {
                //if (Controller.EnableLoop.Checked)
                //{

                //}
            }
        }

        private void Music_WOE_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Music_Stop();
        }
    }
}
