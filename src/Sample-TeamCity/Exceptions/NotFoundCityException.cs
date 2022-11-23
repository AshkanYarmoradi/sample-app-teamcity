namespace Sample_TeamCity.Exceptions;

public class NotFoundCityException: Exception
{
    public NotFoundCityException(): base("City Not Found!")
    {
        
    }
}