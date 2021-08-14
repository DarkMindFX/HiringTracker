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
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
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
            var dalCandidateCommentDal = InitDal<ICandidateCommentDal>(serviceCfg);
            services.AddSingleton<ICandidateCommentDal>(dalCandidateCommentDal);
            services.AddSingleton<Dal.ICandidateCommentDal, Dal.CandidateCommentDal>();

            var dalCandidateDal = InitDal<ICandidateDal>(serviceCfg);
            services.AddSingleton<ICandidateDal>(dalCandidateDal);
            services.AddSingleton<Dal.ICandidateDal, Dal.CandidateDal>();

            var dalCandidatePropertyDal = InitDal<ICandidatePropertyDal>(serviceCfg);
            services.AddSingleton<ICandidatePropertyDal>(dalCandidatePropertyDal);
            services.AddSingleton<Dal.ICandidatePropertyDal, Dal.CandidatePropertyDal>();

            var dalCandidateSkillDal = InitDal<ICandidateSkillDal>(serviceCfg);
            services.AddSingleton<ICandidateSkillDal>(dalCandidateSkillDal);
            services.AddSingleton<Dal.ICandidateSkillDal, Dal.CandidateSkillDal>();

            var dalCommentDal = InitDal<ICommentDal>(serviceCfg);
            services.AddSingleton<ICommentDal>(dalCommentDal);
            services.AddSingleton<Dal.ICommentDal, Dal.CommentDal>();

            var dalDepartmentDal = InitDal<IDepartmentDal>(serviceCfg);
            services.AddSingleton<IDepartmentDal>(dalDepartmentDal);
            services.AddSingleton<Dal.IDepartmentDal, Dal.DepartmentDal>();

            var dalInterviewDal = InitDal<IInterviewDal>(serviceCfg);
            services.AddSingleton<IInterviewDal>(dalInterviewDal);
            services.AddSingleton<Dal.IInterviewDal, Dal.InterviewDal>();

            var dalInterviewFeedbackDal = InitDal<IInterviewFeedbackDal>(serviceCfg);
            services.AddSingleton<IInterviewFeedbackDal>(dalInterviewFeedbackDal);
            services.AddSingleton<Dal.IInterviewFeedbackDal, Dal.InterviewFeedbackDal>();

            var dalInterviewRoleDal = InitDal<IInterviewRoleDal>(serviceCfg);
            services.AddSingleton<IInterviewRoleDal>(dalInterviewRoleDal);
            services.AddSingleton<Dal.IInterviewRoleDal, Dal.InterviewRoleDal>();

            var dalInterviewStatusDal = InitDal<IInterviewStatusDal>(serviceCfg);
            services.AddSingleton<IInterviewStatusDal>(dalInterviewStatusDal);
            services.AddSingleton<Dal.IInterviewStatusDal, Dal.InterviewStatusDal>();

            var dalInterviewTypeDal = InitDal<IInterviewTypeDal>(serviceCfg);
            services.AddSingleton<IInterviewTypeDal>(dalInterviewTypeDal);
            services.AddSingleton<Dal.IInterviewTypeDal, Dal.InterviewTypeDal>();

            var dalPositionCommentDal = InitDal<IPositionCommentDal>(serviceCfg);
            services.AddSingleton<IPositionCommentDal>(dalPositionCommentDal);
            services.AddSingleton<Dal.IPositionCommentDal, Dal.PositionCommentDal>();

            var dalPositionDal = InitDal<IPositionDal>(serviceCfg);
            services.AddSingleton<IPositionDal>(dalPositionDal);
            services.AddSingleton<Dal.IPositionDal, Dal.PositionDal>();

            var dalPositionSkillDal = InitDal<IPositionSkillDal>(serviceCfg);
            services.AddSingleton<IPositionSkillDal>(dalPositionSkillDal);
            services.AddSingleton<Dal.IPositionSkillDal, Dal.PositionSkillDal>();

            var dalPositionStatusDal = InitDal<IPositionStatusDal>(serviceCfg);
            services.AddSingleton<IPositionStatusDal>(dalPositionStatusDal);
            services.AddSingleton<Dal.IPositionStatusDal, Dal.PositionStatusDal>();

            var dalProposalCommentDal = InitDal<IProposalCommentDal>(serviceCfg);
            services.AddSingleton<IProposalCommentDal>(dalProposalCommentDal);
            services.AddSingleton<Dal.IProposalCommentDal, Dal.ProposalCommentDal>();

            var dalProposalDal = InitDal<IProposalDal>(serviceCfg);
            services.AddSingleton<IProposalDal>(dalProposalDal);
            services.AddSingleton<Dal.IProposalDal, Dal.ProposalDal>();

            var dalProposalStatusDal = InitDal<IProposalStatusDal>(serviceCfg);
            services.AddSingleton<IProposalStatusDal>(dalProposalStatusDal);
            services.AddSingleton<Dal.IProposalStatusDal, Dal.ProposalStatusDal>();

            var dalProposalStepDal = InitDal<IProposalStepDal>(serviceCfg);
            services.AddSingleton<IProposalStepDal>(dalProposalStepDal);
            services.AddSingleton<Dal.IProposalStepDal, Dal.ProposalStepDal>();

            var dalRoleDal = InitDal<IRoleDal>(serviceCfg);
            services.AddSingleton<IRoleDal>(dalRoleDal);
            services.AddSingleton<Dal.IRoleDal, Dal.RoleDal>();

            var dalSkillDal = InitDal<ISkillDal>(serviceCfg);
            services.AddSingleton<ISkillDal>(dalSkillDal);
            services.AddSingleton<Dal.ISkillDal, Dal.SkillDal>();

            var dalSkillProficiencyDal = InitDal<ISkillProficiencyDal>(serviceCfg);
            services.AddSingleton<ISkillProficiencyDal>(dalSkillProficiencyDal);
            services.AddSingleton<Dal.ISkillProficiencyDal, Dal.SkillProficiencyDal>();

            var dalUserDal = InitDal<IUserDal>(serviceCfg);
            services.AddSingleton<IUserDal>(dalUserDal);
            services.AddSingleton<Dal.IUserDal, Dal.UserDal>();

            var dalUserRoleCandidateDal = InitDal<IUserRoleCandidateDal>(serviceCfg);
            services.AddSingleton<IUserRoleCandidateDal>(dalUserRoleCandidateDal);
            services.AddSingleton<Dal.IUserRoleCandidateDal, Dal.UserRoleCandidateDal>();

            var dalUserRolePositionDal = InitDal<IUserRolePositionDal>(serviceCfg);
            services.AddSingleton<IUserRolePositionDal>(dalUserRolePositionDal);
            services.AddSingleton<Dal.IUserRolePositionDal, Dal.UserRolePositionDal>();

            var dalUserRoleSystemDal = InitDal<IUserRoleSystemDal>(serviceCfg);
            services.AddSingleton<IUserRoleSystemDal>(dalUserRoleSystemDal);
            services.AddSingleton<Dal.IUserRoleSystemDal, Dal.UserRoleSystemDal>();

            /** Connection Tester for health endpoint **/
            var dalConnTest = InitDal<IConnectionTestDal>(serviceCfg);
            services.AddSingleton<IConnectionTestDal>(dalConnTest);
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
