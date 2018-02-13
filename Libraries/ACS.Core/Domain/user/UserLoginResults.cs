namespace ACS.Core.Domain.User
{
    public enum UserLoginResults : int
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// User dies not exist (email or username)
        /// </summary>
        UserNotExist = 2,
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
        /// Contact not registered 
        /// </summary>
        NotRegistered = 6,

        CustomerNotExist = 2,

        //Added by sanjeet 
        /// <summary>
        /// User ALL Ready logged In
        /// </summary>
        AllReadyLogged = 7,

        /// <summary>
        /// User Blocked
        /// </summary>
        UserBlocked = 8,
        //
    }
}
