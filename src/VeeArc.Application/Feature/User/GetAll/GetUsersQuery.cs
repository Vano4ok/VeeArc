using AutoMapper;
using MediatR;
using VeeArc.Application.Common.Interfaces;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.User.GetAll;

public class GetUsersQuery : IRequest<List<UserResponse>>
{
    
}

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersHandler(IUserRepository userRepository,
                           IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        List<DomainUser> users = await _userRepository.GetAllAsync();

        List<UserResponse> userResponses = _mapper.Map<List<DomainUser>, List<UserResponse>>(users);

        return userResponses;
    }
}
