using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LastSlavda
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static List<string> drinks = new List<string>() { "water", "cola", "juice" };
        public static Dictionary<int, int> items = new Dictionary<int, int>();

        public Entry eCounter { private set; get; }
        public Stepper sCounter { private set; get; }


        public MainPage()
        {
            createComp();
            InitializeComponent();
            initComp();
        }

        private void initComp()
        {
            select.SelectedIndex = 0;
            slCounter.Children.Add(eCounter);
            slCounter.Children.Add(sCounter);
        }

        private void createComp()
        {
            eCounter = new Entry()
            {
                MaxLength = 2,
                Placeholder = "Count",
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = ""
            };

            sCounter = new Stepper()
            {
                Minimum = 0,
                Maximum = 99,
                Increment = 1,
                HorizontalOptions = LayoutOptions.End
            };

            sCounter.ValueChanged += (sender, e) =>
            {
                eCounter.Text = e.NewValue > 0 ? e.NewValue.ToString() : "";
            };

            eCounter.TextChanged += (sender, e) =>
            {
                sCounter.Value = eCounter.Text != "" ? Convert.ToInt32(eCounter.Text) : 0;
            };
        }

        private void Select_SwapWindows(object sender, EventArgs e)
        {
            iDrink.Source = drinks[(sender as Picker).SelectedIndex] + ".jpg";
            eCounter.Text = "";
        }

        private void Button_Confirm(object sender, EventArgs e)
        {
            if (eCounter.Text != "")
            {
                if (!items.TryGetValue(select.SelectedIndex, out _))
                    items.Add(select.SelectedIndex, Convert.ToInt32(eCounter.Text));
                else
                    items[select.SelectedIndex] += Convert.ToInt32(eCounter.Text);
                Navigation.PopToRootAsync();
            }
        }
    }
}
