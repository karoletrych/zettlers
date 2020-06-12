using LiteNetLib;
using UnityEngine;

namespace zettlers
{
    public class Networking : MonoBehaviour
    {

        public static readonly DeliveryMethod PacketDeliveryMethod = DeliveryMethod.ReliableUnordered;
        public static bool IsServer;
        public static int PlayerCount;

        public bool Server;
        public int Players;

        private void Start() 
        {
            IsServer = Server;
            Debug.Log("Is server " + IsServer);

            
            PlayerCount = Players;
            Debug.Log("Players " + PlayerCount);
        }
    }
}
