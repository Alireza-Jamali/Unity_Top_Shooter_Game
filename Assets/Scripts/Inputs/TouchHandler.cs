using System.Collections.Generic;
using Core;

namespace Inputs
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class TouchHandler : InputHandler
    {
        private GraphicRaycaster _graphicRaycaster;
        private EventSystem _eventSystem;
    
        private PointerEventData _pointerData;

        protected override void Start()
        {
            base.Start();
            Initialize();
        }
        
        void Initialize()
        {
            _graphicRaycaster = FindObjectOfType<GraphicRaycaster>();
            _eventSystem = EventSystem.current;
            _pointerData = new PointerEventData(_eventSystem);
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                _pointerData.position = touch.position;

                if (IsOverUI(touch.fingerId))
                {
                    // Touch is over UI - abort
                    return;
                }

                if (touch.phase is TouchPhase.Began or TouchPhase.Stationary or TouchPhase.Moved)
                {
                    // Handle valid touch
                    OnAction?.Invoke(touch.position);
                }
            }
        }

        private bool IsOverUI(int fingerId)
        {
            // First check EventSystem
            if (_eventSystem.IsPointerOverGameObject(fingerId))
                return true;

            // Then check GraphicRaycaster
            List<RaycastResult> results = new List<RaycastResult>();
            _graphicRaycaster.Raycast(_pointerData, results);
            return results.Count > 0;
        }
    }
}