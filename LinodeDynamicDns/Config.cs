using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace LinodeDynamicDns {
	public class Config {

		private const String filename = "settings.json";

		public enum Keys {
			ApiKey,
			DomainId,
			ResourceId
		}

		private static Dictionary<Keys, String> KeyMappings =>
			new Dictionary<Keys, String>() {
				[Keys.ApiKey] = "ApiKey",
				[Keys.DomainId] = "DomainId",
				[Keys.ResourceId] = "ResourceId"
			};

		public static String Get(Keys key) {
			return ReadFromFile(key);
		}

		public static void SetAll(Dictionary<Keys, String> values) {
			var json = new JObject();
			foreach (var kvp in values) {
				json.Add(KeyMappings[kvp.Key], kvp.Value);
			}
			File.WriteAllText(filename, json.ToString());
		}

		private static String ReadFromFile(Keys key) {

			var json = JObject.Parse(File.ReadAllText(filename));
			return json[KeyMappings[key]].Value<String>();

		}

	}
}
