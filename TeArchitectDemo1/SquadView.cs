using TeArchitecture.Domain;
using TeArchitecture.Shared;

namespace TeArchitecture.Demo1
{
    public class SquadView : IDataView<ISquad>
    {
        // TODO: Inject somehow. Constructor, reflection, implicit from base class...
        // Will use contructor in this example.
        private IBus bus;

        IDataView<string> formationView;
        IDataView<ISquad> playersOnPitchView;
        IDataView<ISquad> reservesView;

        public SquadView(IBus bus)
        {
            this.bus = bus;
        }

        private IDataView<Moral> moralView;
        private IDataView<Condition> conditionView;

        public void SetData(ISquad squad)
        {
            // TODO:
            // Show data
            // Split sub-data to sub-views
            // This could be done automatically

            formationView.SetData(squad.Formation);
            playersOnPitchView.SetData(squad);
            reservesView.SetData(squad);
        }

        private void OnSellButtonClicked()
        {
            var clickedPlayer = GetClickedPlayerId();

            bus.Send(new SellPlayerNowAction(clickedPlayer), this)
                .OnSuccess(res => { })
                .OnFail(res => { });
        }

        private PlayerId GetClickedPlayerId() => new PlayerId(100);       
    }
}
