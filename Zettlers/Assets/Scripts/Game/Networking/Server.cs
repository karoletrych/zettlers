using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
namespace zettlers
{

    public class Server : MonoBehaviour, INetEventListener, INetLogger
    {
        private NetManager _netServer;
        private NetPeer _ourPeer;
        private NetDataWriter _dataWriter;


        void Start()
        {
            NetDebug.Logger = this;
            _dataWriter = new NetDataWriter();
            _netServer = new NetManager(this);
            _netServer.Start(port: 5000);
            _netServer.BroadcastReceiveEnabled = true;
            _netServer.UpdateTime = 15;
        }

        void Update()
        {
            _netServer.PollEvents();
        }

        void FixedUpdate()
        {
            if (_ourPeer != null)
            {
                _dataWriter.Reset();
                _dataWriter.Put(2);
                _ourPeer.Send(_dataWriter, Networking.PacketDeliveryMethod);
            }
        }

        void OnDestroy()
        {
            NetDebug.Logger = null;
            if (_netServer != null)
                _netServer.Stop();
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Debug.Log("[SERVER] We have new peer " + peer.EndPoint);
            _ourPeer = peer;
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
                _netServer.SendUnconnectedMessage(resp, remoteEndPoint);
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
            if (peer == _ourPeer)
                _ourPeer = null;
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
        }

        public void WriteNet(NetLogLevel level, string str, params object[] args)
        {
            Debug.LogFormat(str, args);
        }
    }
}