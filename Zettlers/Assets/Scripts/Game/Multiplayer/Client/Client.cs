using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using System;
using LiteNetLib.Utils;
using Google.Protobuf;

namespace zettlers
{

    public class Client : INetEventListener, IClient
    {
        private NetManager _netClient;

        public event EventHandler<LockstepUpdateResponse> ResponseReceivedEvent;

        public Client()
        {
            _netClient = new NetManager(this);
            _netClient.UnconnectedMessagesEnabled = true;
            _netClient.UpdateTime = 15;
            _netClient.Start();
        }

        private void OnReceiveNoCommand(NoCommand obj)
        {
        }

        public void PollResponses()
        {
            _netClient.PollEvents();
        }

        public void Send(LockstepUpdateRequest request)
        {
            NetPeer peer = _netClient.FirstPeer;
            if (peer != null && peer.ConnectionState == ConnectionState.Connected)
            {
                byte[] bytes = request.Serialize().ToByteArray();
                peer.Send(bytes, NetworkingCommonConstants.PacketDeliveryMethod);
            }
            else
            {
                _netClient.SendBroadcast(new byte[] { 1 }, 5000);
            }
        }

        public void OnDestroy()
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
            Commands.LockstepUpdateResponse response =
                Commands.LockstepUpdateResponse.Parser
                .ParseFrom(bytes);
            Debug.Log("Client received" + response.ToString());

            var deserialized = response.Deserialize();
            this.ResponseReceivedEvent?.Invoke(this, deserialized);
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