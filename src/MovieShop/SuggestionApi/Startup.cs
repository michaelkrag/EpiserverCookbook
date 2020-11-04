using CommonLib.Cache;
using CommonLib.Cache.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuggestionApi.InformationRetrieval;
using SuggestionApi.Infrastructor.Document;
using SuggestionApi.NLP.Gram;
using SuggestionApi.NLP.TernaryTree;
using SuggestionApi.NLP.Tokenizers;
using SuggestionApi.NLP.Vocabularys;
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
            services.AddSingleton<IFileLocation, FileLocation>();
            services.AddSingleton<IVocabularyRepository, VocabularyRepository>();
            services.AddTransient<ITernarySearchFactory, TernarySearchFactory>();
            services.AddTransient<INGramRepository, NGramRepository>();
            services.AddTransient<ITokenizer, Tokenizer>();
            services.AddTransient<IDocumentStoreRepository, DocumentStoreRepository>();
            services.AddTransient<ISearchRespository, SearchRespository>();
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