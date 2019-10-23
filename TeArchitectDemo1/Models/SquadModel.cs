using TeArchitecture.Domain;
using TeArchitecture.Shared.Bus;
using TeArchitecture.Shared.MVC;

namespace TeArchitecture.Demo1
{
    public class SquadMode : Model<SquadMode, Squad>
    {
        protected override void OnInit()
        {
            base.OnInit();

            var bus = GlobalBus.Instance;

            bus.On<SellPlayerNowAction>(() => new SellPlayerNowHandler(GlobalBus.Instance, SquadMode.Data, WalletModel.Data, Session.Instance));
            bus.On<SubstitutePlayersAction>(() => new SubstitutePlayerHandler(GlobalBus.Instance, SquadMode.Data, Session.Instance));
        }
    }
}
