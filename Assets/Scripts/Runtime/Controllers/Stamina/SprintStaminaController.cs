using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Runtime.Controllers.Stamina
{
    public class SprintStaminaController : MonoBehaviour
    {
        #region Self Variables
        
        #region Serialized Variables
        
        [SerializeField] public float stamina;
        [SerializeField] private float maxStamina;
        [SerializeField] private float dValue;
        [SerializeField] public Image image1;
        #endregion
        
        #endregion
        

        void Start()
        {
            image1.fillAmount = maxStamina;
        }

        public void DecreaseStamina()
        {
            if (stamina >= 0)
            {
                stamina -= dValue * Time.deltaTime;
                image1.fillAmount = stamina;
            }
        }

        public void IncreaseStamina()
        {
            if (stamina <= maxStamina)
            {
                stamina += dValue * Time.deltaTime;
                image1.fillAmount = stamina;
            }
        }
    }
}