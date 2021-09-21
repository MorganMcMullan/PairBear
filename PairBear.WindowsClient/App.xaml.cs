using Microsoft.Extensions.DependencyInjection;
using PairBear.WindowsClient.ViewModels;
using System.Windows;

namespace PairBear.WindowsClient
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		public static ServiceProvider ServiceProvider { get; set; }

		//Initialize the service provider for dependency injection
		public App()
		{
			ServiceCollection services = new ServiceCollection();
			ConfigureServices(services);
			ServiceProvider = services.BuildServiceProvider();
		}

		//Configure the services for dependency injection
		private void ConfigureServices(ServiceCollection services)
		{
			Service.Configure.ConfigureServices(services);
			Data.Configure.ConfigureServices(services);
			services.AddScoped<NewPairFormViewModel>();
			services.AddScoped<PairListViewModel>();
		}
	}
}
