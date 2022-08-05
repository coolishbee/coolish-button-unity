using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine;

namespace CoolishUI
{
    public class CoolishButton : Selectable, IPointerClickHandler
    {
        public enum Sound
        {
            none,

            click_1,
            click_2,
        }
        public Sound sound = Sound.click_1;
        public bool pressScaling = true;
        Vector3 orgScale = new Vector3(1f, 1f, 1f);

        [Serializable]
        public class ButtonClickedEvent : UnityEvent { }

        // Event delegates triggered on click.
        [FormerlySerializedAs("onClick")]
        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

        protected CoolishButton()
        {
        }

        public ButtonClickedEvent onClick
        {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }

        static public void PlaySound(Sound snd)
        {
            if (snd != Sound.none)
            {
                switch (snd)
                {
                    case Sound.click_1: SoundManager.Play("click1"); break;
                    case Sound.click_2: SoundManager.Play("click2"); break;
                }
            }
        }

        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            PlaySound(sound);
            Press();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (pressScaling)
            {                
                if (IsPressed())
                {
                    float duration = 0.05f;
                    Vector3 vScale = gameObject.transform.localScale * 1.05f;

                    iTween.ScaleTo(gameObject, iTween.Hash(
                        "scale", vScale,
                        "time", duration,
                        "easetype", "easeInOutBack",
                        "looptype", "none"
                    ));
                }
            }
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (pressScaling)
            {                
                if (IsPressed() == false)
                {
                    float duration = 0.05f;

                    iTween.ScaleTo(gameObject, iTween.Hash(
                        "scale", orgScale,
                        "time", duration,
                        "easetype", "easeInOutBack",
                        "looptype", "none"
                    ));
                }
            }
        }
    }
}