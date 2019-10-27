using TeArchitecture.Domain;

namespace TeArchitecture.Demo1
{
    public class SquadModel : Model<SquadModel, Squad>
    {
        protected override void OnInit()
        {
            base.OnInit();

            On<SellPlayerNowAction>(() => new SellPlayerNowHandler(GlobalBus.Instance, Data, WalletModel.Data, Session.Instance));
            On<SubstitutePlayersAction>(() => new SubstitutePlayerHandler(GlobalBus.Instance, Data, Session.Instance));
        }
    }
}
