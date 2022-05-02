using Xunit;

namespace ApiTesting;

public class UnitTest
{
    [Fact]
    public async void Should_Post_NewHealer_bad()
    {
        var body = new Healer()
        {
            CharacterName = "asdfasf",
            HealingPerSecond = 0,
            ILevel = 0
        };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var results = await client.PostAsJsonAsync("/healer", body);
        results.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async void Should_Post_NewHealer_good()
    {
        var body = new Healer()
        {
            CharacterName = "asdfasf",
            HealingPerSecond = 100000,
            ILevel = 255
        };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var results = await client.PostAsJsonAsync("/healer", body);
        results.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}