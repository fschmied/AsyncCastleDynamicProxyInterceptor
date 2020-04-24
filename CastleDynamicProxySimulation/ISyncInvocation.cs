using System.Reflection;

interface ISyncInvocation
{
    object[] Arguments { get; set; }
    object Result { get; set; }
    MethodInfo Method { get; }
    void Proceed();
}