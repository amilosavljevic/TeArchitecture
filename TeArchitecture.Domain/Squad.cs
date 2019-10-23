using System.Collections.Generic;
using System.Linq;

namespace TeArchitecture.Domain
{
    public interface ISquad
    {
        // Should not be string.
        string Formation { get; }
        IReadOnlyList<PlayerData> PlayersOnPitch { get; }
        IReadOnlyList<PlayerData> AllPlayers { get; }

        // TODO: Nullable reference types would be great here!
        PlayerData GetPlayer(PlayerId id);
    }

    public class Squad : ISquad
    {
        public List<PlayerId> PlayersOnPitch { get; set; }

        public List<PlayerData> AllPlayers { get; set; }
        
        public string Formation { get; set; }
        
        private readonly List<PlayerData> cachedPlayersOnPitch = new List<PlayerData>();

        IReadOnlyList<PlayerData> ISquad.PlayersOnPitch
        {
            get
            {
                cachedPlayersOnPitch.Clear();
                cachedPlayersOnPitch.AddRange(PlayersOnPitch.Select(GetPlayer));
                return cachedPlayersOnPitch;
            }
        }

        IReadOnlyList<PlayerData> ISquad.AllPlayers => AllPlayers;

        public PlayerData GetPlayer(PlayerId id) => AllPlayers.Find(p => p.Id == id);

        public bool IsOnPitch(PlayerId playerId) => PlayersOnPitch.Contains(playerId);
    }
}
