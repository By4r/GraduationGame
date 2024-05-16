using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Runtime.Controllers
{
    public class SubtitleManager : MonoBehaviour
    {
       // private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
       private Dictionary<string, VoiceOverLine> lines = new Dictionary<string, VoiceOverLine>(StringComparer.OrdinalIgnoreCase);

        public string resourceFolder = "subtitles";

        // public string[] GetText(string textKey)
        // {
        //     string[] tmp = new string[] { };
        //     if (lines.TryGetValue(textKey,out tmp))
        //     {
        //         return tmp;
        //     }
        //
        //     return new string[] {"missing text for" + textKey};
        // }
        public (string[], float[]) GetTextWithDurations(string textKey)
        {
            string[] tmpLines = new string[] { };
            float[] tmpDurations = new float[] { };

            if (lines.TryGetValue(textKey, out var tmp))
            {
                tmpLines = tmp.line;
                tmpDurations = tmp.duration;
            }

            return (tmpLines, tmpDurations);
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

        
        // private void Awake()
        // {
        //     
        //     var textAsset = Resources.Load<TextAsset>(resourceFolder + "/subtitles");
        //     if (textAsset == null)
        //     {
        //         Debug.LogError("Subtitles text asset not found!");
        //         return;
        //     }
        //     var voText = JsonUtility.FromJson<VoiceOverText>(textAsset.text);
        //     
        //     foreach (var t in voText.lines)
        //     {
        //         lines[t.key] = t.line;
        //     }
        //     
        // }
        
    }
}
