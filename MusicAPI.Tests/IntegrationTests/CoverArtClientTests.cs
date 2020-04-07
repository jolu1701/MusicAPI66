using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicAPI.APIs;
using System;
using System.Threading.Tasks;

namespace MusicAPI.UnitTests
{
    [TestClass]
    public class CoverArtClientTests
    {
        [TestMethod]
        public async Task GetUrl_CorrectId_ReturnsString()
        {
            //Arrange
            string id = "f1afec0b-26dd-3db5-9aa1-c91229a74a24";
            var apiHelper = new ApiHelper();
            var coverArt = new CoverArtClient(apiHelper);
            //Act
            var result = await coverArt.GetUrl(id);

            //Assert
            //StringAssert.Contains(result, "http");
            Assert.IsTrue(result.Contains("http"));
        }

        [TestMethod]
        public async Task GetUrl_UnknownId_ReturnsString()
        {
            //Arrange
            string id = "f1afec0b-26dd-3db5-99a1-c91229a74a24";
            var apiHelper = new ApiHelper();
            var coverArt = new CoverArtClient(apiHelper);
            //Act
            var result = await coverArt.GetUrl(id);

            //Assert
            StringAssert.Contains(result, "No cover found");
        }

        [TestMethod]
        public void GetUrl_BadId_ReturnsException()
        {
            //Arrange
            string id = "sfdsfdfsdf";
            var apiHelper = new ApiHelper();
            var coverArt = new CoverArtClient(apiHelper);

            //Act & Assert
            Assert.ThrowsExceptionAsync<SystemException>(async () => await coverArt.GetUrl(id));
        }
    }
}
