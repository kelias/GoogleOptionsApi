using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GoogleOptionsApi
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<OptionPair> OptionPairs { get; set; }
        public ObservableCollection<Expiration> Expirations { get; set; }
        public Expiration SelectedExpiration { get; set; }
        public string Symbol { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            OptionPairs = new ObservableCollection<OptionPair>();
            Expirations = new ObservableCollection<Expiration>();
        }

        public async void GetInitialData()
        {
            if (Symbol == null) return;

            var od = await GoogleOptions.GetOptionChains(Symbol.ToUpper());

            foreach (var e in od.expirations)
            {
                Expirations.Add(e);
            }

            ShowOptionPairs(od);
        }

        public async void FetchData()
        {
            if (SelectedExpiration == null) return;
            if (Symbol == null) return;

            var od = await GoogleOptions.GetOptionChains(Symbol.ToUpper(), SelectedExpiration.y.To<int>(),
                SelectedExpiration.m.To<int>(), SelectedExpiration.d.To<int>());
            ShowOptionPairs(od);
        }

        public void ShowOptionPairs(OptionData od)
        {
            OptionPairs.Clear();

            var map = od.calls.Select(o => new OptionPair
            {
                Strike = o.strike.To<decimal>(),
                Expiry = o.expiry.To<DateTime>(),
                Call = o
            }).ToDictionary(p => p.Strike);

            foreach (var p in od.puts)
            {
                var strike = p.strike.To<decimal>();

                if (map.ContainsKey(strike))
                {
                    map[strike].Put = p;
                }
                else
                {
                    var x = new OptionPair
                    {
                        Strike = p.strike.To<decimal>(),
                        Expiry = p.expiry.To<DateTime>(),
                        Put = p
                    };
                    map.Add(x.Strike, x);
                }
            }

            var optionPairs = map.Select(pair => pair.Value).ToList();

            foreach (var pair in optionPairs)
            {
                OptionPairs.Add(pair);
            }
        }

        private void Expiry_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FetchData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Symbol == null) return;

            GetInitialData();
        }
    }
}