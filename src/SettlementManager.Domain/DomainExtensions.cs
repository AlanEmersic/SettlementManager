﻿using Microsoft.Extensions.DependencyInjection;

namespace SettlementManager.Domain;

public static class DomainExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}