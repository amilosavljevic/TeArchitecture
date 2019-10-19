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
        // TODO: inject
        private Squad squad;
        private Wallet wallet;
        private IBus bus;

        public IResult Process(SellPlayerNowAction sellAction, IResultBuilder result)
        {
            var player = squad.GetPlayer(sellAction.PlayerId);

            if (player == null)
            {
                return result.Fail("Player not part Of the Squad.");
            }

            if (player.Age > 31)
            {
                return result.Fail("Player too old to sell.");
            }

            var req = new SellPlayerRequest
            {
                PlayerId = sellAction.PlayerId.Id
            };

            bus.Send<SellPlayerRequest, SellPlayerResponse>(req)
                .OnSuccess(r =>
                {
                    if (!r.Value.IsSuccess)
                    {
                        result.Fail("Server fail to sell players");
                        return;
                    }

                    // Success -> update model and stuff...
                    squad.RemovePlayer(sellAction.PlayerId);
                    wallet.Money += r.Value.SellPrice;
                    result.Finish();
                }
                )
                .OnFail(r => result.Fail("Server did not respond."));

            return result.Wait();
        }
    }


    // This is proto. It would be better if servers are in C#, we would not need to re-pack request in proto.
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
