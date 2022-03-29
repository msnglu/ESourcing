using ESourcing.Order.Consumers;

namespace ESourcing.Order.Extensions
{
    public static class ApplicationBuilderExtensions
    {
       
        public static EventBusOrderCreateConsumer Listener { get; set; } = null!;

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusOrderCreateConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();


            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);
            return app;
        }

        private static void OnStarted()
        {
            Listener.Consume();

        }

        private static void OnStopping()
        {
            Listener.Disconnect();

        }
    }
}
