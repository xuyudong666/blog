using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BSS.Common.Helper;

public class Appsettings
{
    static IConfiguration Configuration { get; set; }
    static string ContentPath { get; set; }

    public Appsettings(string contentPath)
    {
        string path = "appsettings.json";
        Configuration = new ConfigurationBuilder().SetBasePath(contentPath)
            .Add(new JsonConfigurationSource { Path = path, Optional = false, ReloadOnChange = true })
            .Build();
    }

    public Appsettings(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public static string App(params string[] sections)
    {
        try
        {
            if(sections.Length != 0)
            {
                return Configuration[string.Join(":", sections)];
            }
        }
        catch (Exception)
        {
        }
        return string.Empty;
    }

    public static List<T> App<T> (params string[] sections)
    {
        List<T> list = new();
        Configuration.Bind(string.Join(":", sections, list));
        return list;
    }
}
