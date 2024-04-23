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
        
        [SerializeField] public float mentalStamina;
        [SerializeField] private float maxMentalStamina;
        [SerializeField] private float mentalMultiplier;

        [SerializeField] private Image mentalHealthImage;
        
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

        public void IncreaseMentalHealth()
        {
            if (mentalStamina <= maxMentalStamina)
            {
                mentalStamina += mentalMultiplier * Time.deltaTime;
                //Debug.Log(mentalStamina+"IncreaseMentalHealth");
                Color mentalAlpha = mentalHealthImage.color;
                mentalAlpha.a = 0.2f - mentalStamina;
                mentalHealthImage.color = mentalAlpha;
            }
        }
        
        public void DecreaseMentalHealth()
        {
            if (mentalStamina >= 0)
            {
                mentalStamina -= mentalMultiplier * Time.deltaTime;
                //Debug.Log(mentalStamina+"DecreaseMentalHealth");
                
                Color mentalAlpha = mentalHealthImage.color;
                mentalAlpha.a = 0.2f - mentalStamina;
                mentalHealthImage.color = mentalAlpha;
            }
        }
        
    }
}