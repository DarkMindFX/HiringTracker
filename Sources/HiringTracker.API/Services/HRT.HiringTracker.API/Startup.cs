using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Helpers;
using HRT.HiringTracker.API.MiddleWare;
using HRT.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HRT.HiringTracker.API
{
    public class Startup
    {
        #region Service Config
        class ServiceConfig
        {
            [JsonPropertyName("DALType")]
            public string DALType { get; set; }


            [JsonPropertyName("DALInitParams")]
            public Dictionary<string, string> DALInitParams { get; set; }
        }

        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetSection("ServiceConfig").Get<ServiceConfig>();
            PrepareComposition();

            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            AddInjections(services, serviceConfig);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void PrepareComposition()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(AssemblyDirectory);
            catalog.Catalogs.Add(directoryCatalog);
            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);
        }

        private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private CompositionContainer Container
        {
            get;
            set;
        }

        private void AddInjections(IServiceCollection services, ServiceConfig serviceCfg)
        {
            var dalUser = InitDal<IUserDal>(serviceCfg);
            services.AddSingleton<IUserDal>(dalUser);

            var dalSkill = InitDal<ISkillDal>(serviceCfg);
            services.AddSingleton<ISkillDal>(dalSkill);

            var dalSkillProf = InitDal<ISkillProficiencyDal>(serviceCfg);
            services.AddSingleton<ISkillProficiencyDal>(dalSkillProf);

            var dalPosition = InitDal<IPositionDal>(serviceCfg);
            services.AddSingleton<IPositionDal>(dalPosition);

            var dalPositionStatus = InitDal<IPositionStatusDal>(serviceCfg);
            services.AddSingleton<IPositionStatusDal>(dalPositionStatus);

            var dalProposalStatus = InitDal<IProposalStatusDal>(serviceCfg);
            services.AddSingleton<IProposalStatusDal>(dalProposalStatus);

            var dalProposalStep = InitDal<IProposalStepDal>(serviceCfg);
            services.AddSingleton<IProposalStepDal>(dalProposalStep);

            var dalCandidate = InitDal<ICandidateDal>(serviceCfg);
            services.AddSingleton<ICandidateDal>(dalCandidate);

            services.AddSingleton<Dal.ICandidateDal, Dal.CandidateDal>();
            services.AddSingleton<Dal.IProposalStatusDal, Dal.ProposalStatusDal>();
            services.AddSingleton<Dal.IProposalStepDal, Dal.ProposalStepDal>();
            services.AddSingleton<Dal.IPositionStatusDal, Dal.PositionStatusDal>();
            services.AddSingleton<Dal.IPositionDal, Dal.PositionDal>();
            services.AddSingleton<Dal.ISkillDal, Dal.SkillDal>();
            services.AddSingleton<Dal.ISkillProficiencyDal, Dal.SkillProficiencyDal>();
            services.AddSingleton<Dal.IUserDal, Dal.UserDal>();
        }

        private TDal InitDal<TDal>(ServiceConfig serviceCfg) where TDal : IInitializable
        {
            var dal = Container.GetExportedValue<TDal>(serviceCfg.DALType);
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters = serviceCfg.DALInitParams;
            dal.Init(dalInitParams);

            return dal;

        }
    }
}
