using AutoMapper;
using FluentValidation;

namespace Application.Abstraction;

public abstract class BaseCommandHandler<T>
{
    protected IMapper _mapper;
    protected IValidator<T> _validator;

    protected BaseCommandHandler(IValidator<T> validator, IMapper mapper)
    {
        _validator = validator;
        _mapper = mapper;
    }
}
