using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using System.IO;
using System;
using LiteNetLib.Utils;
using System.Collections.Generic;

namespace zettlers.Test
{
    public class Server : MonoBehaviour
    {
        private zettlers.Server _server;

        void Start()
        {
            _server = new zettlers.Server();
            _server.Start();

            _server.RequestReceivedEvent += SendResponse;
        }

        private void SendResponse(object sender, RequestReceivedEventArgs e)
        {
            ((zettlers.Server)sender).SendToAll(
                new LockstepUpdateResponse{
                    LockstepTurnId = e.Request.LockstepTurnId,
                    PlayerCommands = new Dictionary<Player, PlayerCommand>{
                        [new Player(e.ClientId.ToString())] = e.Request.PlayerCommand
                    }
                }
            );
        }

        private void Update() {
            _server.PollEvents();
            
        }

        void OnDestroy()
        {
            _server.OnDestroy();
        }
    }
}