using Molecules.Shared.Logger;
using Serilog;

namespace Molecules.Logger.Logger
{
    public class MoleculesLogger : IMoleculesLogger
    {

        public MoleculesLogger() { }

        public void LogVerbose(string message)
        {
            Log.Verbose(message);
        }

        public void LogVerbose(string message, params object?[]? propertyValues)
        {
            Log.Verbose(message, propertyValues);
        }

        public void LogVerbose(Exception exception, string message, params object?[]? propertyValues)
        {
            Log.Verbose(exception, message, propertyValues);
        }

        public void LogDebug(string message)
        {
            Log.Debug(message);
        }

        public void LogDebug(string message, params object?[]? propertyValues)
        {
            Log.Debug(message, propertyValues);
        }

        public void LogDebug(Exception exception, string message, params object?[]? propertyValues)
        {
            Log.Debug(exception, message, propertyValues);
        }

        public void LogInformation(string message)
        {
            Log.Information(message);
        }

        public void LogInformation(string message, params object?[]? propertyValues)
        {
            Log.Information(message, propertyValues);
        }

        public void LogInformation(Exception exception, string message, params object?[]? propertyValues)
        {
            Log.Information(exception, message, propertyValues);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public void LogWarning(string message, params object?[]? propertyValues)
        {
            Log.Warning(message, propertyValues);
        }

        public void LogWarning(Exception exception, string message, params object?[]? propertyValues)
        {
            Log.Warning(exception, message, propertyValues);
        }

        public void LogError(string message)
        {
            Log.Error(message);
        }

        public void LogError(string message, params object?[]? propertyValues)
        {
            Log.Error(message, propertyValues);
        }

        public void LogError(Exception exception, string message, params object?[]? propertyValues)
        {
            Log.Error(exception, message, propertyValues);
        }
        public void LogFatal(string message)
        {
            Log.Fatal(message);
        }

        public void LogFatal(string message, params object?[]? propertyValues)
        {
            Log.Fatal(message, propertyValues);
        }

        public void LogFatal(Exception exception, string message, params object?[]? propertyValues)
        {
            Log.Fatal(exception, message, propertyValues);
        }

    }
}
