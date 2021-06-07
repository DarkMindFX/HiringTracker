using DalCreator.Generators;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace DalCreator
{
    class Program
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

            DateTime timestamp = DateTime.Now;

            // Generating Storprocs
            SqlScriptsGeneratorParams sqlGenParams = new SqlScriptsGeneratorParams();
            sqlGenParams.OutputRoot = settings.OutputRoot;
            sqlGenParams.TemplatesRoot = settings.TemplatesRoot;
            sqlGenParams.TemplateName = settings.TemplateName;
            sqlGenParams.Timestamp = timestamp;

            var sqlGenerator = new SqlScriptsGenerator(sqlGenParams);
            var sqlFiles = sqlGenerator.GenerateScripts(tables);

            // Generating Dal interfaces & entities
            DalEntitiesGeneratorParams dalEntGenParams = new DalEntitiesGeneratorParams();
            dalEntGenParams.OutputRoot = settings.OutputRoot;
            dalEntGenParams.TemplatesRoot = settings.TemplatesRoot;
            dalEntGenParams.TemplateName = settings.TemplateName;
            dalEntGenParams.Timestamp = timestamp;
            dalEntGenParams.DalNamespace = settings.DalNamespace;

            var dalEntGenerator = new DalEntitiesGenerator(dalEntGenParams);
            var dalEntFiles = dalEntGenerator.GenerateScripts(tables);

            // Generating Dal Implementations
            DalImplsGeneratorParams dalImplsGenParams = new DalImplsGeneratorParams();
            dalImplsGenParams.OutputRoot = settings.OutputRoot;
            dalImplsGenParams.TemplatesRoot = settings.TemplatesRoot;
            dalImplsGenParams.TemplateName = settings.TemplateName;
            dalImplsGenParams.Timestamp = timestamp;
            dalImplsGenParams.DalNamespace = settings.DalNamespace;
            dalImplsGenParams.DalImplNamespace = settings.DalImplNamespace;

            var dalImplsGenerator = new DalImplsGenerator(dalImplsGenParams);
            var dalImplsFiles = dalImplsGenerator.GenerateScripts(tables);

            // Generating DTOs & API Dals
            DtosGeneratorParams dtosGenParams = new DtosGeneratorParams();
            dtosGenParams.OutputRoot = settings.OutputRoot;
            dtosGenParams.TemplatesRoot = settings.TemplatesRoot;
            dtosGenParams.TemplateName = settings.TemplateName;
            dtosGenParams.Timestamp = timestamp;
            dtosGenParams.DtoNamespace = settings.DtoNamespace;
            dtosGenParams.ApiDalNamespace = settings.ApiDalNamespace;

            var dtosGenerator = new DtosGenerator(dtosGenParams);
            var dtosFiles = dtosGenerator.GenerateScripts(tables);

            // Generating Dal Tests Implementations
            DalTestsGeneratorParams dalTestsGenParams = new DalTestsGeneratorParams();
            dalTestsGenParams.OutputRoot = settings.OutputRoot;
            dalTestsGenParams.TemplatesRoot = settings.TemplatesRoot;
            dalTestsGenParams.TemplateName = settings.TemplateName;
            dalTestsGenParams.Timestamp = timestamp;
            dalTestsGenParams.DalNamespace = settings.DalNamespace;
            dalTestsGenParams.DalImplNamespace = settings.DalImplNamespace;
            dalTestsGenParams.DalTestsNamespace = settings.DalTestsNamespace;
            dalTestsGenParams.ConnectionString = initParams.ConnectionString;

            var dalTestsGenerator = new DalTestsGenerator(dalTestsGenParams);
            var dalTestsFiles = dalTestsGenerator.GenerateScripts(tables);

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
