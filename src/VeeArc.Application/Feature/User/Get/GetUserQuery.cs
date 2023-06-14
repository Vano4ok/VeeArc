using AutoMapper;
using MediatR;
using VeeArc.Application.Common.Interfaces;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.User.Get;

public class GetUserQuery : IRequest<UserResponse>
{
    public required int Id { get; init; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        DomainUser? user = await _userRepository.GetByIdAsync(request.Id);

        UserResponse response = _mapper.Map<UserResponse>(user);

        return response;
    }
}
