using AutoMapper;
using Dapper;
using Holiday.API.Common.Extension;
using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request;
using Holiday.API.Infrastructures.Database;
using Holiday.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace Holiday.API.Repositories.Implement
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly DbConnectionHelper _connection;

        public HolidayRepository(DbConnectionHelper connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<HolidayEntity>> GetAsync(HolidayEntity? entity)
        {
            using var conn = _connection.GetConnection;

            var queryString = @"SELECT [Id]
                                      ,[Date]
                                      ,[name]
                                      ,[isHoliday]
                                      ,[holidayCategory]
                                      ,[Description]
                                FROM [dbo].[Holiday]
                                WHERE 1 = 1";

            if (entity is not null)
            {
                if (entity.IsHoliday is not null) { queryString += @" AND [IsHoliday] = @isHoliday"; }
                if (!String.IsNullOrEmpty(entity.HolidayCategory)) { queryString += @" AND [HolidayCategory] = @holidayCategory"; }
            }
            return await conn.QueryAsync<HolidayEntity>(queryString, entity);
        }

        public async Task<HolidayEntity?> GetByDateAsync(DateTime date)
        {
            using var conn = _connection.GetConnection;

            var queryString = @"SELECT [Id]
                                      ,[Date]
                                      ,[name]
                                      ,[isHoliday]
                                      ,[holidayCategory]
                                      ,[Description]
                                FROM [dbo].[Holiday]
                                WHERE Date = @Date";

            var para = new DynamicParameters();
            para.Add("Date", date);

            var result = await conn.QuerySingleOrDefaultAsync<HolidayEntity>(queryString, para);
            return result;
        }

        public async Task<HolidayEntity?> GetByIdAsync(int id)
        {
            using var conn = _connection.GetConnection;

            var queryString = @"SELECT [Id]
                                      ,[Date]
                                      ,[name]
                                      ,[isHoliday]
                                      ,[holidayCategory]
                                      ,[Description]
                                FROM [dbo].[Holiday]
                                WHERE [Id] = @id";

            var result = await conn.QuerySingleOrDefaultAsync<HolidayEntity>(queryString, new { id = id });
            return result;
        }

        public async Task<bool> InsertAsync(HolidayEntity entity)
        {
            using var conn = _connection.GetConnection;

            var insertString = @"INSERT INTO [dbo].[Holiday]
                                       ([Date]
                                       ,[Name]
                                       ,[IsHoliday]
                                       ,[HolidayCategory]
                                       ,[Description])
                                 VALUES
                                       (@date
                                       ,@name
                                       ,@isHoliday
                                       ,@holidayCategory
                                       ,@description)";

            var para = new DynamicParameters(entity);

            int affectedRow = await conn.ExecuteAsync(insertString, para);

            return affectedRow > 0;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {

            using var conn = _connection.GetConnection;

            var deleteString = @"DELETE FROM [dbo].[Holiday]
                                 WHERE Id = @id";

            int AffectedRow = await conn.ExecuteAsync(deleteString, new { id = id });

            return AffectedRow > 0;
        }

        public async Task<bool> UpdateAsync(HolidayEntity entity)
        {
            using var conn = _connection.GetConnection;

            var updateString = @"UPDATE [dbo].[Holiday]
                                   SET [Date] = @date
                                      ,[Name] = @name
                                      ,[IsHoliday] = @isHoliday
                                      ,[HolidayCategory] = @holidayCategory
                                      ,[description] = @Description
                                 WHERE [Id] = @id";

            var para = new DynamicParameters(entity);

            int AffectedRow = await conn.ExecuteAsync(updateString, para);
            return AffectedRow > 0;
        }

        public async Task<bool> InsertParsedCsvData(IEnumerable<HolidayEntity>? records)
        {
            using var conn = _connection.GetConnection;

            var insertString = @"INSERT INTO [PublicHoliday_TW].[dbo].[HolidayT1]
                                       ([Date]
                                       ,[Name]
                                       ,[IsHoliday]
                                       ,[HolidayCategory]
                                       ,[Description])
                                 VALUES
                                       (@Date
                                       ,@Name
                                       ,@IsHoliday
                                       ,@HolidayCategory
                                       ,@Description)";
        
            int affectedRow = await conn.ExecuteAsync(insertString, records);
            return affectedRow > 0;
        }
    }
}