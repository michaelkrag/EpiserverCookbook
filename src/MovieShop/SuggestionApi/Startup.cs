using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLib.Cache;
using CommonLib.Cache.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLPLib.Tokenizers;
using NLPLib.Vocabularys;
using SuggestionApi.NLP.TernaryTree;
using SuggestionApi.NLP.Vocabularys.Repository;
using SuggestionApi.Services;

namespace SuggestionApi
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
            services.AddControllers();
            services.AddTransient<IndexService, IndexService>();
            services.AddSingleton<ICache, MsObjectCache>();
            services.AddTransient<IndexFactory, IndexFactory>();
            services.AddTransient<IVocabulary, Vocabulary>();
            services.AddSingleton<IFileLocation, FileLocation>();
            services.AddTransient<IVocabularyFileFactory, VocabularyFileFactory>();
            services.AddTransient<ITernarySearchFactory, TernarySearchFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}