using HRSystem.BLL.ModelVM.Position;

namespace HRSystem.BLL.Services.Implementation
{
    public class PositionService: IPositionService
    {
        /* ------------------------------------------------------------------------------------ */

        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /* ------------------------------------------------------------------------------------ */

        public PositionService(IPositionRepository positionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<IEnumerable<PositionVM>>> GetAllPositionsAsync()
        {
            try
            {
                var result = await _positionRepository.GetPositionssWithDepartmentNamesAndNumOfEmployees();

                var mapp = result.Select(p => new PositionVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    DepartmentId = p.DepartmentId,
                    DepartmentName = p.Department.Name,
                    EmployeesCount = p.Employees.Count
                });

                return new Response<IEnumerable<PositionVM>>(mapp, null, true);

            }
            catch (Exception ex)
            {

                return new Response<IEnumerable<PositionVM>>(null, ex.Message, false);
            }

        }

        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<PositionVM>> GetPositionByIdAsync(Guid id)
        {
            try
            {
                var isExist = await _positionRepository.FindAsync(d => d.Id == id);
                if (isExist == null)
                {
                    return new Response<PositionVM>(null, "Position not found", false);
                }
                else
                {
                    var result = await _positionRepository.GetPositionByIdWithItsRelatedDepartment(id);

                    var mapp = new PositionVM
                    {
                        Id = result.Id,
                        Name = result.Name,
                        DepartmentId = result.Department.Id,
                        DepartmentName = result.Department.Name,
                    };

                    return new Response<PositionVM>(mapp, null, true);
                }
            }
            catch (Exception ex)
            {
                return new Response<PositionVM>(null, ex.Message, false);
            }

        }

        /* ------------------------------------------------------------------------------------ */


        public async Task<Response<bool>> AddPositionAsync(PositionFormVM positionVM)
        {
            try
            {
                var position = _mapper.Map<Position>(positionVM);

                await _positionRepository.AddAsync(position);
                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }

        /* ------------------------------------------------------------------------------------ */
        public async Task<Response<bool>> UpdatePositionAsync(PositionFormVM positionVM)
        {
            try
            {
                var position = await _positionRepository.GetByIdAsync(positionVM.Id.Value);
                if (position == null)
                {
                    return new Response<bool>(false, "Department not found", false);
                }
                else
                {
                    position.Name = positionVM.Name;
                    position.DepartmentId = positionVM.DepartmentId;

                    _positionRepository.Update(position);

                    await _unitOfWork.SaveChangesAsync();

                    return new Response<bool>(true, null, true);
                }

            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);

            }
        }

        /* ------------------------------------------------------------------------------------ */


        public async Task<Response<bool>> DeletePositionAsync(Guid id)
        {
            try
            {
                var dept = await _positionRepository.GetByIdAsync(id);
                if (dept == null)
                {
                    return new Response<bool>(false, "Position not found", false);
                }
                else
                {

                    _positionRepository.Delete(dept);
                    await _unitOfWork.SaveChangesAsync();

                    return new Response<bool>(true, null, true);
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }


        /* ------------------------------------------------------------------------------------ */

    }
}
