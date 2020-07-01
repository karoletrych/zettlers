using System;
using System.Collections.Generic;

namespace zettlers
{
    public interface IServer
    {
        void PollEvents();
        event EventHandler<RequestReceivedEventArgs> RequestReceivedEvent;
        event EventHandler<int> PlayerConnected;
    }

    public class RequestReceivedEventArgs
    {
        public LockstepUpdateRequest Request { get; set; }
        public int ClientId { get; set; }
    }
}