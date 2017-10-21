namespace Coders.Models
{
    public class HintView
    {
        private Hint h;

        public HintView(Hint h)
        {
            this.h = h;
        }

        public object[] Id { get; internal set; }
        public string Description { get; internal set; }
        public string Answer { get; internal set; }
        public int? Landmark_Id { get; internal set; }
    }
}