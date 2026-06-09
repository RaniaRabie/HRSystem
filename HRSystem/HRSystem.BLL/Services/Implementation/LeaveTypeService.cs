

using HRSystem.BLL.ModelVM.LeaveType;
using HRSystem.DAL.Repository.Implementation;

namespace HRSystem.BLL.Services.Implementation
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository, IUnitOfWork unitOfWork)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<LeaveTypeVM>>> GetAllAsync()
        {
            try
            {

                var result = await _leaveTypeRepository.GetAllWithRequestsCountAsync();

                var mapp = result.Select(lt => new LeaveTypeVM
                {
                    Id = lt.Id,
                    Name = lt.Name,
                    RequestsCount = lt.LeaveRequests.Count
                });

                return new Response<IEnumerable<LeaveTypeVM>>(mapp, null, true);

            }catch (Exception ex)
            {
                return new Response<IEnumerable<LeaveTypeVM>>(null, ex.Message, false);

            }

        }


        public async Task<Response<LeaveTypeFormVM>> GetForEditAsync(Guid id)
        {
            try
            {

                var result = await _leaveTypeRepository.GetByIdAsync(id);
                if (result == null)
                    return new Response<LeaveTypeFormVM>(null, "Leave type not found", false);


                var mapp = new LeaveTypeFormVM { Id = result.Id, Name = result.Name };

                return new Response<LeaveTypeFormVM>(mapp, null, true);

            }
            catch (Exception ex)
            {
                return new Response<LeaveTypeFormVM>(null, ex.Message, false);

            }

        }

        public async Task<Response<bool>> CreateAsync(LeaveTypeFormVM vm)
        {
            try
            {
                // Check if LeaveType Exists before Create new one
                var exists = await _leaveTypeRepository.FindAsync(lt => lt.Name.ToLower() == vm.Name.ToLower().Trim());

                if (exists != null)
                {
                    return new Response<bool>(false, "Leave type already exists", false);

                }

                // then add
                await _leaveTypeRepository.AddAsync(new LeaveType { Name = vm.Name });

                // then Save
                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);

            }

        }

        public async Task<Response<bool>> UpdateAsync(LeaveTypeFormVM vm)
        {
            try
            {
                var entity = await _leaveTypeRepository.GetByIdAsync(vm.Id!.Value);
                if (entity == null)
                {
                    return new Response<bool>(false, "Leave type not found", false);
                }

                entity.Name = vm.Name;
                _leaveTypeRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);
            }
            catch (Exception ex) {
                return new Response<bool>(false, ex.Message, false);
            }
            

        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var lt = await _leaveTypeRepository.GetByIdAsync(id);
                if (lt == null)
                {
                    return new Response<bool>(false, "Leave type not found", false);
                }
                else
                {

                    _leaveTypeRepository.Delete(lt);
                    await _unitOfWork.SaveChangesAsync();

                    return new Response<bool>(true, null, true);
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }



    }
}
