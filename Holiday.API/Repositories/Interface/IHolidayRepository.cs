using Holiday.API.Domain.Entity;
using Holiday.API.Domain.Request;
using Holiday.API.Domain.Response;

namespace Holiday.API.Repositories.Interface
{
    public interface IHolidayRepository
    {
        /// <summary>
        /// 取得多筆holiday
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IEnumerable<HolidayEntity>> GetAsync(HolidayEntity? entity);

        /// <summary>
        /// 取得單筆，依日期篩選
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<HolidayEntity?> GetByDateAsync(DateTime date);

        /// <summary>
        /// 取得單筆，依id篩選
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HolidayEntity?> GetByIdAsync(int id);
        

        /// <summary>
        /// 單筆新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(HolidayEntity entity);

        Task<bool> DeleteByIdAsync(int id);

       

    }
}
