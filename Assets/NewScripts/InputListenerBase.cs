using UnityEngine;
using UnityEngine.Events;

    public enum ButtonInteractType
    {
        Down,
        Up,
        Hold
    }

    public class InputListenerBase : MonoBehaviour
    {
        [SerializeField]
        private KeyCode keycode;

        [SerializeField]
        private ButtonInteractType buttonType;

        [SerializeField]
        private UnityEvent action = new UnityEvent();

        protected virtual void Update()
        {
            ReactToButtonPress();
        }

        protected virtual void ReactToButtonPress()
        {
            //listen for button
            switch (buttonType)
            {
                case ButtonInteractType.Down:
                    if (Input.GetKeyDown(keycode))
                    {
                        action.Invoke();
                    }
                    break;
                case ButtonInteractType.Up:
                    if (Input.GetKeyDown(keycode))
                    {
                        action.Invoke();
                    }
                    break;
                case ButtonInteractType.Hold:
                    if (Input.GetKeyDown(keycode))
                    {
                        action.Invoke();
                    }
                    break;
            }
        }
    }
