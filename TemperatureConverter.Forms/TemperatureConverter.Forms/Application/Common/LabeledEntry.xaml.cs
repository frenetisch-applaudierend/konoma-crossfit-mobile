using Xamarin.Forms;

namespace TemperatureConverter.Forms.Application.Common
{
    public partial class LabeledEntry
    {
        public LabeledEntry()
        {
            InitializeComponent();

            TextView.TextChanged += (_, e) => this.Text = e.NewTextValue;
        }

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label),
            typeof(string),
            typeof(LabeledEntry),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: LabelPropertyChanged);

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        private static void LabelPropertyChanged(BindableObject self, object oldValue, object newValue)
        {
            ((LabeledEntry)self).LabelView.Text = (string)newValue;
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(LabeledEntry),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void TextPropertyChanged(BindableObject self, object oldValue, object newValue)
        {
            ((LabeledEntry)self).TextView.Text = (string)newValue;
        }
    }
}
