using TeArchitecture.Domain;
using TeArchitecture.Shared;

namespace TeArchitecture.Demo1
{
    public readonly struct SellPlayerNowAction
    {
        public readonly PlayerId PlayerId;
        public SellPlayerNowAction(PlayerId playerId) => PlayerId = playerId;
    }

    public class SellPlayerNowHandler : Handler<SellPlayerNowAction>
    {
        // TODO: inject somehow => for now, constructor.
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

        public override ITask Process(SellPlayerNowAction sellAction)
        {
            var player = squad.GetPlayer(sellAction.PlayerId);

            if (player == null)
            {
                return Fail("Player not part Of the Squad.");                
            }

            if (player.Age > 31)
            {
                return Fail("Player too old for this shit.");                
            }

            if (squad.IsOnPitch(sellAction.PlayerId))
            {
                return Fail("Cannot sell player on pitch.");                
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
                            Fail("Server fail to sell players");
                            return;
                        }

                        // Success -> update model and stuff...
                        squad.AllPlayers.RemoveAll(p=>p.Id == sellAction.PlayerId);
                        wallet.Money += r.SellPrice;
                        Finish();
                    }
                ).OnFail( _ => Fail("Server did not respond."));

            // TODO: maybe we can merge this instruction with previous one?
            return FinishAsync();
        }
    }

    #region Proto imitation
   
    public class SellPlayerRequest
    {
        public long PlayerId;
    }

    public class SellPlayerResponse
    {
        public bool IsSuccess { get; set; }
        public int SellPrice { get; set; }
    }

    #endregion
}
