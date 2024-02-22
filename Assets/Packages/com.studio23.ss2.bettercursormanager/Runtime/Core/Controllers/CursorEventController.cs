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
        private Camera _camera;
        private List<RaycastResult> _raycastResults = new List<RaycastResult>();

        internal void Initialize(LayerMask layerMask, Camera mainCamera)
        {
            _layerMask = layerMask;
            _camera = mainCamera;
        }

        private void Update()
        {
            UpdateCollision();
        }

        private void UpdateCollision()
        {
            IHoverable currentHoveredObject = null;

            if (BetterCursor.Instance.UiOnHoverEnabled)
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
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out hit, int.MaxValue, _layerMask)) return hit.collider.gameObject;

            return null;
        }

        private GameObject GetHoveredUIObject()
        {
            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Mouse.current.position.ReadValue();

            _raycastResults.Clear(); // Clear the list before using it again
            EventSystem.current.RaycastAll(pointerEventData, _raycastResults);

            if (_raycastResults.Count > 0) return _raycastResults[0].gameObject;

            return null;
        }

    }
}