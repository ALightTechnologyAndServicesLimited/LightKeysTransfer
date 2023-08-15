using Microsoft.VisualStudio.TestPlatform.Utilities;
using TextCopy;

namespace LightKeysTransfer.UnitTests
{
    public class Tests
    {
        CryptHelper cryptHelper = new();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthOf0()
        {
            var plainText = "";
            cryptHelper.GenerateRSAKeyPair();

            var enc = cryptHelper.EncryptRSA(plainText);
            var dec = cryptHelper.DecryptRSA(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthOf1()
        {
            var plainText = "1";
            cryptHelper.GenerateRSAKeyPair();

            var enc = cryptHelper.EncryptRSA(plainText);
            var dec = cryptHelper.DecryptRSA(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthDivisibleBy3AndRemainder0()
        {
            var plainText = "123";
            cryptHelper.GenerateRSAKeyPair();

            var enc = cryptHelper.EncryptRSA(plainText);
            var dec = cryptHelper.DecryptRSA(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthDivisibleBy3AndRemainder1()
        {
            var plainText = "1234";
            cryptHelper.GenerateRSAKeyPair();

            var enc = cryptHelper.EncryptRSA(plainText);
            var dec = cryptHelper.DecryptRSA(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthDivisibleBy3AndRemainder2()
        {
            var plainText = "12345";
            cryptHelper.GenerateRSAKeyPair();

            var enc = cryptHelper.EncryptRSA(plainText);
            var dec = cryptHelper.DecryptRSA(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestGenerateTripleDES()
        {
            cryptHelper.GenerateNewTripleDES();
            Assert.Pass();
        }

        [Test]
        public void TestGenerateRSAKeyPair()
        {
            cryptHelper.GenerateRSAKeyPair();
            Assert.Pass();
        }

        [Test]
        public void TestRSASE2E()
        {
            var testText = "Hello";

            //Client
            var client = new CryptHelper();
            var server = new CryptHelper();

            client.GenerateRSAKeyPair();
            client.CopyPublicKey();
            var pk = ClipboardService.GetText();

            server.InitializeRSA(pk);
            CryptHelper.ClearClipBoard();

            var encText = server.EncryptRSA(testText);
            var plainText = client.DecryptRSA(encText);

            Assert.AreEqual(testText, plainText);
        }

        [Test]
        public void TestTripleDES()
        {
            var testText = "Hello";

            var server = new CryptHelper();
            var client = new CryptHelper();

            client.GenerateRSAKeyPair();
            client.CopyPublicKey();
            var pk = ClipboardService.GetText();

            server.InitializeRSA(pk);
            CryptHelper.ClearClipBoard();

            server.GenerateNewTripleDES();
            var key = server.GetEncryptedTripleDESKey();
            var iv = server.GetEncryptedTripleDESIV();

            client.ImportTripleDES(key, iv);
            var enc = server.EncryptTripleDES(testText);
            //var enc = server.EncryptText(testText, true);
            var dec = client.DecryptTripleDES(enc);

            Assert.AreEqual(testText, dec);
        }
    }
}