using Application.Client.Boundaries.Authenticate;
using Application.Client.Boundaries.Create;
using Application.Client.Boundaries.JoinAsAttendant;
using Application.Client.Boundaries.List;
using Application.Client.Commands;
using Application.Client.Handlers;
using Application.Commom.Boundaries;
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

            services.AddTransient<IRequestHandler<AuthenticateCommand, AuthenticateOutput>, AuthenticateHandler>();
            services.AddTransient<IRequestHandler<CreateCommand, CreateOutput>, CreateHandler>();
            services.AddTransient<IRequestHandler<JoinAsAttendantCommand, JoinAsAttendantOutput>, JoinAsAttendantHandler>();
            services.AddTransient<IRequestHandler<ListCommand, PaginatedResponse<ListOutput>>, ListHandler>();

            services.AddScoped<IAttendantUseCase, AttendantUseCase>();
            services.AddScoped<IClientUseCase, ClientUseCase>();

            services.AddScoped<IAttendantRepository, AttendantRepository>();
            services.AddScoped<IAttendantTokenRepository, AttendantTokenRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}
