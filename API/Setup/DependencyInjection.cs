using Application.Client.Boundaries.Create;
using Application.Client.Commands;
using Application.Client.Handlers;
using Application.UseCase.Attendant;
using Application.UseCase.Client;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Repository;
using Infrastructure.Repository;
using MediatR;

namespace API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddTransient<IRequestHandler<CreateCommand, CreateOutput>, CreateHandler>();

            services.AddScoped<IAttendantUseCase, AttendantUseCase>();
            services.AddScoped<IClientUseCase, ClientUseCase>();

            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}
