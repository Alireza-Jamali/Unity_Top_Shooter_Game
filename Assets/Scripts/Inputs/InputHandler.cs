using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    /// <summary>
    /// is placed on Input Handler
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] protected Toggle touchInputToggle;
        [SerializeField] protected Toggle mouseInputToggle;

        public static Action<Vector2> OnAction;

        protected virtual void Start()
        {
            touchInputToggle.onValueChanged.AddListener(val => mouseInputToggle.isOn = !val);
            mouseInputToggle.onValueChanged.AddListener(val => touchInputToggle.isOn = !val);
        }
    }
}