using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReceiver : MonoBehaviour
{
    private bool cameraMouseButtonDown;
    private Vector2 mouseDelta;
    private float zoomDelta;

    void OnToggleInventory(InputValue value)
    {
        UIPlayerInventoryWindow.instance.Toggle();
    }

    void OnToggleChat(InputValue value)
    {
        UIChatController.instance.Toggle();
	}

	void OnActionButton(InputValue value)
	{
        if (Reticle.SelectedTarget != null)
        {
            if (!PlayerAnimator.Local.Interacting) {

                var resource = Reticle.SelectedTarget.GetComponent<GameResource>();
                if (resource != null)
                {
                    PlayerAnimator.Local.Interact(resource);
                    SpacetimeDB.Reducer.Extract(NetworkPlayer.localPlayerId.Value, resource.EntityId);
                    SpacetimeDB.Reducer.UpdateAnimation(NetworkPlayer.localPlayerId.Value, false, resource.EntityId);
                    return;
                }
                var player = Reticle.SelectedTarget.GetComponent<NetworkPlayer>();
				if (player != null)
				{
                    SpacetimeDB.Reducer.InitiateTradeSession(NetworkPlayer.localPlayerId.Value, player.EntityId);
				}
			}
		}
    }

	
    void OnMove(InputValue value)
    {
        if (PlayerMovementController.Local == null)
        {
            return;
        }
        PlayerMovementController.Local.SetMove(value.Get<Vector2>());
    }

    void OnPointerPosition(InputValue value)
    {
        mouseDelta = value.Get<Vector2>();
    }

    void OnZoom(InputValue value)
    {
        zoomDelta = value.Get<float>();
    }
    
    private void Update()
    {
        if (!CameraController.HasInstance())
        {
            return;
        }
        CameraController.instance.SetMouseDelta(mouseDelta);
        CameraController.instance.SetZoomDelta(zoomDelta);

        zoomDelta = 0.0f;
        mouseDelta = Vector2.zero;
    }
}
