namespace TeArchitecture.MVVM.Domain.YouthAcademy
{
    public interface ICoach
    {
        long Id { get; }
        bool IsActive { get; }
        Star Gain { get; }
        int DurationInDays { get; }
        int DaysLeft { get; }
    }

    public class Coach : ICoach
    {
        public long Id { get; set; }

        public bool IsActive { get; set; }

        public Star Gain { get; set; }

        public int DurationInDays { get; set; }

        public int DaysLeft { get; set; }
    }


}
