using DHHelper.Models.Base;
using DHHelper.Options;
using DHSDK.Clients;
using DHSDK.Models.DTO.DataGo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DHSDKUnitTest;

public class Tests
{
    private CovidClient? _covidClient;


    [SetUp]
    public void Setup()
    {

        var config = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json").Build();

        var service_provider = new ServiceCollection();

        service_provider.AddSingleton<IConfiguration>(config);

        service_provider.AddOptions<ApiClientOption>().Configure<IConfiguration>((settings, configuration) =>
        {
            configuration.GetSection("DataGo").Bind(settings);
        });


        service_provider.AddLogging();
        service_provider.AddHttpClient();

        service_provider.AddScoped<CovidClient>();

        var services = service_provider.BuildServiceProvider();

        _covidClient = services.GetService<CovidClient>();

    }

    [Test]
    public async Task Test1()
    {
        RequestCovid19 req = new RequestCovid19("jIRXSameCnqB8%2FDno7viFXvxm26ZGKzZ3zixLbBqpBJbQOM6MzgcCcD4oGlxXgstUu9MbX8qQcVv%2FqeKSaScUw%3D%3D")
        {
            PageNo = 0,
            NumOfRows = 10,
            StartCreateDt = DateTime.Today,
            EndCreateDt = DateTime.Today.AddDays(1)
        };

        var res = await _covidClient!.SendRequestAsync<ResponseCovid19>(req);



        Assert.Pass();
    }
}
