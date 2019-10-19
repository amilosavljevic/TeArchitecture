using System;
using System.Collections.Generic;

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
        public List<PlayerData> PlayersOnPitch { get; set; }
        public List<PlayerData> AllPlayers { get; set; }
        
        public string Formation { get; set; }

        IReadOnlyList<PlayerData> ISquad.PlayersOnPitch => PlayersOnPitch;

        IReadOnlyList<PlayerData> ISquad.AllPlayers => AllPlayers;

        public PlayerData GetPlayer(PlayerId id) => throw new System.NotImplementedException();

        public void RemovePlayer(PlayerId playerId) => throw new NotImplementedException();
    }
}
