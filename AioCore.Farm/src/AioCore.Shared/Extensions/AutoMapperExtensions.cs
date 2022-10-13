using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AioCore.Shared.Extensions;

public static class AutoMapperExtensions
{
    private static IMapper _mapper = null!;

    public static IMapper RegisterMap(this IMapper mapper)
    {
        _mapper = mapper;
        return mapper;
    }

    public static T To<T>(this object? source)
    {
        return _mapper.Map<T>(source);
    }

    public static object To(this object source, Type destinationType)
    {
        return _mapper.Map(source, source.GetType(), destinationType);
    }

    public static T To<T>(this object source, Action<IMappingOperationOptions> opts)
    {
        return _mapper.Map<T>(source, opts);
    }

    public static T To<T>(this object source, T dest)
    {
        return _mapper.Map(source, dest);
    }

    public static IServiceCollection AddMapper<TProfile>(this IServiceCollection services)
        where TProfile : Profile, new()
    {
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<TProfile>(); });
        services.AddSingleton(mapperConfig.CreateMapper().RegisterMap());
        return services;
    }
}