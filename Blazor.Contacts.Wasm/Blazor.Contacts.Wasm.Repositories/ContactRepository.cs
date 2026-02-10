using System;
using System.Data;
using Blazor.Contacts.Wasm.Shared;
using Dapper;

namespace Blazor.Contacts.Wasm.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly IDbConnection _dbConnection;

    public ContactRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<bool> DeleteContact(int id)
    {
        var sql = @"    DELETE
                        FROM [dbo].[Contacts]
                        WHERE   [Id] = @Id";
        
        var result = await _dbConnection.ExecuteAsync(
            sql, new { Id = id }
        );

        return result > 0;

    }

    public async Task<IEnumerable<Contact>> GetAll()
    {
        var sql = @"    SELECT  [Id]
                                ,[FirstName]
                                ,[LastName]
                                ,[Phone]
                                ,[Address]
                        FROM [dbo].[Contacts]";

        return await _dbConnection.QueryAsync<Contact>(
            sql, new {}
        );
    }

    public async Task<Contact> GetDetails(int id)
    {
        var sql = @"    SELECT  [Id]
                                ,[FirstName]
                                ,[LastName]
                                ,[Phone]
                                ,[Address]
                        FROM [dbo].[Contacts]
                        WHERE   [Id] = @Id";

        return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(
            sql, new { Id = id }
        );
    }

    public async Task<bool> InsertContact(Contact contact)
    {
        try{
            var sql = @" INSERT INTO [dbo].[Contacts]([FirstName],[LastName],[Phone],[Address]) 
                        VALUES(@FirstName, @LastName, @Phone, @Address)";
            
            var result = await _dbConnection.ExecuteAsync(
                sql,new{
                    contact.FirstName,
                    contact.LastName,
                    contact.Phone,
                    contact.Address
                }
            );

            return result > 0;
        }
        catch(Exception e)
        {
            throw e;
        }
    }

    public async Task<bool> UpdateContact(Contact contact)
    {
        try{
            var sql = @" UPDATE [dbo].[Contacts]
                            SET [FirstName]=@FirstName,
                                [LastName]=@LastName,
                                [Phone]=@Phone,
                                [Address]=@Address 
                        WHERE   [Id] = @Id";
            
            var result = await _dbConnection.ExecuteAsync(
                sql,new{
                    contact.FirstName,
                    contact.LastName,
                    contact.Phone,
                    contact.Address,
                    contact.Id
                }
            );
            return result > 0;
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }
    }
}
