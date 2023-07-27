using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace LightKeysTransfer.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthOf0()
        {
            var plainText = "";
            Util.GenerateRSAKeyPair();

            var enc = Util.EncryptText(plainText);
            var dec = Util.DecryptText(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthOf1()
        {
            var plainText = "1";
            Util.GenerateRSAKeyPair();

            var enc = Util.EncryptText(plainText);
            var dec = Util.DecryptText(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthDivisibleBy3AndRemainder0()
        {
            var plainText = "123";
            Util.GenerateRSAKeyPair();

            var enc = Util.EncryptText(plainText);
            var dec = Util.DecryptText(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthDivisibleBy3AndRemainder1()
        {
            var plainText = "1234";
            Util.GenerateRSAKeyPair();

            var enc = Util.EncryptText(plainText);
            var dec = Util.DecryptText(enc);

            Assert.AreEqual(plainText, dec);
        }

        [Test]
        public void TestEncryptDecryptWithStringLengthDivisibleBy3AndRemainder2()
        {
            var plainText = "12345";
            Util.GenerateRSAKeyPair();

            var enc = Util.EncryptText(plainText);
            var dec = Util.DecryptText(enc);

            Assert.AreEqual(plainText, dec);
        }

    }
}