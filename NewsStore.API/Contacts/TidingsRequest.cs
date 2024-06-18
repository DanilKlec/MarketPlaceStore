using NewsStore.Core.Models;
using System;

namespace NewsStore.API.Contacts
{
    public record TidingsRequest(
        Guid Id,
        int? number,
        string name,
        string description,
        int rating,
        Users? user);
}
