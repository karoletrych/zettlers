using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace zettlers
{
    public class PlayerRequest
    {
        public int PlayerId { get; set; }
        public Request Request { get; set; }
    }
    class AllPlayersCommand
    {
        public List<PlayerRequest> PlayerCommands { get; set; }
        public int LockstepTurnId { get; set; }
    }

    public class Server : MonoBehaviour, INetEventListener, INetLogger
    {
        public event EventHandler<PlayerRequest> NetworkReceivedEvent;
        private NetManager _netManager;
        private List<NetPeer> _peers = new List<NetPeer>();
        private NetDataWriter _dataWriter;
        private BinaryFormatter _binaryFormatter = new BinaryFormatter();

        void Start()
        {
            NetDebug.Logger = this;
            _dataWriter = new NetDataWriter();
            _netManager = new NetManager(this);
            _netManager.Start(port: 5000);
            _netManager.BroadcastReceiveEnabled = true;
            _netManager.UpdateTime = 15;
        }

        void PollEvents()
        {
            _netManager.PollEvents();
        }

        void SendToAll(AllPlayersCommand allPlayersCommand)
        {
            MemoryStream memoryStream = new MemoryStream();
            _binaryFormatter.Serialize(memoryStream, allPlayersCommand);
            byte[] bytes = memoryStream.ToArray();
            _netManager.SendToAll(bytes, Networking.PacketDeliveryMethod);
        }

        void OnDestroy()
        {
            NetDebug.Logger = null;
            if (_netManager != null)
            {
                _netManager.Stop();
            }
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Debug.Log("[SERVER] We have new peer " + peer.EndPoint);
            _peers.Add(peer);
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketErrorCode)
        {
            Debug.Log("[SERVER] error " + socketErrorCode);
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader,
            UnconnectedMessageType messageType)
        {
            if (messageType == UnconnectedMessageType.Broadcast)
            {
                Debug.Log("[SERVER] Received discovery request. Send discovery response");
                NetDataWriter resp = new NetDataWriter();
                resp.Put(1);
                _netManager.SendUnconnectedMessage(resp, remoteEndPoint);
            }
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            request.AcceptIfKey("sample_app");
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Debug.Log("[SERVER] peer disconnected " + peer.EndPoint + ", info: " + disconnectInfo.Reason);
            _peers.Remove(peer);
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            byte[] bytes = reader.GetRemainingBytes();
            var ms = new MemoryStream(bytes);
            Request request = (Request)_binaryFormatter.Deserialize(ms);
            var playerRequest = new PlayerRequest { PlayerId = peer.Id, Request = request };
            NetworkReceivedEvent?.Invoke(this, playerRequest);
        }

        public void WriteNet(NetLogLevel level, string str, params object[] args)
        {
            Debug.LogFormat(str, args);
        }
    }
}