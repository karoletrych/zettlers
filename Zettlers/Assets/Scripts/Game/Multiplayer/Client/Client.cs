using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace zettlers
{

    public class Client : MonoBehaviour, INetEventListener, IClient
    {
        BinaryFormatter _binaryFormatter = new BinaryFormatter();

        private NetManager _netClient;

        public event EventHandler<Response> ResponseReceivedEvent;

        void Start()
        {
            _netClient = new NetManager(this);
            _netClient.UnconnectedMessagesEnabled = true;
            _netClient.UpdateTime = 15;
            _netClient.Start();
        }

        public void PollResponses()
        {
            _netClient.PollEvents();
        }

        public void Send(LockstepUpdate request)
        {
            NetPeer peer = _netClient.FirstPeer;
            if (peer != null && peer.ConnectionState == ConnectionState.Connected)
            {
                MemoryStream memoryStream = new MemoryStream();
                _binaryFormatter.Serialize(memoryStream, request);
                byte[] bytes = memoryStream.ToArray();
                peer.Send(bytes, NetworkingCommonConstants.PacketDeliveryMethod);
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
            byte[] bytes = reader.GetRemainingBytes();
            var ms = new MemoryStream(bytes);
            Response response = (Response)_binaryFormatter.Deserialize(ms);
            ResponseReceivedEvent?.Invoke(this, response);
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