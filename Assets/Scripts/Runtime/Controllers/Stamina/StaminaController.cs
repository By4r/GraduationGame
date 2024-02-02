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
        [SerializeField] public Image image1;
        
        [SerializeField] public float mentalStamina;
        [SerializeField] private float maxMentalStamina;
        [SerializeField] private float mentalMultiplier;

        [SerializeField] private Image mentalHealthImage;
        [SerializeField] public float mentalImageMultiplier;
        
        //[SerializeField] private PlayerMovementController playerMovementController;
        #endregion
        
        #endregion
        

        void Start()
        {
            image1.fillAmount = maxSprintStamina;
        }

        public void DecreaseStamina()
        {
            if (sprintStamina >= 0 )
            {
                sprintStamina -= sprintMultiplier * Time.deltaTime;
                image1.fillAmount = sprintStamina;
            }
        }
        

        public void IncreaseStamina()
        {
            if (sprintStamina <= maxSprintStamina)
            {
                sprintStamina += sprintMultiplier * Time.deltaTime;
                image1.fillAmount = sprintStamina;
            }
        }

        public void IncreaseMentalHealth()
        {
            if (mentalStamina <= maxMentalStamina)
            {
                mentalStamina += mentalMultiplier * Time.deltaTime;
                //Debug.Log(mentalStamina+"IncreaseMentalHealth");
                Color mentalAlpha = mentalHealthImage.color;
                mentalAlpha.a = 0.8f - mentalStamina;//(mentalStamina / maxMentalStamina)*Time.deltaTime*mentalImageMultiplier;
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
                mentalAlpha.a = 0.8f - mentalStamina;//1 - (mentalStamina / maxMentalStamina);
                mentalHealthImage.color = mentalAlpha;
            }
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onDecreaseStamina += DecreaseStamina;
            PlayerSignals.Instance.onIncreaseStamina += IncreaseStamina;
            PlayerSignals.Instance.onIncreaseMentalHealth += IncreaseMentalHealth;
            PlayerSignals.Instance.onDecreaseMentalHealth += DecreaseMentalHealth;
            
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onDecreaseStamina -= DecreaseStamina;
            PlayerSignals.Instance.onIncreaseStamina -= IncreaseStamina;
            PlayerSignals.Instance.onIncreaseMentalHealth -= IncreaseMentalHealth;
            PlayerSignals.Instance.onDecreaseMentalHealth -= DecreaseMentalHealth;
        }
    }
}