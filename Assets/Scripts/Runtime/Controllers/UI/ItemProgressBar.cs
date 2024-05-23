using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class ItemProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        public void UpdateProgress(float progress)
        {
            if (progressBar != null)
            {
                progressBar.fillAmount = progress;
            }
        }

        public void ResetProgress()
        {
            if (progressBar != null)
            {
                progressBar.fillAmount = 0f;
            }
        }
    }
}