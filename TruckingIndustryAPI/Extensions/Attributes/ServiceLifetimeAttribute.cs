namespace TruckingIndustryAPI.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceLifetimeAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; }

        public ServiceLifetimeAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
