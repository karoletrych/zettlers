using System.Collections.Generic;
using LiteNetLib;
using UnityEngine;

namespace zettlers
{
    class NetworkingCommonConstants : MonoBehaviour
    {

        public static readonly DeliveryMethod PacketDeliveryMethod = DeliveryMethod.ReliableUnordered;
        public static bool IsServer;
        public static int PlayerCount;

        public bool Server;

        public static List<Player> Players = new List<Player>
        {
            new Player("player2")
        };

        public static Player ThisPlayer = new Player("player2");


        private void Start() 
        {
            IsServer = Server;
            Debug.Log("Is server " + IsServer);

        }
    }
}
