using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using System.Collections.Generic;
using LiteNetLib.Utils;
using Unity.Mathematics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace zettlers
{
    public class Client : MonoBehaviour, INetEventListener
    {
        BinaryFormatter _binaryFormatter = new BinaryFormatter();

        private NetManager _netClient;

        void Start()
        {
            _netClient = new NetManager(this);
            _netClient.UnconnectedMessagesEnabled = true;
            _netClient.UpdateTime = 15;
            _netClient.Start();
        }

        public void Send(Request request)
        {
            _netClient.PollEvents();

            NetPeer peer = _netClient.FirstPeer;
            if (peer != null && peer.ConnectionState == ConnectionState.Connected)
            {
                MemoryStream memoryStream = new MemoryStream();
                _binaryFormatter.Serialize(memoryStream, request);
                byte[] bytes = memoryStream.ToArray();
                peer.Send(bytes, Networking.PacketDeliveryMethod);
            }
            else
            {
                _netClient.SendBroadcast(new byte[] { 1 }, 5000);
            }
        }

        void OnDestroy()
        {
            if (_netClient != null)
                _netClient.Stop();
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Debug.Log("[CLIENT] We connected to " + peer.EndPoint);
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketErrorCode)
        {
            Debug.Log("[CLIENT] We received error " + socketErrorCode);
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            if (messageType == UnconnectedMessageType.BasicMessage && _netClient.ConnectedPeersCount == 0 && reader.GetInt() == 1)
            {
                Debug.Log("[CLIENT] Received discovery response. Connecting to: " + remoteEndPoint);
                _netClient.Connect(remoteEndPoint, "sample_app");
            }
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {

        }

        public void OnConnectionRequest(ConnectionRequest request)
        {

        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Debug.Log("[CLIENT] We disconnected because " + disconnectInfo.Reason);
        }
    }
}