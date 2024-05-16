using System;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers
{
    public class TextController : MonoBehaviour
    {
        public TextMeshProUGUI textBox;

        public void SetText(string text)
        {
            textBox.text = text;
        }

        public void ClearText()
        {
            textBox.text = String.Empty;
        }
        
        
        
    }
}