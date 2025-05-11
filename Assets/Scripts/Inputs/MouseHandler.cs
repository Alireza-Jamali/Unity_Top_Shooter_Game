using Core;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventSystem;

namespace Inputs
{
    public class MouseHandler : InputHandler
    {
        void Update()
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
        }
    }
}