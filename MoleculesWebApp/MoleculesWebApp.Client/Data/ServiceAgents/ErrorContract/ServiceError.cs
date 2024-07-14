namespace MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract
{
    /// <summary>
    /// Gives information about a service error
    /// </summary>
    public class ServiceError : IServiceError
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceError() { }


        /// <summary>
        /// Message to display to the user
        /// </summary>
        public string DisplayMessage { get; set; } = "An unexpected error happend please contact support";

        /// <summary>
        /// Get the standard error message
        /// </summary>
        /// <returns>standard error message</returns>
        public string GetErrorMsg()
        {
            return DisplayMessage;
        }
    }
}
