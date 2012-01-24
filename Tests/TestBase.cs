using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace Tests
{
    public abstract class TestBase
    {
        public IDocumentStore DocumentStore { get; set; }
        public IDocumentSession DocumentSession { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            DocumentStore = new EmbeddableDocumentStore{RunInMemory = true}.Initialize();
            DocumentSession = DocumentStore.OpenSession();
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (DocumentSession != null)
            {
                DocumentSession.Dispose();
            }

            if (DocumentStore != null)
            {
                DocumentStore.Dispose();
            }
        }
    }
}