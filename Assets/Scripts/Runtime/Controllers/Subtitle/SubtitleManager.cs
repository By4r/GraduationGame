using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Controllers
{
    public class SubtitleManager : MonoBehaviour
    {
        private Dictionary<string, VoiceOverLine> lines = new Dictionary<string, VoiceOverLine>(StringComparer.OrdinalIgnoreCase);
        public string resourceFolder = "subtitles";

        public (string[], float[]) GetTextWithDurations(string textKey)
        {
            if (lines.TryGetValue(textKey, out var tmp))
            {
                return (tmp.line, tmp.duration);
            }

            Debug.LogError("Subtitle not found for key: " + textKey);
            return (new string[] { "Missing text for " + textKey }, new float[] { 2.0f });
        }

        private void Awake()
        {
            var textAsset = Resources.Load<TextAsset>(resourceFolder + "/subtitles");
            if (textAsset == null)
            {
                Debug.LogError("Subtitles text asset not found!");
                return;
            }

            var voText = JsonUtility.FromJson<VoiceOverText>(textAsset.text);

            foreach (var t in voText.lines)
            {
                lines[t.key] = t;
            }
        }
    }
}