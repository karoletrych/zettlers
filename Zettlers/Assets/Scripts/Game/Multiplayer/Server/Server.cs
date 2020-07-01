using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Google.Protobuf;

namespace zettlers
{


    public class Server : MonoBehaviour, INetEventListener, INetLogger, IServer
    {
        public event EventHandler<RequestReceivedEventArgs> RequestReceivedEvent;
        public event EventHandler<int> PlayerConnected;
        private NetManager _netManager;
        private List<NetPeer> _peers = new List<NetPeer>();
        private NetDataWriter _dataWriter;


        public void Start()
        {
            NetDebug.Logger = this;
            _dataWriter = new NetDataWriter();
            _netManager = new NetManager(this);
            _netManager.Start(port: 5000);
            _netManager.BroadcastReceiveEnabled = true;
            _netManager.UpdateTime = 15;
        }

        public void PollEvents()
        {
            _netManager.PollEvents();
        }

        public void SendToAll(LockstepUpdateResponse allPlayersCommand)
        {
            byte[] bytes = allPlayersCommand.Serialize().ToByteArray();
            _netManager.SendToAll(bytes, NetworkingCommonConstants.PacketDeliveryMethod);
        }

        public void OnDestroy()
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

            Commands.LockstepUpdateRequest rq =
                Commands.LockstepUpdateRequest.Parser.ParseFrom(bytes);

            Debug.Log("Server received" + rq.ToString());

            var playerRequest = new RequestReceivedEventArgs { ClientId = peer.Id, Request = rq.Deserialize() };

            RequestReceivedEvent?.Invoke(this, playerRequest);
        }

        public void WriteNet(NetLogLevel level, string str, params object[] args)
        {
            Debug.LogFormat(str, args);
        }
    }
}