using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace Securitybot
{
    internal class AudioGreeting
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("welcome.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
