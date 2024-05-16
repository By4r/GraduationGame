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
        private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        public string resourceFolder = "subtitles";

        public string[] GetText(string textKey)
        {
            string[] tmp = new string[] { };
            if (lines.TryGetValue(textKey,out tmp))
            {
                return tmp;
            }

            return new string[] {"missing text for" + textKey};
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
                lines[t.key] = t.line;
            }
            
        }
        
    }
}
