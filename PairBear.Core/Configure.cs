using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairBear.Service
{
	public static class Configure
	{
		public static void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IPairService, PairService>();
		}
	}
}
