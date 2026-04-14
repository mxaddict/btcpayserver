using BTCPayServer.Models.StoreViewModels;
using Xunit;

// ParseDerivationStrategy tests that require BlsctDerivationStrategyFactory or AltNetworkSets.Navio
// cannot run here: BTCPayServer.Common references published NBXplorer.Client 5.0.5 and
// published NBitcoin.Altcoins — neither contains our fork's types.
// Those tests live in NBXplorer.Tests instead.

namespace BTCPayServer.Tests
{
    public class NavioPluginTests(ITestOutputHelper helper) : UnitTestBase(helper)
    {
        [Fact]
        public void DerivationSchemeViewModelIsBLSCTDefaultsFalse()
        {
            var viewModel = new DerivationSchemeViewModel();
            Assert.False(viewModel.IsBLSCT);
        }

        [Fact]
        public void BTCPayNetworkIsBLSCTDefaultsFalse()
        {
            var network = new BTCPayNetwork();
            Assert.False(network.IsBLSCT);
        }

        [Fact]
        public void BTCPayNetworkIsBLSCTCanBeSetTrue()
        {
            var network = new BTCPayNetwork { IsBLSCT = true };
            Assert.True(network.IsBLSCT);
        }
    }
}
