namespace ACS.Core.Domain.Contact
{
    public enum ContactLoginResults : int
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Customer dies not exist (email or username)
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// Customer not registered 
        /// </summary>
        NotRegistered = 6,
    }
}
