using PickleballCourtBookingSystem.Core.DTOs;
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
            string? DevMsg = null)
        {
            ServiceResult result = new ServiceResult();
            result.Success = Success;
            result.UserMsg = UserMsg;
            result.DevMsg = DevMsg;
            result.StatusCode = StatusCode;
            return result;
        }

        public virtual ServiceResult DeleteService(Guid entityId)
        {
            var res = baseRepository.Delete(entityId);

            ServiceResult serviceResult = new ServiceResult();

            if (res > 0)
            {
                serviceResult = CreateServiceResult(Success: true, StatusCode: 200);
            }
            else
            {
                serviceResult = CreateServiceResult(Success: false, StatusCode: 404);
            }

            return serviceResult;
        }

        public virtual ServiceResult InsertService(T entity)
        {
            var res = baseRepository.Insert(entity);

            ServiceResult serviceResult = new ServiceResult();

            if (res > 0)
            {
                serviceResult = CreateServiceResult(Success: true, StatusCode: 201);
            }
            else
            {
                serviceResult = CreateServiceResult(Success: false, StatusCode: 400);
            }

            return serviceResult;
        }

        public virtual ServiceResult UpdateService(T entity, Guid id)
        {
            var res = baseRepository.Update(entity, id);

            ServiceResult serviceResult = new ServiceResult();

            if (res > 0)
            {
                serviceResult = CreateServiceResult(Success: true, StatusCode: 201);
            }
            else
            {
                serviceResult = CreateServiceResult(Success: false, StatusCode: 404);
            }

            return serviceResult;
        }

        public ServiceResult DeleteAnyService(List<Guid> ids)
        {
            var res = baseRepository.DeleteAny(ids);

            ServiceResult serviceResult = new ServiceResult();

            if (res > 0)
            {
                serviceResult = CreateServiceResult(Success: true, StatusCode: 201);
            }
            else
            {
                serviceResult = CreateServiceResult(Success: false, StatusCode: 404);
            }

            return serviceResult;
        }

        #endregion
    }
}