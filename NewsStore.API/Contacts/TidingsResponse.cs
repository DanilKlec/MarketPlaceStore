namespace NewsStore.API.Contacts
{
    public record TidingsResponse(
        Guid Id,
        int? number,
        string description,
        int rating,
        Guid? userId);
}
