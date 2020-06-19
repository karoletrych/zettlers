using System;

namespace zettlers
{
    public interface IClient
    {
        void Send(LockstepUpdate update);

        void PollResponses();
        event EventHandler<Response> ResponseReceivedEvent;
    }
}