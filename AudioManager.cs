using System;
using System.Media;
using System.IO;

namespace CyberBotPart2
{
    public class AudioManager
    {
        public static void PlayVoiceGreeting()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                if (File.Exists(path))
                {
                    using (SoundPlayer player = new SoundPlayer(path))
                    {
                        player.Play();
                    }
                }
            }
            catch (Exception ex)
            {
                // Silent fail - program continues without voice
                System.Diagnostics.Debug.WriteLine("Voice error: " + ex.Message);
            }
        }
    }
}