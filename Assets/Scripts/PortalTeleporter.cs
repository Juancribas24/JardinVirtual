using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public TeleportationAnchor receiverAnchor;

    private bool playerIsOverlapping = false;

    void Update()
    {
        if (playerIsOverlapping)
        {
            // Simula una teletransportación hacia el anchor del receptor
            TeleportToAnchor();
            playerIsOverlapping = false;
        }
    }

    void TeleportToAnchor()
    {
        // Desactiva la entrada de usuario para el láser y realiza la teletransportación manual
        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = receiverAnchor.teleportAnchorTransform.position,
            destinationRotation = receiverAnchor.teleportAnchorTransform.rotation
        };

        var teleporter = FindObjectOfType<TeleportationProvider>();
        if (teleporter != null)
        {
            teleporter.QueueTeleportRequest(request);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = false;
        }
    }
}
