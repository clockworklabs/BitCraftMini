using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SpacetimeDB;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BitCraftMiniGameManager : Singleton<BitCraftMiniGameManager>
{
    [SerializeField] private NetworkPlayer playerPrefab;
    [SerializeField] private GameObject preSpawnCamera;
    
    [SerializeField] private float spawnAreaRadius;

    private bool playerCreated;
    
    readonly Dictionary<uint, NetworkPlayer> players = new Dictionary<uint, NetworkPlayer>();

    protected void Start()
    {
        Application.targetFrameRate = 60;
        
        StdbNetworkManager.instance.onConnect += () =>
        {
            Debug.Log("Connected.");
        };

        StdbNetworkManager.instance.onDisconnect += () => { };

        StdbNetworkManager.instance.tableUpdate += (index, op, value, newValue) =>
        {
            switch (op)
            {
                case StdbNetworkManager.TableOp.Insert:
                    switch (index)
                    {
                        case 1:
                            if (newValue.HasValue)
                            {
                                var player = Player.From(newValue.Value);
                        
                                // check to see if this player already exists
                                if (!players.TryGetValue(player.entityId, out _))
                                {
                                    // Create a new player
                                    var newNetworkPlayer = Instantiate(playerPrefab);

                                    // Do we own this player?
                                    if (NetworkPlayer.identity.HasValue && player.ownerId.Equals(NetworkPlayer.identity.Value))
                                    {
                                        if (NetworkPlayer.localPlayerId.HasValue)
                                        {
                                            Debug.LogWarning("This identity has more than one player!");
                                            return;
                                        }

                                        Debug.Log($"Attaching to player with id: {player.entityId}");
                                        NetworkPlayer.localPlayerId = player.entityId;
                                    }

                                    newNetworkPlayer.Spawn(player.entityId);
                                    players[player.entityId] = newNetworkPlayer;
                                }
                            }
                            break;
                        case 2:
                            if (newValue.HasValue)
                            {
                                var entityTransform = EntityTransform.From(newValue.Value);
                                // check to see if this player already exists
                                if (players.TryGetValue(entityTransform.entityId, out var networkPlayer))
                                {
                                    // Is this our player?
                                    if (networkPlayer.IsLocal())
                                    {
                                        // Ignore local updates
                                    }
                                    else
                                    {
                                        networkPlayer.transform.position = entityTransform.pos.ToVector3();
                                        networkPlayer.transform.rotation = entityTransform.rot.ToQuaternion();
                                    }
                                }
                            }
                            break;
                        case 3:
                            if (newValue.HasValue)
                            {
                                var playerAnimation = PlayerAnimation.From(newValue.Value);
                                // check to see if this player already exists
                                if (players.TryGetValue(playerAnimation.entityId, out var networkPlayer))
                                {
                                    // Is this our player?
                                    if (networkPlayer.IsLocal())
                                    {
                                        // Ignore local updates
                                    }
                                    else
                                    {
                                        networkPlayer.GetComponent<PlayerMovementController>().SetMoving(playerAnimation.moving);
                                    }
                                }
                            }
                            break;
                        case 4:
                            if (newValue.HasValue)
                            {
                                var entityInventory = EntityInventory.From(newValue.Value);
                                // check to see if this player already exists
                                if (players.TryGetValue(entityInventory.entityId, out var networkPlayer))
                                {
                                    // Is this our player?
                                    if (networkPlayer.IsLocal())
                                    {
                                        networkPlayer.GetComponent<PlayerInventoryController>().InventoryUpdate(entityInventory);
                                    }
                                }
                            }
                            break;
                        case 5:
                            if (newValue.HasValue)
                            {
                                var loginState = PlayerLogin.From(newValue.Value);
                                // check to see if this player already exists
                                if (players.TryGetValue(loginState.entityId, out var networkPlayer))
                                {
                                    networkPlayer.LoginStateChanged();
                                }
                            }
                            break;
                        case 7:
                            if (newValue.HasValue)
                            {
                                var chatMessage = PlayerChatMessage.From(newValue.Value);
                                UIChatController.instance.OnChatMessageReceived(chatMessage.playerId, chatMessage.message);
                            }

                            break;
                    }
                    break;
            }
        };

        StdbNetworkManager.instance.subscriptionUpdate += () =>
        {
            // If we don't have any data for our player, then we are creating a new one.
            var player = Player.FilterByOwnerId(NetworkPlayer.identity.Value);
            if (!NetworkPlayer.localPlayerId.HasValue || player == null)
            {
                playerCreated = true;
                Debug.Log("Sending request for new player.");
                var newPlayerId = (uint)(Random.value * uint.MaxValue);
                var spawnPosition = Random.insideUnitSphere * spawnAreaRadius;
                spawnPosition.y = 0.0f;
                Reducer.CreateNewPlayer(newPlayerId, spawnPosition.ToStdb(), Quaternion.identity.ToStdb());
            }
        };

        StdbNetworkManager.instance.Connect();
    }

    public void LocalPlayerCreated()
    {
        preSpawnCamera.SetActive(false);
    }
}