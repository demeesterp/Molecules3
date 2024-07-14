namespace MoleculesWebApp.Handlers.Model
{
    /// <summary>
    /// Gives information about a service error
    /// </summary>
    public class ServiceError
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceError() { }


        /// <summary>
        ///Contructor with a given message
        /// </summary>
        public ServiceError(string message) 
        {
            DisplayMessage = message;
        }


        /// <summary>
        /// Message to display to the user
        /// </summary>
        public string DisplayMessage { get; set; } = "An unexpected error happend please contact support";
    }
}
