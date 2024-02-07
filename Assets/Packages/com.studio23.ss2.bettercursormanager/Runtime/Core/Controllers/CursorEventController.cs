using System.Collections.Generic;
using Studio23.SS2.BetterCursor.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Studio23.SS2.BetterCursor.Core
{
    public class CursorEventController : MonoBehaviour
    {
        private IHoverable _currentHoveringObject;
        private IHoverable _lastHoveredObject;

        private LayerMask _layerMask;
        private Camera _camera;

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
            if (BetterCursor.Instance.UiOnHoverEnabled)
                _currentHoveringObject = GetHoveredUIObject()?.GetComponent<IHoverable>();
            else
                _currentHoveringObject = GetHoveredObject()?.GetComponent<IHoverable>();


            if (_currentHoveringObject == _lastHoveredObject) return;

            if (_lastHoveredObject != _currentHoveringObject)
            {
                _lastHoveredObject?.OnHoverExit();
                _lastHoveredObject = _currentHoveringObject;
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

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count > 0) return raycastResults[0].gameObject;

            return null;
        }
    }
}