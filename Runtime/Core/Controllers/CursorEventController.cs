using System.Collections.Generic;
using Studio23.SS2.BetterCursor.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Studio23.SS2.BetterCursor.Core
{
    public class CursorEventController : MonoBehaviour
    {
        private IHoverable _lastHoveredObject;
        private LayerMask _layerMask;
        private float _sphereCastRadius;
        private Camera _camera;
        private readonly List<RaycastResult> _raycastResults = new();
        private CursorLocoMotionController _cursorLocoMotionController;
        private BetterCursor _betterCursor;

        internal void Initialize(LayerMask layerMask, float castRadius, BetterCursor betterCursor)
        {
            _layerMask = layerMask;
            _sphereCastRadius = castRadius;
            _betterCursor = betterCursor;
        }

        private void Start()
        {
            _cursorLocoMotionController = GetComponent<CursorLocoMotionController>();
        }

        private void Update()
        {
            if(_betterCursor!=null)
                UpdateCollision();
        }

        private void UpdateCollision()
        {
            IHoverable currentHoveredObject = null;

            if (_betterCursor.UiOnHoverEnabled)
                currentHoveredObject = GetHoveredUIObject()?.GetComponent<IHoverable>();
            else
                currentHoveredObject = GetHoveredObject()?.GetComponent<IHoverable>();

            if (currentHoveredObject == _lastHoveredObject) return;

            if (_lastHoveredObject != currentHoveredObject)
            {
                _lastHoveredObject?.OnHoverExit();
                _lastHoveredObject = currentHoveredObject;
                _lastHoveredObject?.OnHoverEnter();
            }
        }

        private GameObject GetHoveredObject()
        {
            RaycastHit hit;
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            if (_camera == null)
            {
                Debug.LogError($"Main Camera Not Found");
            }

            var ray = _camera.ScreenPointToRay(_betterCursor.GetCursorImagePosition());
            if(Physics.SphereCast(ray, _sphereCastRadius, out hit, int.MaxValue, _layerMask))
                return hit.collider.gameObject;
            return null;
        }

        private GameObject GetHoveredUIObject()
        {
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = _cursorLocoMotionController.GetCursorPosition()
            };

            _raycastResults.Clear(); // Clear the list before using it again
            EventSystem.current.RaycastAll(pointerEventData, _raycastResults);
            if (_raycastResults.Count > 0)
                return _raycastResults[0].gameObject;
            return null;
        }
    }
}