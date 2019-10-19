using TeArchitecture.Domain;
using TeArchitecture.Shared;

namespace TeArchitecture.Demo1
{
    public readonly struct SubstitutePlayersAction
    {
        public readonly PlayerId PlayerIn;
        public readonly PlayerId PlayerOut;

        public SubstitutePlayersAction(PlayerId playerIn, PlayerId playerOut)
        {
            PlayerIn = playerIn;
            PlayerOut = playerOut;
        }
    }

    public class SubstitutePlayerHandler //: IHandler<SubstitutePlayersAction>
    {
        // TODO: inject
        private Squad squad;
    }
}
