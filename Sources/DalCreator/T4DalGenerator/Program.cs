using DataModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using T4DalGenerator.Generators;
using T4DalGenerator.Templates;

namespace T4DalGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = PrepareConfig();
            ModelExtractorParams initParams = new ModelExtractorParams();
            var settings = config.GetSection("DalCreatorSettings").Get<DalCreatorSettings>();
            initParams.ConnectionString = settings.ConnectionString;

            ModelExtractor extractor = new ModelExtractor();
            extractor.Init(initParams);
            var tables = extractor.GetModel();

            var timestamp = DateTime.Now;

            Generate<StorProcsGenerator>(tables, settings, timestamp);
            Generate<IDalsGenerator>(tables, settings, timestamp);
            Generate<SQLDalGenerator>(tables, settings, timestamp);
            Generate<SQLDalTestGenerator>(tables, settings, timestamp);
            Generate<EntitiesGenerator>(tables, settings, timestamp);
            Generate<DtosGenerator>(tables, settings, timestamp);
            Generate<IServiceDalsGenerator>(tables, settings, timestamp);
            Generate<IServiceDalsGenerator>(tables, settings, timestamp);
            Generate<ServiceDalsImplGenerator>(tables, settings, timestamp);
            Generate<ConvertorsGenerator>(tables, settings, timestamp);
            Generate<EntityControllerGenerator>(tables, settings, timestamp);
        }

        private static IList<string> Generate<TGenerator>(IList<DataModel.DataTable> tables, DalCreatorSettings settings, DateTime timestamp) where TGenerator : IGenerator
        {
            List<string> result = new List<string>();
            var genParams = new Generators.GeneratorParams()
            {
                Settings = settings,
                Timestamp = timestamp,
            };

            var generator = (TGenerator)Activator.CreateInstance(typeof(TGenerator), new object[] { genParams });

            foreach (var t in tables)
            {
                genParams.Table = t;
                var files = generator.Generate();
                result.AddRange(files);
            }

            return result;
        }

        private static IConfigurationRoot PrepareConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return config;
        }
    }
}
