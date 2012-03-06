using Raven.Client.Listeners;
using Raven.Json.Linq;
using Shared.Entities;

namespace ConsoleApplication1.Migration
{
	public class CustomerVersion1ToVersion2Converter : IDocumentConversionListener
	{
		public void EntityToDocument(object entity, RavenJObject document, RavenJObject metadata)
		{
		}

		public void DocumentToEntity(object entity, RavenJObject document, RavenJObject metadata)
		{
			var c = entity as Customer;
			if (c == null || metadata.Value<int>("Customer-Schema-Version") >= 2)
			{
				return;
			}

			c.Name = document.Value<string>("FirstName") + " " + document.Value<string>("LastName");
			c.Email = document.Value<string>("Email");
		}
	}
}