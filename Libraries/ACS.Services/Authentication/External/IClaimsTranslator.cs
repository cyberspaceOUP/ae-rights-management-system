//Contributor:  Nicholas Mayne


namespace ACS.Services.Authentication.External
{
    public partial interface IClaimsTranslator<T>
    {
        UserClaims Translate(T response);
    }
}