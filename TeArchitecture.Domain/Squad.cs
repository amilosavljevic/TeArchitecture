﻿using System.Collections.Generic;
using System.Linq;

namespace TeArchitecture.Domain
{
    public interface ISquad
    {
        // Should not be string.
        string Formation { get; }
        IReadOnlyList<Player> PlayersOnPitch { get; }
        IReadOnlyList<Player> AllPlayers { get; }

        // TODO: Nullable reference types would be great here!
        Player GetPlayer(PlayerId id);
    }

    public class Squad : ISquad
    {
        public List<PlayerId> PlayersOnPitch { get; set; }

        public List<Player> AllPlayers { get; set; }
        
        public string Formation { get; set; }
        
        private readonly List<Player> cachedPlayersOnPitch = new List<Player>();

        IReadOnlyList<Player> ISquad.PlayersOnPitch
        {
            get
            {
                cachedPlayersOnPitch.Clear();
                cachedPlayersOnPitch.AddRange(PlayersOnPitch.Select(GetPlayer));
                return cachedPlayersOnPitch;
            }
        }

        IReadOnlyList<Player> ISquad.AllPlayers => AllPlayers;

        public Player GetPlayer(PlayerId id) => AllPlayers.Find(p => p.Id == id);

        public bool IsOnPitch(PlayerId playerId) => PlayersOnPitch.Contains(playerId);
    }
}
