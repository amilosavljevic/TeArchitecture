using TeArchitecture.Domain;
using TeArchitecture.Shared;
using TeArchitecture.Shared.MVC;

namespace TeArchitecture.Demo1
{
    public class SquadView : IDataView<ISquad>
    {
        // TODO: Inject somehow. Constructor, reflection, implicit from base class...
        // Will use constructor in this example.
        private IBus bus;

        private IDataView<string> formationView;
        private IDataView<ISquad> playersOnPitchView;
        private IDataView<ISquad> reservesView;

        public SquadView(IBus bus)
        {
            this.bus = bus;
        }
        
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

        #region Event handlers

        private void OnSellButtonClicked()
        {
            var clickedPlayer = GetClickedPlayerId();
            bus.Send(new SellPlayerNowAction(clickedPlayer), this);                
        }

        private void OnPlayerDroppedOntoPitch()
        {
            var player1 = GetClickedPlayerId();
            var player2 = GetClickedPlayerId();

            bus.Send(new SubstitutePlayersAction(player1, player2), this)
                .OnFail( task =>
                {
                    Toaster.Show("SubstitutePlayerHandler_CannotSwapWithSamePlayer");
                });
        }

        #endregion

        #region Helper methods

        private PlayerId GetClickedPlayerId() => new PlayerId(100);

        #endregion
    }
}
