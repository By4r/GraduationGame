using System;

namespace Runtime.Controllers
{
    [Serializable]
    public class VoiceOverLine
    {
        public string key;
        public string[] line;
        public float[] duration; // Altyazı süresi (saniye cinsinden)
    }

//text joson dosyasını burada temsil edecek
}