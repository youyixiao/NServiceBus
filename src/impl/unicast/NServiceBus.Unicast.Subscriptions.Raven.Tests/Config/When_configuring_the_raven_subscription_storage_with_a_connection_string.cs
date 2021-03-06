using NUnit.Framework;
using Raven.Client.Document;

namespace NServiceBus.Unicast.Subscriptions.Raven.Tests.Config
{
    [TestFixture]
    public class When_configuring_the_raven_subscription_storage_with_a_connection_string : WithRavenDbServer
    {
        RavenSubscriptionStorage subscriptionStorage;
        string connectionStringName;

        [TestFixtureSetUp]
        public void SetUp()
        {
            using (var server = GetNewServer())
            {
                connectionStringName = "Raven";

                var config = Configure.With(new[] {GetType().Assembly})
                    .DefaultBuilder()
                    .RavenSubscriptionStorage(connectionStringName);

                subscriptionStorage = config.Builder.Build<RavenSubscriptionStorage>();
            }
        }

        [Test]
        public void It_should_use_a_document_store_configured_with_the_connection_string()
        {
            var store = subscriptionStorage.Store as DocumentStore;
            Assert.AreEqual(connectionStringName, store.ConnectionStringName);
        }
    }
}