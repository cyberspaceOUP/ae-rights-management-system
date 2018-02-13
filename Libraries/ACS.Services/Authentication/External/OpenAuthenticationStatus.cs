//Contributor:  Nicholas Mayne


namespace ACS.Services.Authentication.External
{
    public enum OpenAuthenticationStatus
    {
        Unknown,
        Error,
        Authenticated,
        RequiresRedirect,
        AssociateOnLogon,
        AutoRegisteredEmailValidation,
        AutoRegisteredAdminApproval,
        AutoRegisteredStandard,
    }
}