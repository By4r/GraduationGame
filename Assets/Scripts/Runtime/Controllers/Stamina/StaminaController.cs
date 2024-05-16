using UnityEngine;
using Image = UnityEngine.UI.Image;
using Runtime.Signals;
using Runtime.Controllers.Player;

namespace Runtime.Controllers.Stamina
{
    public class StaminaController : MonoBehaviour
    {
        #region Self Variables
        
        #region Serialized Variables
        
        [SerializeField] public float sprintStamina;
        [SerializeField] private float maxSprintStamina;
        [SerializeField] private float sprintMultiplier;
        [SerializeField] public Image staminaImage;
        
        #endregion
        
        #endregion
        

        void Start()
        {
            staminaImage.fillAmount = maxSprintStamina;
        }

        public void DecreaseStamina()
        {
            if (sprintStamina >= 0 )
            {
                sprintStamina -= sprintMultiplier * Time.deltaTime;
                staminaImage.fillAmount = sprintStamina;
            }
        }
        

        public void IncreaseStamina()
        {
            if (sprintStamina <= maxSprintStamina)
            {
                sprintStamina += sprintMultiplier * Time.deltaTime;
                staminaImage.fillAmount = sprintStamina;
            }
        }
        
    }
}