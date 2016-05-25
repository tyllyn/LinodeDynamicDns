using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinodeDynamicDns {
	class Program {
		static void Main(string[] args) {
			
			if (!args.Contains("setup")) {
				UpdateDns();
				return;
			}

			#region Setup

			var values = new Dictionary<Config.Keys, String>();

			Console.WriteLine("Sign into your Linode account, and click the 'My Profile' link on the top right. Go to the 'API Keys' tab, and generate a non-expiring key.");
			Console.WriteLine();
			Console.Write("API Key: ");
			var apiKey = Console.ReadLine();
			values.Add(Config.Keys.ApiKey, apiKey);
			Console.WriteLine();

			var linodeApi = new LinodeApi(apiKey);

			Console.WriteLine("Please enter the domain ID that you wish to use:");
			foreach (var kvp in linodeApi.GetDomainIds()) {
				Console.WriteLine(kvp.Key + ": " + kvp.Value);
			}
			Console.WriteLine();
			Console.Write("Domain ID: ");
			var domainId = Console.ReadLine();
			values.Add(Config.Keys.DomainId, domainId);
			Console.WriteLine();

			Console.WriteLine("Please enter the resource ID that you wish to use:");
			foreach (var kvp in linodeApi.GetDomainResources(Convert.ToInt32(domainId))) {
				Console.WriteLine(kvp.Key + ": " + kvp.Value);
			}
			Console.WriteLine();
			Console.Write("Resource ID: ");
			var resourceId = Console.ReadLine();
			values.Add(Config.Keys.ResourceId, resourceId);
			Console.WriteLine();

			Config.SetAll(values);
			Console.WriteLine("All settings have been saved. Run this without arguments to update your DNS entries.");

			#endregion

		}

		public static void UpdateDns() {

			Console.WriteLine("Updating DNS...");
			var api = new LinodeApi(Config.Get(Config.Keys.ApiKey));
			api.SetDynamicDns(
				Config.Get(Config.Keys.ApiKey),
				Config.Get(Config.Keys.DomainId),
				Config.Get(Config.Keys.ResourceId)
			);
			Console.WriteLine("Request sent!");
		}
	}
}
