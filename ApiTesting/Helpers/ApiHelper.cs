namespace raiding.API.helpers;

public class Helper
{

    public static WebApplicationFactory<Program> CreateApi()
    {
        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IHealerRepository));
                services.Remove(descriptor);

                var fakeHealerRepository = new ServiceDescriptor(typeof(IHealerRepository), typeof(FakeHealerRepository), ServiceLifetime.Transient);
                services.Add(fakeHealerRepository);

            });

        });

        return application;

    }
}