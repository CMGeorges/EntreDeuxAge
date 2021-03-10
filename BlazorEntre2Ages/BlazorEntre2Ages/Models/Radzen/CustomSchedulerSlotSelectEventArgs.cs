using Radzen;

namespace BlazorEntre2Ages.Models.Radzen
{
    public class CustomSchedulerSlotSelectEventArgs : SchedulerSlotSelectEventArgs
    {
        public string Subject { get; set; }
        public string Guest { get; set; }
        public bool Status { get; set; }
    }
}