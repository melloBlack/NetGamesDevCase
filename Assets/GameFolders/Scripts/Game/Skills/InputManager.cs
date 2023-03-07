using GameFolders.Scripts.Core;
using GameFolders.Scripts.Skills;
using UnityEngine;

namespace GameFolders.Scripts.Game.Skills
{
    public class InputManager : MonoSingleton<InputManager>
    {
        [Header("Joystick")] 
        [SerializeField] private DynamicJoystick joystick;
        
        [Header("Skill Buttons")]
        [SerializeField] private SkillButton attackButton;
        [SerializeField] private SkillButton speedUpButton;
        [SerializeField] private SkillButton ultimateButton;
        
        private SkillData _skillData;
        private EventData _eventData;

        private void Awake()
        {
            Singleton();
            _skillData = Resources.Load("Data/SkillData") as SkillData;
            _eventData = Resources.Load("Data/EventData") as EventData;
        }

        private void Start()
        {
            attackButton.Button.onClick.AddListener(ClickedAttackButton);
            speedUpButton.Button.onClick.AddListener(ClickedSpeedUpButton);
            ultimateButton.Button.onClick.AddListener(ClickedUltimateButton);
        }

        private void ClickedAttackButton()
        {
            if (attackButton.CanClick())
            {
                _eventData.OnClickedAttackButton?.Invoke();
                attackButton.OnButtonClick(_skillData.GetAttackCooldown());
            }
        }
        private void ClickedSpeedUpButton()
        {
            if (speedUpButton.CanClick())
            {
                _eventData.OnClickedSpeedUpButton?.Invoke();
                speedUpButton.OnButtonClick(_skillData.GetSpeedUpCooldown());
            }
        }
        private void ClickedUltimateButton()
        {
            if (ultimateButton.CanClick())
            {
                _eventData.OnClickedUltimateButton?.Invoke();
                ultimateButton.OnButtonClick(_skillData.GetUltimateCooldown());
            }
        }

        public Vector2 GetDirection()
        {
            return joystick.Direction;
        }
        
        public void SetUltimateButtonImages()
        {
            
        }
    }
}
