using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventSystem;

namespace Core
{
    /// <summary>
    /// is placed on Input Handler
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] Toggle mouseInputToggle;
        [SerializeField] Toggle touchInputToggle;

        public static Action<Vector2> OnAction;

        void Awake()
        {
            mouseInputToggle.onValueChanged.AddListener(val => touchInputToggle.isOn = !val);
            touchInputToggle.onValueChanged.AddListener(val => mouseInputToggle.isOn = !val);
        }
        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (mouseInputToggle.isOn)
            {
                if (!current.IsPointerOverGameObject() && Input.GetMouseButton(0))
                {
                    OnAction?.Invoke(Input.mousePosition);
                }
            }
            else if (touchInputToggle.isOn)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        return;
                    if (touch.phase is TouchPhase.Began or TouchPhase.Stationary)
                    {
                        OnAction?.Invoke(touch.position);
                    }
                }
            }
        }
    }
}