namespace TeArchitecture.MVVM.Domain.YouthAcademy
{
    public interface ISlot
    {
        long Id { get; }
        IPlayer Player {get;}        
    }

    public class Slot : ISlot
    {
        public long Id { get; set; }
        public Player Player { get; set; }
        
        IPlayer ISlot.Player => Player;
    }
}
