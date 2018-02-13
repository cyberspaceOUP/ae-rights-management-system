//Contributor:  Nicholas Mayne


namespace ACS.Services.Authentication.External
{
    public partial interface IExternalAuthorizer
    {
        AuthorizationResult Authorize(OpenAuthenticationParameters parameters);
    }
}