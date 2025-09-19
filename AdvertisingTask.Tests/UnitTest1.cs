using AdvertisingTask.Services;
using System.Net;
using Xunit;

namespace AdvertisingTask.Tests
{
    public class AdPlatformStorageTests
    {
        [Fact]
        public void Search_Given_Location()
        {
            var storage = new AdPlatformStorage();

            var inputLines = new[]

            {
                "PlatformA:/ru",

                "PlatformB:/ru/svrd",

                "PlatformC:/ru/svrd/revda"
            };
            storage.LoadFromLines(inputLines);

            var result = storage.Search("/ru/svrd/revda");

            Assert.Equal(3, result.Count);

            Assert.Contains("PlatformA", result);

            Assert.Contains("PlatformB", result);

            Assert.Contains("PlatformC", result);
        }

        [Fact]
        public void Search_NotFullLocation()
        {
            var storage = new AdPlatformStorage();

            storage.LoadFromLines(new[] { "PlatformA:/ru", "PlatformB:/ru/svrd" });

            var result = storage.Search("/ru/svrd/revda");

            Assert.Contains("PlatformA", result);

            Assert.Contains("PlatformB", result);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Search_UnknownLocation()
        {
            var storage = new AdPlatformStorage();

            storage.LoadFromLines(new[] { "PlatformA:/ru" });

            var result = storage.Search("/us/ny");

            Assert.Empty(result);
        }

        [Fact]
        public void Search_UselessSlashes()
        {
            var storage = new AdPlatformStorage();
            storage.LoadFromLines(new[] { "PlatformA:/ru/svrd" });

            var result = storage.Search("/ru/svrd/");

            Assert.Contains("PlatformA", result);
        }

        [Fact]
        public void LoadFromLines_IgnoresEmptyLines()
        {
            var storage = new AdPlatformStorage();
            storage.LoadFromLines(new[] { "", "   ", null });

            var result = storage.Search("/ru");
            Assert.Empty(result);
        }

        [Fact]
        public void LoadFromLines_IgnoresLinesWithoutColon()
        {
            var storage = new AdPlatformStorage();
            storage.LoadFromLines(new[] { "PlatformA /ru/svrd" });

            var result = storage.Search("/ru/svrd");
            Assert.Empty(result);
        }

   

    }
}
