using Application.Client.Boundaries.Authenticate;
using Application.Client.Boundaries.Create;
using Application.Client.Boundaries.GetAttendantById;
using Application.Client.Boundaries.GetAttendantToken;
using Application.Client.Boundaries.GetClientByid;
using Application.Client.Boundaries.JoinAsAttendant;
using Application.Client.Boundaries.ListAttendant;
using Application.Client.Boundaries.ListClient;
using Application.Client.Boundaries.UpdateClient;
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
            services.AddTransient<IRequestHandler<GetAttendantByIdCommand, GetAttendantByIdOutput>, GetAttendantByIdHandler>();
            services.AddTransient<IRequestHandler<GetAttendantTokenCommand, GetAttendantTokenOutput>, GetAttendantTokenHandler>();
            services.AddTransient<IRequestHandler<GetClientByIdCommand, GetClientByIdOutput>, GetClientByIdHandler>();
            services.AddTransient<IRequestHandler<JoinAsAttendantCommand, JoinAsAttendantOutput>, JoinAsAttendantHandler>();
            services.AddTransient<IRequestHandler<ListAttendantCommand, PaginatedResponse<ListAttendantOutput>>, ListAttendantHandler>();
            services.AddTransient<IRequestHandler<ListClientCommand, PaginatedResponse<ListClientOutput>>, ListClientHandler>();
            services.AddTransient<IRequestHandler<UpdateClientCommand, UpdateClientOutput>, UpdateClientHandler>();

            services.AddScoped<IAttendantUseCase, AttendantUseCase>();
            services.AddScoped<IClientUseCase, ClientUseCase>();

            services.AddScoped<IAttendantRepository, AttendantRepository>();
            services.AddScoped<IAttendantTokenRepository, AttendantTokenRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}
