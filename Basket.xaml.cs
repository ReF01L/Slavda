using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LastSlavda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basket : ContentPage
    {
        public Basket()
        {
            InitializeComponent();
            init(MainPage.items, MainPage.drinks);
        }

        private void generator(Dictionary<int, int> items, List<string> drinks)
        {

            foreach (KeyValuePair<int, int> item in items)
            {
                StackLayout mainLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(15, 25, 15, 0)
                };

                StackLayout confirmStack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(15)
                };

                Image img = new Image()
                {
                    Source = drinks[item.Key] + ".jpg",
                    WidthRequest = 36,
                    HeightRequest = 36,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.Center
                };

                Label lName = new Label()
                {
                    Text = drinks[item.Key].ToUpper(),
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                };

                Label lCount = new Label()
                {
                    Text = item.Value.ToString(),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                Button bDelete = new Button()
                {
                    Text = "X",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                };

                bDelete.Clicked += (sender, args) =>
                {
                    items.Remove(item.Key);
                    MainField.Children.Remove(mainLayout);
                    dataList.Children.Remove(confirmStack);
                };

                Image confirmImg = new Image()
                {
                    Source = drinks[item.Key] + ".jpg",
                    WidthRequest = 25,
                    HeightRequest = 25,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                };

                Label confirmLName = new Label()
                {
                    Text = drinks[item.Key].ToUpper(),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.Center
                };

                Label confirmLCount = new Label()
                {
                    Text = item.Value.ToString(),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                mainLayout.Children.Add(img);
                mainLayout.Children.Add(lName);
                mainLayout.Children.Add(lCount);
                mainLayout.Children.Add(bDelete);

                MainField.Children.Add(mainLayout);

                confirmStack.Children.Add(confirmImg);
                confirmStack.Children.Add(confirmLName);
                confirmStack.Children.Add(confirmLCount);

                dataList.Children.Add(confirmStack);
            }
        }
        private void init(Dictionary<int, int> items, List<string> drinks)
        {
            generator(items, drinks); // Шрифты
            Order.FontFamily = "Assets/LiuJianMaoCao-Regular.ttf#LiuJianMaoCao-Regular";
        }

        private void OpenShop(object sender, EventArgs e)
        {
            MainPage page = new MainPage();
            page.Disappearing += (_s, _e) =>
            {
                MainField.Children.Clear();
                dataList.Children.Clear();
                generator(MainPage.items, MainPage.drinks);
            };
            Navigation.PushAsync(page);
        }

        private void toggleActive(Button btn1, Button btn2, bool isActive)
        {
            btn1.IsEnabled = isActive;
            btn2.IsEnabled = isActive;
        }
        private void toggleWindows(Layout grid1, Layout grid2, bool isVisible)
        {
            grid1.IsVisible = isVisible;
            grid2.IsVisible = !isVisible;
        }

        private void OrderList(object sender, EventArgs e)
        {
            toggleWindows(MainField, ConfirmWindow, false);
            toggleActive(sender as Button, Shop, false);
        }

        private void B_Agree(object sender, EventArgs e)
        {
            dataListFinal.Children.Add(dataList);
            toggleWindows(ConfirmWindow, FinalWindow, false);
        }

        private void B_Disagree(object sender, EventArgs e)
        {
            toggleWindows(MainField, ConfirmWindow, true);
            toggleActive(Shop, Order, true);
        }
        private void B_Finish(object sender, EventArgs e)
        {
            EmployList employList = new EmployList();
            foreach (KeyValuePair<int, int> item in MainPage.items)
            {
                employList.employs.Add(new Employ(MainPage.drinks[item.Key], item.Value));
            }

            string json = JsonConvert.SerializeObject(employList);
            DisplayAlert(json, "Ok", "Cancel");

            FinalWindow.IsVisible = false;
            toggleActive(Shop, Order, true);
            Navigation.PopToRootAsync();
            MainPage.items.Clear();
        }
    }
}