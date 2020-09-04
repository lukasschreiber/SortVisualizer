using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    public class Synthesizer
    {
        private const int SAMPLE_RATE = 41000;
        private const short BITS_PER_SAMPLE = 16;

        public static void Play(float frequency)
        {
            short[] wave = new short[SAMPLE_RATE];
            for (int i = 0; i < SAMPLE_RATE; i++) {
                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(Math.PI * 2 * frequency) / SAMPLE_RATE * i);
            }
        }
    }

 
}
