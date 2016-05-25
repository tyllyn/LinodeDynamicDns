using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LinodeDynamicDns {
	public class LinodeApi {

		public String ApiKey { get; private set; }

		public LinodeApi(String apiKey) {
			ApiKey = apiKey;
		}

		private JObject GetDomainList() {
			return GetData("https://api.linode.com/?api_key=" + ApiKey  + "&api_action=domain.list");
		}
		public Dictionary<Int32, String> GetDomainIds() {
			var json = GetDomainList();
			var toReturn = new Dictionary<Int32, String>();
			foreach (var jDomain in json["DATA"]) {
				toReturn.Add((Int32)jDomain["DOMAINID"], (String)jDomain["DOMAIN"]);
			}
			return toReturn;
		}

		private JObject GetDomainResourceList(Int32 domainId) {
			return GetData("https://api.linode.com/?api_key=" + ApiKey + "&api_action=domain.resource.list&domainid=" + domainId);
		}
		public Dictionary<Int32, String> GetDomainResources(Int32 domainId) {
			var json = GetDomainResourceList(domainId);
			var toReturn = new Dictionary<Int32, String>();
			foreach (var jResource in json["DATA"]) {
				var displayName = jResource["TYPE"].Value<String>().ToUpper() + " Record - " + jResource["NAME"].Value<String>();
				toReturn.Add((Int32)jResource["RESOURCEID"], (String)displayName);
			}
			return toReturn;
		}

		public void SetDynamicDns(String apiKey, String domainId, String resourceId) {
			GetData("https://api.linode.com/?api_key=" + apiKey + "&api_action=domain.resource.update&domainid=" + domainId + "&resourceid=" + resourceId + "&target=[remote_addr]");
		}

		private JObject GetData(String url) {

			var request = WebRequest.Create(url);
			var response = request.GetResponse();
			var dataStream = response.GetResponseStream();
			var reader = new StreamReader(dataStream);
			var strResponse = reader.ReadToEnd();
			reader.Close();
			response.Close();
			return JObject.Parse(strResponse);

		}

	}
}
