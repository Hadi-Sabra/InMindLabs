using System.Reflection;

namespace Lab1.Services;

public class ObjectMapperService
{
    public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        TDestination destination = new TDestination();
        
        var sourceProperties = typeof(TSource).GetProperties();
        var destinationProperties = typeof(TDestination).GetProperties();

        // Mapping Rules
        var dynamicMappings = new Dictionary<string, string>
        {
            { "Id", $"{typeof(TSource).Name}Id" },   // E.g., StudentId, TeacherId, etc.
            { "Name", "FullName" }                   // Maps FullName → Name
            // We can add several keys and their value if needed 
        };

        foreach (var destProp in destinationProperties)
        {
            // Matching by exact property name
            var sourceProp = sourceProperties.FirstOrDefault(sp =>
                sp.Name.Equals(destProp.Name, StringComparison.OrdinalIgnoreCase)
            );

            // Dynamic Mapping using Dictionary (Handles multiple attributes) in this way it is more scalable 
            // We can also use an external library called AutoMapper for dynamic mapping
            if (sourceProp == null && dynamicMappings.TryGetValue(destProp.Name, out var mappedSourceName))
            {
                sourceProp = sourceProperties.FirstOrDefault(sp => sp.Name == mappedSourceName);
            }

            // Setting the value if matching property found
            if (sourceProp != null && destProp.CanWrite)
            {
                destProp.SetValue(destination, sourceProp.GetValue(source));
            }
        }

        return destination;
    }
}