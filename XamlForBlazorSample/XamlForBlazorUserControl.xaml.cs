using Microsoft.AspNetCore.Components;

using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

using static System.Net.WebRequestMethods;
using static XamlForBlazorSample.Pages.Weather;

namespace XamlForBlazorSample
{
	public partial class XamlForBlazorUserControl : UserControl
	{
		HttpClient Http = new HttpClient();
		XamlForBlazorUserControlViewModel viewModel => this.DataContext as XamlForBlazorUserControlViewModel;

		public XamlForBlazorUserControl()
		{
			this.InitializeComponent();

			Http.BaseAddress = new Uri("https://localhost:7011");
		}

		private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			viewModel.Forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
		}
	}
	public class XamlForBlazorUserControlViewModel : INotifyPropertyChanged
	{
		public WeatherForecast[]? forecasts;
		public WeatherForecast[]? Forecasts
		{
			get => forecasts;
			set
			{
				if (forecasts == value) return;

				forecasts = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string callerName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerName));
		}
	}
}
