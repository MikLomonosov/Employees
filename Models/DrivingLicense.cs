

namespace Models
{
    public class DrivingLicense
    {
        private string text;
        public string Text
        {
            get => text;
            set => text = value;
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }
    }
}
