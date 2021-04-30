using System;
using BankAccountReader;
using BankCommandProcessor;
using BankDomain;
using BankStore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BankDesk
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.TryAddSingleton<ITransactionEventStore, BankTransactionEventStore>();
            
            // This will provide an implementation for the storage mechanism the
            // hexagon is expecting. The hexagon doesn't know what it will get
            // that it is working with an event store, it just knows it provided
            // and interface (a contract) and it will operate on the virtues of
            // the contract it has bound itself to follow.
            services.TryAddScoped(CreateEventStore());
            
            services.TryAddScoped(CreateTheBalanceReader());
            
            /* This is a different way of registering, instead of having a separate method
             * You will need to see how you can fix this little problem, it is realted to
             * port implementation...
             */
            throw new Exception("Make me work... :'-(");
            // services.TryAddScoped<IReadStatement>(container => new StatementReport(container.GetService<ITransactionEventStore>()));
        }

        private static Func<IServiceProvider, IReadBalance> CreateTheBalanceReader()
        {
            // Func<IServiceProvider, IReadBalance> fn = 
            //     container => 
            //         new BalanceReport(
            //             container
            //                 .GetService<ITransactionEventStore>());
            throw new Exception("Clean this up!!");
            /*
             * We need to ensure the ports of our hexagon have implementations provided...
             * the above line above this exception achieves this but the following line is
             * cleaner and does the same thing. Uncomment the line below an delete the
             * throw and the messy version above.
             */
            return container => new BalanceReport(container.GetService<ITransactionEventStore>());
        }

        private static Func<IServiceProvider, ICommandHandler> CreateEventStore()
        {
            // Func<IServiceProvider, ICommandHandler> fn = 
            //     container => 
            //         new CommandHandler(
            //             container
            //                 .GetService<ITransactionEventStore>());
            //
            throw new Exception("Clean this up!!");
            /*
             * We need to ensure the ports of our hexagon have implementations provided...
             * the above line above this exception achieves this but the following line is
             * cleaner and does the same thing. Uncomment the line below an delete the
             * throw and the messy version above.
             */
            return container => new CommandHandler(container.GetService<ITransactionEventStore>());
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}