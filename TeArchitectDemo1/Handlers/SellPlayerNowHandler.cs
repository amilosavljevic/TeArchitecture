using TeArchitecture.Domain;
using TeArchitecture.Shared;

namespace TeArchitecture.Demo1
{
    public readonly struct SellPlayerNowAction
    {
        public readonly PlayerId PlayerId;
        public SellPlayerNowAction(PlayerId playerId) => PlayerId = playerId;
    }

    public class SellPlayerNowHandler : IHandler<SellPlayerNowAction>
    {        
        private readonly Squad squad;
        private readonly Wallet wallet;
        private readonly IBus bus;
        private readonly IChannel communicationChannel;

        public SellPlayerNowHandler(IBus bus, Squad squad, Wallet wallet, IChannel channel)
        {
            this.bus = bus;
            this.squad = squad;
            this.wallet = wallet;
            this.communicationChannel = channel;
        }

        public void Process(SellPlayerNowAction sellAction, ITask task)
        {
            var player = squad.GetPlayer(sellAction.PlayerId);

            if (player == null)
            {
                task.Fail("Player not part Of the Squad.");
                return;
            }

            if (player.Age > 31)
            {
                task.Fail("Player too old for this shit.");
                return;
            }

            if (squad.IsOnPitch(sellAction.PlayerId))
            {
                task.Fail("Cannot sell player on pitch.");
                return;
            }

            // this will simulate sending proto to server.
            var req = new SellPlayerRequest
            {
                PlayerId = sellAction.PlayerId
            };

            communicationChannel.Send<SellPlayerRequest, SellPlayerResponse>(req)
                .OnSuccess(
                    r => {
                        if (!r.IsSuccess)
                        {
                            task.Fail("Server fail to sell players");
                            return;
                        }

                        // Success -> update model and stuff...
                        squad.AllPlayers.RemoveAll(p=>p.Id == sellAction.PlayerId);
                        wallet.Money += r.SellPrice;
                        task.Finish();
                    }
                ).OnFail( _ => task.Fail("Server did not respond."));
        }        
    }

    // This is proto. It would be better if servers are in C#, we would not need to re-pack request in proto :O
    public class SellPlayerRequest
    {
        public long PlayerId;
    }

    public class SellPlayerResponse
    {
        public bool IsSuccess { get; set; }
        public int SellPrice { get; set; }
    }
}
