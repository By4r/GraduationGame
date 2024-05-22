using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class ItemProgressBar : MonoBehaviour
    {
        [SerializeField] private float time = 0;
        [SerializeField] private float maxTime = 3;
        [SerializeField] private Image fillImage;
        

        private void Start()
        {
            SetAlpha(0);
        }

        internal void ProgressBar()
        {
            time += Time.deltaTime;
            fillImage.fillAmount = time / maxTime;
            Debug.Log(time + maxTime+"rrrrrrrrrrrr");
            if (time < 0 )
            {
                time = 0;
                SetAlpha(0);
            }

            if (time >= maxTime)
            {
                SetAlpha(0);
            }else SetAlpha(1);
            
        }

       

        private void SetAlpha(float alpha)
        {
            Color color = fillImage.color;
            color.a = alpha;
            fillImage.color = color;
        }
    }
}