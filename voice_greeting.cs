
using System.Media;

namespace CyberBot_Part_Two
{//start of namespace
    public class voice_greeting
    {//start of class
        // Constructor
        public voice_greeting()
        {//start of constructor
            PlaySound();
        }//end of constructor

        // Play sound method
        public void PlaySound()
        {//start of method
            try
            {// Create a SoundPlayer instance and play the sound
                SoundPlayer player = new SoundPlayer("greeting.wav");

                player.Play();
            }//end of try
            catch
            {// Handle exceptions (e.g., file not found)



            }//end of catch
        }//end of method
    }//end of class
}// end of namespace