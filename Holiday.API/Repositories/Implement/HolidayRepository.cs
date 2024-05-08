using AutoMapper;
using Dapper;
using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request;
using Holiday.API.Infrastructures.Database;
using Holiday.API.Repositories.Interface;
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
            var queryString = "";

            using var conn = _connection.GetConnection;

            if (entity is not null)
            {
                if (entity.IsHoliday is not null)
                {
                    queryString = @"SELECT [Id]
                                      ,[Date]
                                      ,[name]
                                      ,[isHoliday]
                                      ,[holidayCategory]
                                      ,[discription]
                                FROM [dbo].[Holiday]
                                WHERE [isHoliday] = @isHoliday";

                    return await conn.QueryAsync<HolidayEntity>(queryString, new { isHoliday = entity.IsHoliday });
                }
                queryString = @"SELECT [Id]
                                       ,[Date]
                                       ,[name]
                                       ,[isHoliday]
                                       ,[holidayCategory]
                                       ,[discription]
                                FROM [dbo].[Holiday]";

                return await conn.QueryAsync<HolidayEntity>(queryString);
            }
          
            return await conn.QueryAsync<HolidayEntity>(queryString);

        }

        public async Task<HolidayEntity?> GetByDateAsync(DateTime date)
        {
            using var conn = _connection.GetConnection;

            var queryString = @"SELECT [Id]
                                      ,[Date]
                                      ,[name]
                                      ,[isHoliday]
                                      ,[holidayCategory]
                                      ,[discription]
                                FROM [dbo].[Holiday]
                                WHERE Date = @Date";

            var para = new DynamicParameters();
            para.Add("Date", date);

            var result = await conn.QuerySingleOrDefaultAsync<HolidayEntity>(queryString, para);
            return result;
        }

        public async Task<bool> InserAsync(HolidayEntity entity)
        {
            using var conn = _connection.GetConnection;

            entity.IsHoliday = true;

            var insertString = @"INSERT INTO [dbo].[Holiday]
                                       ([Date]
                                       ,[Name]
                                       ,[IsHoliday]
                                       ,[HolidayCategory]
                                       ,[Discription])
                                 VALUES
                                       (@date
                                       ,@name
                                       ,@isHoliday
                                       ,@holidayCategory
                                       ,@discription)";

            var para = new DynamicParameters(entity);

            int AffectRow = await conn.ExecuteAsync(insertString, para);

            return AffectRow > 0;          
        }
    }
}
