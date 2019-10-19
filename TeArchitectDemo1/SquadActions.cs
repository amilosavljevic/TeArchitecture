using TeArchitecture.Shared;

namespace TeArchitecture.Demo1
{
    public static class SquadActions
    {
        public readonly struct SubstitutePlayers
        {
            public readonly PlayerId PlayerIn;
            public readonly PlayerId PlayerOut;

            public SubstitutePlayers(PlayerId playerIn, PlayerId playerOut)
            {
                PlayerIn = playerIn;
                PlayerOut = playerOut;
            }
        }

        public readonly struct SellPlayer
        {
            public readonly PlayerId Player;

            public SellPlayer(PlayerId player)
            {
                Player = player;
            }
        }
    }    
}
