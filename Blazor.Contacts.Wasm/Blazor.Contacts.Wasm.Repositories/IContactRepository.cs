using System;
using Blazor.Contacts.Wasm.Shared;

namespace Blazor.Contacts.Wasm.Repositories;

public interface IContactRepository
{
    Task<bool> InsertContact(Contact contact);
    Task<bool> UpdateContact(Contact contact);
    Task<bool> DeleteContact(int id);
    Task<IEnumerable<Contact>> GetAll();
    Task<Contact> GetDetails(int id);
}
