namespace MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract
{
    /// <summary>
    /// An error item to be send to the user in case of error
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="message">The error message</param>
    /// <param name="propertyName">The name of the property to what it applies</param>
    public class ServiceValidationErrorItem(string message, string propertyName)
    {

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; } = message;

        /// <summary>
        /// The name of the property to what it applies
        /// </summary>
        public string PropertyName { get; set; } = propertyName;
    }
}
