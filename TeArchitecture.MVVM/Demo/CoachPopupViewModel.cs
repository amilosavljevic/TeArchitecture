using System.Collections.Generic;

namespace TeArchitecture.MVVM.Demo
{
    public class CoachPopupViewModel
    {
        public string PlayerName { get; set; }
        public string CoachTitle { get; set; }
        public List<CoachRendererViewModel> CoachesData { get; set;}
        public CoachRendererViewModel SelectedCoach { get; set; }
    }

    public class CoachRendererViewModel
    {
        public int Id { get; set; }
        public bool IsExpanded { get; set; }
        public string LabelText { get; set; }
        public string ButtonText { get; set; }

        public void HireCoach()
        {
        }
    }
}
