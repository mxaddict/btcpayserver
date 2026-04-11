using BTCPayServer.Common;
using BTCPayServer.Hosting;
using BTCPayServer.Payments;
using BTCPayServer.Services;
using Microsoft.Extensions.DependencyInjection;
using NBitcoin;

namespace BTCPayServer.Plugins.Altcoins;

public partial class AltcoinsPlugin
{
    public void InitNavio(IServiceCollection services)
    {
        var nbxplorerNetwork = NBXplorerNetworkProvider.GetNAV();
        var network = new BTCPayNetwork()
        {
            CryptoCode = nbxplorerNetwork.CryptoCode,
            DisplayName = "Navio",
            NBXplorerNetwork = nbxplorerNetwork,
            DefaultRateRules = new[]
            {
                "NAV_X = NAV_BTC * BTC_X",
                "NAV_BTC = coingecko(NAV_BTC)"
            },
            CryptoImagePath = "imlegacy/navio.svg",
            DefaultSettings = BTCPayDefaultSettings.GetDefaultSettings(ChainName),
            CoinType = ChainName == ChainName.Mainnet
                ? new KeyPath("0'")
                : new KeyPath("1'"),
            SupportRBF = true,
            SupportPayJoin = false,
            VaultSupported = true
        }.SetDefaultElectrumMapping(ChainName);

        var blockExplorerLink = ChainName == ChainName.Mainnet
            ? "https://explorer.navio.org/tx/{0}"
            : "https://testnet.explorer.navio.org/tx/{0}";

        services.AddBTCPayNetwork(network)
            .AddTransactionLinkProvider(
                PaymentTypes.CHAIN.GetPaymentMethodId(
                    nbxplorerNetwork.CryptoCode),
                new DefaultTransactionLinkProvider(blockExplorerLink));
    }
}