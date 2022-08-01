using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;
using DHHelper.Helper;
using Newtonsoft.Json;
using OpenApiHelperUnitTest.Models;

namespace OpenApiHelperUnitTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public void ToQueryString()
    {

        SampleModel request = new SampleModel()
        {
            Name = "Test",
            Qty = 1,
            Status = EStatus.BLOCKED
        };

        string queryString = request.ConvertToQueryString();
        
    }



}