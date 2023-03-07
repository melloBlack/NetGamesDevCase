using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Skills
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] private Image lightFrame;
        [SerializeField] private Image cooldownImage;

        private float _currentCooldown;
        private bool _canClick;
        
        public Button Button { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        private void Start()
        {
            lightFrame.color = Color.white;
            cooldownImage.fillAmount = 0;
            _canClick = true;
        }

        private void NotReadyFeedback()
        {
            Debug.Log("Cooldown not finish");
        }

        public void OnButtonClick(float cooldown)
        {
            StartCoroutine(CoolDownTimer(cooldown));
        }

        public bool CanClick()
        {
            if (!_canClick)
            {
                NotReadyFeedback();
            }

            return _canClick;
        }

        private IEnumerator CoolDownTimer(float cooldown)
        {
            _canClick = false;
            lightFrame.color = Color.gray;
            cooldownImage.fillAmount = 1;
            
            float tempCooldown = cooldown;

            while (cooldown > 0)
            {
                cooldown -= Time.deltaTime;

                cooldownImage.fillAmount = cooldown / tempCooldown;
                
                yield return null;
            }
            
            cooldownImage.fillAmount = 0;
            lightFrame.color = Color.white;
            _canClick = true;
        }
    }
}
