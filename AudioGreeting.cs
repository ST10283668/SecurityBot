using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace Securitybot
{
    internal class AudioGreeting
    {
        public static void PLayGreeting() 
        {
         SoundPlayer player = new SoundPlayer("welcome.wav");
         player.Play();
            

        }
    }
}
