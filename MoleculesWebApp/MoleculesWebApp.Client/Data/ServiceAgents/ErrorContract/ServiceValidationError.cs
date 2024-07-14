namespace MoleculesWebApp.Client.Data.ServiceAgents.ErrorContract
{
    /// <summary>
    /// Gives information about a service error
    /// </summary>
    public class ServiceValidationError : IServiceError
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceValidationError()
        {
            ValidationErrors = new List<ServiceValidationErrorItem>();
        }

        /// <summary>
        /// The list of error
        /// </summary>
        public IList<ServiceValidationErrorItem> ValidationErrors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetErrorMsg()
        {
            return "A validation error occured";
        }
    }
}
