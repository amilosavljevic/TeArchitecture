using System.Collections.Generic;

namespace TeArchitecture.MVVM.Domain.YouthAcademy
{
    public interface IPlayer
    {
        long Id { get; }
        Star CurrentQuality { get; }
        Star ProjectedQuality { get; }
        Star MaxQuality { get; }
        IReadOnlyList<ICoach> Coaches { get; }
    }

    public class Player : IPlayer
    {
        public long Id { get; set; }
        public Star CurrentQuality { get; set; }
        public Star ProjectedQuality { get; set; }
        public Star MaxQuality { get; set; }
        public List<Coach> Coaches { get; set; }

        IReadOnlyList<ICoach> IPlayer.Coaches => Coaches;
    }
}
