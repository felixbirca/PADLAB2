using FastEndpoints;
using FluentValidation;

namespace SyncNode.DTOs
{
    public class NodeInfoDto
    {
        public string IpAddress { get; set; } = string.Empty;
    }

    public class NodeInfoDtoValidator : Validator<NodeInfoDto>
    {
        public NodeInfoDtoValidator()
        {
            RuleFor(x => x.IpAddress)
                .NotEmpty()
                .WithMessage("IpAddress is required!");
        }
    }
}
