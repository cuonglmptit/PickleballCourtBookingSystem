using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services
{
    /// <summary>
    /// Service Base chung cho các class
    /// </summary>
    /// <typeparam name="T">Class muốn làm việc</typeparam>
    public class BaseService<T> : IBaseService<T> where T : class
    {
        /// <summary>
        /// baseRepository trong base service
        /// </summary>
        protected IBaseRepository<T> baseRepository;

        #region Constructors

        /// <summary>
        /// Constructor của BaseService
        /// Author: CuongLM (07/08/2024)
        /// </summary>
        /// <param name="baseRepository">DI sẽ tự tiêm vào</param>
        public BaseService(IBaseRepository<T> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tạo 1 service result
        /// </summary>
        /// <param name="Success">Thành công hoặc thất bại</param>
        /// <param name="UserMsg">User message</param>
        /// <param name="DevMsg">Dev message</param>
        /// <param name="StatusCode">Statuscode</param>
        /// <returns>Service Result với các tham số được truyền vào</returns>
        protected ServiceResult CreateServiceResult(bool Success, int StatusCode, string? UserMsg = null,
            string? DevMsg = null, object? Data = null)
        {
            ServiceResult result = new ServiceResult();
            result.Success = Success;
            result.UserMsg = UserMsg;
            result.DevMsg = DevMsg;
            result.StatusCode = StatusCode;
            result.Data = Data;
            return result;
        }

        public virtual ServiceResult DeleteService(Guid entityId)
        {
            try
            {
                var res = baseRepository.Delete(entityId);
                return CreateServiceResult(Success: res > 0, StatusCode: res > 0 ? 200 : 404);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Có lỗi xảy ra khi xóa đối tượng.", ex.Message);
            }
        }

        public virtual ServiceResult InsertService(T entity)
        {
            try
            {
                var res = baseRepository.Insert(entity);
                return CreateServiceResult(Success: res > 0, StatusCode: res > 0 ? 201 : 400);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Có lỗi xảy ra khi thêm đối tượng.", DevMsg: ex.Message);
            }
        }

        public ServiceResult InsertManyService(List<T> entities)
        {
            try
            {
                var res = baseRepository.InsertMany(entities);
                return CreateServiceResult(Success: res > 0, StatusCode: res > 0 ? 201 : 400);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Có lỗi xảy ra khi thêm đối tượng.", DevMsg: ex.Message);
            }
        }

        public virtual ServiceResult UpdateService(T entity, Guid id)
        {
            try
            {
                var res = baseRepository.Update(entity, id);
                return CreateServiceResult(Success: res > 0, StatusCode: res > 0 ? 200 : 404);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Có lỗi xảy ra khi cập nhật đối tượng.", DevMsg: ex.Message);
            }
        }
        
        // Update custom thuoc tinh
        public virtual ServiceResult UpdateCustomFieldService(T entity, Guid id)
        {
            try
            {
                var entityToUpdate = baseRepository.GetById(id);
                if (entityToUpdate == null)
                {
                    Console.WriteLine("Object is null");
                    return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Đối tượng không tồn tại.");
                }
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    var newValue = property.GetValue(entity);
                    if (newValue != null && newValue != property.GetValue(entityToUpdate))
                    {
                        property.SetValue(entityToUpdate, newValue);
                    }
                }
                var res = baseRepository.Update(entityToUpdate, id);
                return CreateServiceResult(Success: res > 0, StatusCode: res > 0 ? 200 : 404);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Có lỗi xảy ra khi cập nhật đối tượng.", DevMsg: ex.Message);
            }
        }

        public ServiceResult DeleteAnyService(List<Guid> ids)
        {
            try
            {
                var res = baseRepository.DeleteAny(ids);
                return CreateServiceResult(Success: res > 0, res > 0 ? 200 : 404);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Có lỗi xảy ra khi xóa đối tượng.", DevMsg: ex.Message);
            }
        }

        public ServiceResult GetAllService()
        {
            var res = baseRepository.GetAll();
            return CreateServiceResult(Success: true, StatusCode: 200, Data: res);
        }

        public ServiceResult GetByIdService(Guid entityId)
        {
            try
            {
                var res = baseRepository.GetById(entityId);
                return CreateServiceResult(Success:res != null, StatusCode: res != null ? 200 : 404, Data: res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Có lỗi xảy ra khi tìm đối tượng.", DevMsg: ex.Message);
            }
        }

        #endregion
    }
}
