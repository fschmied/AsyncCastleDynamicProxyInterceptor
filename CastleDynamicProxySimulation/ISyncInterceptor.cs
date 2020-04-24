namespace AsyncInterceptor.CastleDynamicProxySimulation
{
    interface ISyncInterceptor
    {
        void Intercept(ISyncInvocation invocation);
    }
}