using TeArchitecture.Domain;
using TeArchitecture.Shared;

namespace TeArchitecture.Demo1
{
    public readonly struct SubstitutePlayersAction
    {
        public readonly PlayerId Player1;
        public readonly PlayerId Player2;

        public SubstitutePlayersAction(PlayerId player1, PlayerId player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
    }

    public class SubstitutePlayerHandler : Handler<SubstitutePlayersAction>
    {
        // Let's do a example where you define in advance what errors could happen.
        public static readonly IError PlayerNotPartOfSquad = new Error("Player not part of this squad!");
        public static readonly IError PlayersNotOnPitch = new Error("At least one player needs to be on pitch.");
        public static readonly IError CannotSwapWithSamePlayer = new Error("Tried to swap player with same player");
        public static readonly IError FailToSubstituePlayersOnServer = new Error("Failed to substitute players.");
        public static readonly IError FailToConnectToServer = new Error("Failed to connect to server.");

        // TODO: inject somehow => for now, constructor.
        private IBus bus;
        private Squad squad;
        private IChannel communicationChannel;

        public SubstitutePlayerHandler(IBus bus, Squad squad, IChannel communicationChannel)
        {
            this.squad = squad;
            this.bus = bus;
            this.communicationChannel = communicationChannel;
        }

        public override ITask Process(SubstitutePlayersAction action)
        {
            if (squad.GetPlayer(action.Player1) == null || squad.GetPlayer(action.Player2) == null)
            {
                return Fail(PlayerNotPartOfSquad);
            }

            if (action.Player1 == action.Player2)
            {
                return Fail(CannotSwapWithSamePlayer);                
            }

            var player1IsOnPitch = squad.IsOnPitch(action.Player1);
            var player2IsOnPitch = squad.IsOnPitch(action.Player2);

            if (!player1IsOnPitch && !player2IsOnPitch)
            {
                return Fail(PlayersNotOnPitch);                
            }

            var request = new SubstitutePlayerRequest()
            {
                Player1 = action.Player1,
                Player2 = action.Player2,
            };

            communicationChannel.Send<SubstitutePlayerRequest, SubstitutePlayerResponse> (request, this)
               .OnSuccess ( (SubstitutePlayerResponse res) =>
                {
                    if (!res.IsSuccess)
                    {
                        Fail(FailToSubstituePlayersOnServer);
                        return;
                    }                    

                    if (player1IsOnPitch && player2IsOnPitch)
                    {
                        var player1Index = squad.PlayersOnPitch.IndexOf(action.Player1);
                        var player2Index = squad.PlayersOnPitch.IndexOf(action.Player2);

                        // Do the switcheroo
                        squad.PlayersOnPitch.Swap(player1Index, player2Index);
                    }
                    else if (player1IsOnPitch)
                    {
                        var indexOnPitch = squad.PlayersOnPitch.IndexOf(action.Player1);
                        squad.PlayersOnPitch[indexOnPitch] = action.Player2;
                    }
                    else // player2 is on pitch
                    {
                        var indexOnPitch = squad.PlayersOnPitch.IndexOf(action.Player2);
                        squad.PlayersOnPitch[indexOnPitch] = action.Player1;
                    }

                    bus.Send(new SquadUpdatedEvent(squad));
                    Finish();
                })
               .OnFail(innerTask => Fail(FailToConnectToServer));

            return FinishAsync();
        }
    }

    public readonly struct SquadUpdatedEvent
    {
        public readonly ISquad Squad;

        public SquadUpdatedEvent(ISquad Squad) => this.Squad = Squad;
    }


    // This will simulate protos. Ideally we wouldn't have them.
    public class SubstitutePlayerRequest
    {
        public PlayerId Player1;
        public PlayerId Player2;
    }

    public class SubstitutePlayerResponse
    {
        public bool IsSuccess;
    }
}
