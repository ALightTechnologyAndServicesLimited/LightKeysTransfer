using LightKeysTransfer.Common;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Text;
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
        public void TestEncryptDecryptWithStringLengthOf84()
        {
            var sb = new StringBuilder();
            var i = 0;
            while (sb.Length < 84)
            {
                sb.Append(i++);
            }

            var plainText = sb.ToString();
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
            var testText = "3viMgEe9w3QREg9KaJTtwdClUmOTeskkBy7R1g7+ceL8Anbllu5ODz9E69lcSbfE4hg+EcUnbVdTWy/A9FBWfvrltMvTSXPuwjdsIj76PHnrThB1DFNdEaJIc6rgqF7is5EqR8ROiFQBlva4iAziPpm8UuXEoHY9H06hmxMmSToNaumfkrp2HkQbIu+UtRLmbyClvUSVoBFZbeRsXzi5o6Pi6pBQW+WYiz3tcBnomggznjJPl/j++OWSPceP/KiMcnVx9KjPiPMtLR01epr4GFn0642mRGghdDt8L+EwMv+K/mI/UNEvlBmmnqQ+8lrbnoVJq9E6/8IwKB+8MTfb6A==";
            //var testText = "Hello";
            var server = new CryptHelper();
            var client = new CryptHelper();

            //var ret = server.EncryptTripleDES2(testText);

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