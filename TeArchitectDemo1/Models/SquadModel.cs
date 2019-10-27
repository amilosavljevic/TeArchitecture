using TeArchitecture.Domain;

namespace TeArchitecture.Demo1
{
    public class SquadMode : Model<SquadMode, Squad>
    {
        protected override void OnInit()
        {
            base.OnInit();

            On<SellPlayerNowAction>(() => new SellPlayerNowHandler(GlobalBus.Instance, SquadMode.Data, WalletModel.Data, Session.Instance));
            On<SubstitutePlayersAction>(() => new SubstitutePlayerHandler(GlobalBus.Instance, SquadMode.Data, Session.Instance));
        }
    }
}
