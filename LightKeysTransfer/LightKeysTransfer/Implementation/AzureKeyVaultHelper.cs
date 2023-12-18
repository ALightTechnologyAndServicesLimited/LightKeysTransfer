using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using LightKeysTransfer.Abstract;
using LightKeysTransfer.Common;
using LightKeysTransfer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightKeysTransfer.Implementation
{
    internal class AzureKeyVaultHelper : IKeyTransferHelper
    {
        public string MainText => "Set or update secrets in Azure Key Vault";

        public KeyTransferResult Perform()
        {
            try
            {
                Console.WriteLine("Enter name of the vault:");
                var vault = Console.ReadLine();

                var kvUri = $"https://{vault}.vault.azure.net";

                Console.WriteLine("Enter key:");
                var key = Console.ReadLine();

                Console.WriteLine("Enter Secret:");
                var secret = CryptHelper.GetSensitiveText();

                var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
                client.SetSecret(new KeyVaultSecret(key, secret));

                Console.WriteLine($"The secret has been written for the key: {key} in vault: {vault}.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return KeyTransferResult.Errored;
            }

            return KeyTransferResult.Success;
        }
    }
}
