using System;

namespace zettlers
{
    public interface IClient
    {
        void Send(LockstepUpdateRequest update);

        void PollResponses();
        event EventHandler<LockstepUpdateResponse> ResponseReceivedEvent;
    }
}