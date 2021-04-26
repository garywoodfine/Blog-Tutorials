using MethodBoundaryAspect.Fody.Attributes;
using Serilog;

namespace Cms.Aspects
{
    public sealed class LoggingAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs arg)
        {
            Log.Information($"Init: {arg.Method.DeclaringType.FullName}.{arg.Method.Name} [{arg.Arguments.Length}] params");
            foreach (var item in arg.Method.GetParameters())
            {
                Log.Debug($"{item.Name}: {arg.Arguments[item.Position]}");
            }
        }
        public override void OnExit(MethodExecutionArgs args)
        {
            Log.Information($"Exit: [{args.ReturnValue}]");
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Log.Error($"OnException: {args.Exception.GetType()}: {args.Exception.Message}");
        }
    }
}