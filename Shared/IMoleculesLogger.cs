namespace Shared
{
    public interface IMoleculesLogger
    {
        void LogVerbose(string message);

        void LogVerbose(string message, params object?[]? propertyValues);


        void LogVerbose(Exception exception, string message, params object?[]? propertyValues);

        void LogDebug(string message);

        void LogDebug(string message, params object?[]? propertyValues);

        void LogDebug(Exception exception, string message, params object?[]? propertyValues);

        void LogInformation(string message);

        void LogInformation(string message, params object?[]? propertyValues);

        void LogInformation(Exception exception, string message, params object?[]? propertyValues);

        void LogWarning(string message);

        void LogWarning(string message, params object?[]? propertyValues);

        void LogWarning(Exception exception, string message, params object?[]? propertyValues);

        void LogError(string message);

        void LogError(string message, params object?[]? propertyValues);

        void LogError(Exception exception, string message, params object?[]? propertyValues);
        void LogFatal(string message);

        void LogFatal(string message, params object?[]? propertyValues);

        void LogFatal(Exception exception, string message, params object?[]? propertyValues);
    }
}
