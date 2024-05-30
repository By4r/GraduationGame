using System;

namespace Runtime.Controllers
{
    [Serializable]
    public class VoiceOverLine
    {
        public string key;
        public string[] line;
        public float[] duration;
    }

    [Serializable]
    public class VoiceOverText
    {
        public VoiceOverLine[] lines;
    }
}