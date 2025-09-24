using System;
using Blazor.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.DependencyInjection;

namespace DragonChessUI;

public sealed class PlayerManager : CircuitHandler
{
    public Guid Id { get; private set; }
    public GameManager? Game { get; set; }

    public PlayerManager()
    {
        this.Id = Guid.NewGuid();
    }

    public override Task OnConnectionUpAsync(Circuit circuit,
                                             CancellationToken cancellationToken)
    {
        // Adding to player list in first render (not here)

        return Task.CompletedTask;
    }

    public override Task OnConnectionDownAsync(Circuit circuit,
                                               CancellationToken cancellationToken)
    {
        // Remove from player list
        this.Game?.RemovePlayer(this);

        return Task.CompletedTask;
    }
}

public static class PlayerManagerServiceCollectionExtensions
{
      public static IServiceCollection AddPlayerManager(
        this IServiceCollection services)
    {
        services.AddScoped<PlayerManager>();
        services.AddScoped<CircuitHandler, PlayerManager>(sp => sp.GetRequiredService<PlayerManager>());

        return services;
    }
}
