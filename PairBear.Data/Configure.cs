using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairBear.Data
{
	public static class Configure
	{
		public static void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IPairRepository, PairRepository>();
		}
	}
}
